﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PHP.Core;
using System.Collections;
using System.Reflection;
using PHP.Core.Reflection;
using Convert = PHP.Core.Convert;

namespace PHP.Library.Soap
{
    class ParameterBinder
    {
        #region Fields and Properties

        /// <summary>
        /// Stack of objects being currently serialized. Used to avoid stack overflow and to properly outputs "recursion_detected" warning.
        /// </summary>
        private List<object> recursionStack = null;

        private bool lastPrimitive;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new <see cref="ParameterBinder"/>
        /// </summary>
        internal ParameterBinder()
        {
        }

        #endregion

        #region Recursion

        /// <summary>
        /// Push currently serialized array or object to the stack to prevent recursion.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool PushObject(object/*!*/obj)
        {
            Debug.Assert(obj != null);

            if (recursionStack == null)
                recursionStack = new List<object>(8);
            else
            {
                // check recursion
                for (int i = 0; i < recursionStack.Count; i++)
                    if (recursionStack[i] == obj)
                        return false;
            }

            recursionStack.Add(obj);
            return true;
        }

        /// <summary>
        /// Pop the serialized object from the stack.
        /// </summary>
        private void PopObject()
        {
            Debug.Assert(recursionStack != null);
            recursionStack.RemoveAt(recursionStack.Count - 1);
        }

        #endregion

        #region Bind* methods

        public object[] BindParams(MethodInfo mi, PhpArray parameters, bool wrappedArgs)
        {
            var resultParams = new List<object>();
            var parameterInfos = mi.GetParameters();
            object value;


            if (!wrappedArgs)
            {
                //TODO: make sure: When arguments are not wrapped soap method parameter is only one
                Debug.Assert(parameterInfos.Length == 1);

                resultParams.Add(Bind(parameters, parameterInfos[0].ParameterType));

            }
            else
            {
                foreach (var pi in parameterInfos)
                {
                    if (SetSpecifiedParameter(resultParams, pi))
                        continue;
                    value = null;
                    if (parameters == null || parameters.TryGetValue(pi.Name, out value))
                    {
                        resultParams.Add(Bind(value, pi.ParameterType));
                    }
                    else
                    {
                        resultParams.Add(pi.ParameterType.IsValueType ? Activator.CreateInstance(pi.ParameterType) : null);
                    }
                }
            }

            lastPrimitive = false;

            return resultParams.ToArray();
        }

        /// <summary>
        /// Serializes an PHP object or graph of objects to CLR object.
        /// </summary>
        /// <param name="graph">The PHP object (graph) to serialize.</param>
        /// <param name="targetType">Expected type of the return argument</param>
        private object Bind(object graph, Type targetType)
        {
            if (graph == null)
            {
                if(!targetType.IsValueType)
                    return null;
                return Activator.CreateInstance(targetType);
            }

            // unwrap Nullable<>
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                targetType = targetType.GetGenericArguments()[0];
            if (targetType.IsByRef)
                targetType = targetType.GetElementType();

            switch (Type.GetTypeCode(graph.GetType()))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Char:
                case TypeCode.String:
                    return BindPrimitiveType(graph, targetType);

                case TypeCode.Object:
                    {
                        PhpArray array;
                        if ((array = graph as PhpArray) != null)
                        {
                            if (PushObject(graph))
                            {
                                object res = BindArray(array, targetType);
                                PopObject();
                                return res;
                            }
                            else
                                return BindEmptyInstance(targetType);// Could this happen?
                        }

                        DObject obj;
                        if ((obj = graph as DObject) != null)
                        {
                            if (PushObject(graph))
                            {
                                object res = BindObject(obj, targetType);
                                PopObject();
                                return res;
                            }
                            else
                                return BindEmptyInstance(targetType);
                        }

                        PhpReference reference;
                        if ((reference = graph as PhpReference) != null)
                        {
                            return Bind(reference.Value, targetType);
                        }

                        PhpBytes bytes;
                        if ((bytes = graph as PhpBytes) != null)
                        {
                            return BindPrimitiveType(bytes.ToString(), targetType);
                        }

                        PhpString str;
                        if ((str = graph as PhpString) != null)
                        {
                            return BindPrimitiveType(str.ToString(), targetType);
                        }

                        if (graph is PhpResource)
                        {
                            return BindUnsupported(PhpResource.PhpTypeName);
                        }

                        goto default;
                    }

                default:
                    return BindUnsupported(graph.GetType().FullName);
            }
        }

        private object BindPrimitiveType(object obj, Type targetType)
        {
            if (targetType.IsEnum)
            {
                if (obj.GetType() == typeof(String))
                {
                    lastPrimitive = true;
                    string str = (string)obj;
                    return Enum.Parse(targetType, str);
                }
            }

            if (targetType.IsArray)//We are assign one element to array
            {
                var arr = new object[]{obj};
                return BindArrayToArray(new PhpArray(arr), targetType);
            }


            if (targetType.IsValueType)
                lastPrimitive = true;

            if (targetType == typeof (string))
                obj = Encoding.UTF8.GetString(Convert.ObjectToPhpBytes(obj).ReadonlyData);

            return PHP.Core.ConvertToClr.ObjectToType(obj, targetType);
        }

        private object BindEmptyInstance(Type targetType)
        {
            if (targetType.IsArray)
            {
                Type elementType = targetType.GetElementType();
                return Array.CreateInstance(elementType, 0);
            }
            else
            {
                return Activator.CreateInstance(targetType);
            }
        }


        /// <summary>
        /// Serializes null and throws an exception.
        /// </summary>
        /// <param name="TypeName"></param>
        private object BindUnsupported(string TypeName)
        {
            PhpException.Throw(PhpError.Warning, LibResources.GetString("serialization_unsupported_type", TypeName));
            return null;
        }

        private object BindObject(DObject obj, Type targetType)
        {
            object instance = Activator.CreateInstance(targetType);// no ctor parameters

            FieldInfo[] fi = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            object value;

            foreach (var field in fi)
            {
                if (SetSpecifiedField(instance, field))
                    continue;

                value = obj.GetProperty(field.Name, null, true);

                if (value != null)
                {
                    field.SetValue(instance, Bind(value, field.FieldType));
                }
            }

            lastPrimitive = false;

            return instance;
        }

        private object BindArray(PhpArray array, Type targetType)
        {
            if (targetType.IsArray)
            {
                array = (PhpArray) (array.ContainsKey("item") ? array["item"] : array); // TODO: ???
                return BindArrayToArray(array, targetType);
            }
            else
                return BindArrayToObject(array, targetType);
        }

        private object BindArrayToObject(PhpArray array, Type targetType)
        {
            object instance = Activator.CreateInstance(targetType);// no ctor parameters

            FieldInfo[] fi = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            object value;

            foreach (var field in fi)
            {
                if (SetSpecifiedField(instance, field))
                    continue;

                if (array.TryGetValue(field.Name, out value))
                {
                    field.SetValue(instance, Bind(value, field.FieldType));
                }
            }

            lastPrimitive = false;

            return instance;
        }


        //SetSpecified(Parameter|Field) has to be here, because .NET generates proxy objects with special parameters when value type is present.
        //The reason is value types are not Nullable so this special parameter indicates if value is present or not
        //http://stackoverflow.com/questions/3362574/how-to-get-rid-of-xmlignoreattribute-when-creating-proxy-dynamically-from-wsdl
                
        private bool SetSpecifiedParameter(List<object> resultParams, ParameterInfo pi)
        {
            //check XmlIgnoreAttribute
            //There is case when field always has to be specified so *Specified field isn't present
            if (lastPrimitive && pi.IsDefined(typeof(System.Xml.Serialization.XmlIgnoreAttribute), false))
            {
                resultParams.Add(true);
                lastPrimitive = false;
                return true;
            }

            lastPrimitive = false;
            return false;
        }

        private bool SetSpecifiedField(object instance, FieldInfo field)
        {
            //check XmlIgnoreAttribute
            //There is case when field always has to be specified so *Specified field isn't present
            if (lastPrimitive && field.IsDefined(typeof(System.Xml.Serialization.XmlIgnoreAttribute), false))
            {
                field.SetValue(instance, true);
                lastPrimitive = false;
                return true;
            }

            lastPrimitive = false;
            return false;
        }

        private object BindArrayToArray(PhpArray array, Type targetType)
        {
            Debug.Assert(targetType.IsArray);

            var count = array == null ? 0 : array.Count;

            Type elementType = targetType.GetElementType();
            Array vals = Array.CreateInstance(elementType, count);

            for (int i = 0; i < count; ++i)
            {
                vals.SetValue(Bind(array[i], elementType), i);
            }

            lastPrimitive = false;

            return vals;
        }


        #endregion

        public object[] BindServerResult(MethodInfo mi, PhpArray result, bool wrappedArgs)
        {
            var resultParams = new List<object>();
            var parameterInfos = new List<ParameterInfo>();
            parameterInfos.Add(mi.ReturnParameter);
            parameterInfos.AddRange(mi.GetParameters().Where(a=>a.IsOut));
            object value;


            if (!wrappedArgs)
            {
                //TODO: make sure: When arguments are not wrapped soap method parameter is only one
                Debug.Assert(parameterInfos.Count == 1);

                resultParams.Add(Bind(result, parameterInfos[0].ParameterType));

            }
            else
            {
                foreach (var pi in parameterInfos)
                {
                    if (SetSpecifiedParameter(resultParams, pi))
                        continue;
                    var name = WsdlHelper.GetParameterSoapName(pi);
                    if (result.TryGetValue(name, out value))
                    {
                        resultParams.Add(Bind(value, pi.ParameterType));
                    }
                }
            }

            lastPrimitive = false;

            return resultParams.ToArray();
        }
    }
}
