/*

 Copyright (c) 2006 Tomas Matousek.

 The use and distribution terms for this software are contained in the file named License.txt, 
 which can be found in the root of the Phalanger distribution. By using this software 
 in any fashion, you are agreeing to be bound by the terms of this license.
 
 You must not remove this notice from this software.

*/


namespace PHP.Core.Parsers
{
	public enum TokenCategory
	{
		Comment,
		Delimiter,
		Identifier,
		Keyword,
		LineComment,
		Number,
		Operator,
		String,
		Text,
		Unknown,
		WhiteSpace,

		Variable,
		ScriptTags,
		StringCode,
		Html
	}

	// todo: match token numbers against PHP

	public enum Tokens
	{
		ERROR = Toks.ERROR,
		EOF = Toks.EOF,

		T_STRING_E = -20,
		T_LINE_COMMENT = -21,

		// commands:
        ParseBinaryNumber = -25,
		ParseDecimalNumber = -26,
		ParseHexadecimalNumber = -27,
		ParseDouble = -28,
		DoubleQuotedString = -29,
		SingleQuotedString = -30,
		SingleQuotedIdentifier = -31,
		OctalCharCode = -32,
		HexCharCode = -33,
		UnicodeCharCode = -34,
		UnicodeCharName = -35,
		EscapedCharacter = -36,

		// special lexer errors:
		ErrorInvalidIdentifier = -37,
		ErrorNotSupported = -38,

		T_GET = Toks.T_GET,
		T_SET = Toks.T_SET,
		T_CALL = Toks.T_CALL,
        T_CALLSTATIC = Toks.T_CALLSTATIC,
		T_TOSTRING = Toks.T_TOSTRING,
		T_CONSTRUCT = Toks.T_CONSTRUCT,
		T_DESTRUCT = Toks.T_DESTRUCT,
		T_PARENT = Toks.T_PARENT,
		T_SELF = Toks.T_SELF,
		T_AUTOLOAD = Toks.T_AUTOLOAD,

		T_ASSERT = Toks.T_ASSERT,
		T_TRUE = Toks.T_TRUE,
		T_FALSE = Toks.T_FALSE,
		T_NULL = Toks.T_NULL,
		T_WAKEUP = Toks.T_WAKEUP,
		T_SLEEP = Toks.T_SLEEP,

		T_LINQ_SELECT = Toks.T_LINQ_SELECT,
		T_LINQ_BY = Toks.T_LINQ_BY,
		T_LINQ_WHERE = Toks.T_LINQ_WHERE,
		T_LINQ_DESCENDING = Toks.T_LINQ_DESCENDING,
		T_LINQ_ASCENDING = Toks.T_LINQ_ASCENDING,
		T_LINQ_ORDERBY = Toks.T_LINQ_ORDERBY,
		T_LINQ_GROUP = Toks.T_LINQ_GROUP,
		T_LINQ_FROM = Toks.T_LINQ_FROM,
		T_LINQ_IN = Toks.T_LINQ_IN,

		T_LGENERIC = Toks.T_LGENERIC,
		T_RGENERIC = Toks.T_RGENERIC,
		T_PARTIAL = Toks.T_PARTIAL,

		T_GOTO = Toks.T_GOTO,
		T_IMPORT = Toks.T_IMPORT,
		T_NAMESPACE = Toks.T_NAMESPACE,
		T_NAMESPACE_C = Toks.T_NAMESPACE_C,
		T_NS_SEPARATOR = Toks.T_NS_SEPARATOR,
        T_USE = Toks.T_USE,
		T_BINARY_DOUBLE = Toks.T_BINARY_DOUBLE,
		T_BINARY_HEREDOC = Toks.T_BINARY_HEREDOC,

		T_BOOL_TYPE = Toks.T_BOOL_TYPE,
		T_INT_TYPE = Toks.T_INT_TYPE,
		T_INT64_TYPE = Toks.T_INT64_TYPE,
		T_DOUBLE_TYPE = Toks.T_DOUBLE_TYPE,
		T_STRING_TYPE = Toks.T_STRING_TYPE,
		T_RESOURCE_TYPE = Toks.T_RESOURCE_TYPE,
		T_OBJECT_TYPE = Toks.T_OBJECT_TYPE,
		T_TYPEOF = Toks.T_TYPEOF,

		T_INT8_CAST = Toks.T_INT8_CAST,
		T_INT16_CAST = Toks.T_INT16_CAST,
		T_INT32_CAST = Toks.T_INT32_CAST,
		T_INT64_CAST = Toks.T_INT64_CAST,
		T_UINT8_CAST = Toks.T_UINT8_CAST,
		T_UINT16_CAST = Toks.T_UINT16_CAST,
		T_UINT32_CAST = Toks.T_UINT32_CAST,
		T_UINT64_CAST = Toks.T_UINT64_CAST,
		T_FLOAT_CAST = Toks.T_FLOAT_CAST,
		T_DECIMAL_CAST = Toks.T_DECIMAL_CAST,
		T_UNICODE_CAST = Toks.T_UNICODE_CAST,

		T_PRAGMA_LINE = Toks.T_PRAGMA_LINE,
		T_PRAGMA_FILE = Toks.T_PRAGMA_FILE,
		T_PRAGMA_DEFAULT_FILE = Toks.T_PRAGMA_DEFAULT_FILE,
		T_PRAGMA_DEFAULT_LINE = Toks.T_PRAGMA_DEFAULT_LINE,


		T_LPAREN = (int)'(',
		T_RPAREN = (int)')',
		T_LBRACKET = (int)'[',
		T_RBRACKET = (int)']',
		T_LBRACE = (int)'{',
		T_RBRACE = (int)'}',
		T_SEMI = (int)';',
		T_COLON = (int)':',
		T_COMMA = (int)',',
		T_DOT = (int)'.',
		T_PIPE = (int)'|',
		T_CARET = (int)'^',
		T_AMP = (int)'&',
		T_PLUS = (int)'+',
		T_MINUS = (int)'-',
		T_SLASH = (int)'/',
		T_MUL = (int)'*',
		T_EQ = (int)'=',
		T_PERCENT = (int)'%',
		T_EXCLAM = (int)'!',
		T_TILDE = (int)'~',
		T_DOLLAR = (int)'$',
		T_LT = (int)'<',
		T_GT = (int)'>',
		T_QUESTION = (int)'?',
		T_AT = (int)'@',
		T_DOUBLE_QUOTES = (int)'"',
		T_BACKQUOTE = (int)'`',

		T_REQUIRE_ONCE = Toks.T_REQUIRE_ONCE,
		T_REQUIRE = Toks.T_REQUIRE,
		T_EVAL = Toks.T_EVAL,
		T_INCLUDE_ONCE = Toks.T_INCLUDE_ONCE,
		T_INCLUDE = Toks.T_INCLUDE,
		T_LOGICAL_OR = Toks.T_LOGICAL_OR,
		T_LOGICAL_XOR = Toks.T_LOGICAL_XOR,
		T_LOGICAL_AND = Toks.T_LOGICAL_AND,
		T_PRINT = Toks.T_PRINT,
		T_SR_EQUAL = Toks.T_SR_EQUAL,
		T_SL_EQUAL = Toks.T_SL_EQUAL,
		T_XOR_EQUAL = Toks.T_XOR_EQUAL,
		T_OR_EQUAL = Toks.T_OR_EQUAL,
		T_AND_EQUAL = Toks.T_AND_EQUAL,
		T_MOD_EQUAL = Toks.T_MOD_EQUAL,
		T_CONCAT_EQUAL = Toks.T_CONCAT_EQUAL,
		T_DIV_EQUAL = Toks.T_DIV_EQUAL,
		T_MUL_EQUAL = Toks.T_MUL_EQUAL,
		T_MINUS_EQUAL = Toks.T_MINUS_EQUAL,
		T_PLUS_EQUAL = Toks.T_PLUS_EQUAL,
		T_BOOLEAN_OR = Toks.T_BOOLEAN_OR,
		T_BOOLEAN_AND = Toks.T_BOOLEAN_AND,
		T_IS_NOT_IDENTICAL = Toks.T_IS_NOT_IDENTICAL,
		T_IS_IDENTICAL = Toks.T_IS_IDENTICAL,
		T_IS_NOT_EQUAL = Toks.T_IS_NOT_EQUAL,
		T_IS_EQUAL = Toks.T_IS_EQUAL,
		T_IS_GREATER_OR_EQUAL = Toks.T_IS_GREATER_OR_EQUAL,
		T_IS_SMALLER_OR_EQUAL = Toks.T_IS_SMALLER_OR_EQUAL,
		T_SR = Toks.T_SR,
		T_SL = Toks.T_SL,
		T_INSTANCEOF = Toks.T_INSTANCEOF,

		T_UNSET_CAST = Toks.T_UNSET_CAST,
		T_BOOL_CAST = Toks.T_BOOL_CAST,
		T_OBJECT_CAST = Toks.T_OBJECT_CAST,
		T_ARRAY_CAST = Toks.T_ARRAY_CAST,
		T_STRING_CAST = Toks.T_STRING_CAST,
        T_BINARY_CAST = Toks.T_BINARY_CAST,
        T_DOUBLE_CAST = Toks.T_DOUBLE_CAST,

		T_INT_CAST = Toks.T_INT32_CAST,
		T_DEC = Toks.T_DEC,
		T_INC = Toks.T_INC,
		T_CLONE = Toks.T_CLONE,
		T_NEW = Toks.T_NEW,
		T_EXIT = Toks.T_EXIT,
		T_IF = Toks.T_IF,
		T_ELSEIF = Toks.T_ELSEIF,
		T_ELSE = Toks.T_ELSE,
		T_ENDIF = Toks.T_ENDIF,
		T_LNUMBER = Toks.T_LNUMBER,
		T_L64NUMBER = Toks.T_L64NUMBER, 
		T_DNUMBER = Toks.T_DNUMBER,
		T_STRING = Toks.T_STRING,
		T_STRING_VARNAME = Toks.T_STRING_VARNAME,
		T_VARIABLE = Toks.T_VARIABLE,
		T_NUM_STRING = Toks.T_NUM_STRING,
		T_INLINE_HTML = Toks.T_INLINE_HTML,
		T_CHARACTER = Toks.T_CHARACTER,
		T_BAD_CHARACTER = Toks.T_BAD_CHARACTER,
		T_ENCAPSED_AND_WHITESPACE = Toks.T_ENCAPSED_AND_WHITESPACE,
		T_CONSTANT_ENCAPSED_STRING = Toks.T_CONSTANT_ENCAPSED_STRING,
		T_ECHO = Toks.T_ECHO,
		T_DO = Toks.T_DO,
		T_WHILE = Toks.T_WHILE,
		T_ENDWHILE = Toks.T_ENDWHILE,
		T_FOR = Toks.T_FOR,
		T_ENDFOR = Toks.T_ENDFOR,
		T_FOREACH = Toks.T_FOREACH,
		T_ENDFOREACH = Toks.T_ENDFOREACH,
		T_AS = Toks.T_AS,
		T_SWITCH = Toks.T_SWITCH,
		T_ENDSWITCH = Toks.T_ENDSWITCH,
		T_CASE = Toks.T_CASE,
		T_DEFAULT = Toks.T_DEFAULT,
		T_BREAK = Toks.T_BREAK,
		T_CONTINUE = Toks.T_CONTINUE,
		T_FUNCTION = Toks.T_FUNCTION,
		T_CONST = Toks.T_CONST,
		T_RETURN = Toks.T_RETURN,
		T_TRY = Toks.T_TRY,
		T_CATCH = Toks.T_CATCH,
		T_THROW = Toks.T_THROW,
		T_GLOBAL = Toks.T_GLOBAL,
		T_PUBLIC = Toks.T_PUBLIC,
		T_PROTECTED = Toks.T_PROTECTED,
		T_PRIVATE = Toks.T_PRIVATE,
		T_FINAL = Toks.T_FINAL,
		T_ABSTRACT = Toks.T_ABSTRACT,
		T_STATIC = Toks.T_STATIC,
		T_VAR = Toks.T_VAR,
		T_UNSET = Toks.T_UNSET,
		T_ISSET = Toks.T_ISSET,
		T_EMPTY = Toks.T_EMPTY,
		T_CLASS = Toks.T_CLASS,
        T_TRAIT = Toks.T_TRAIT,
		T_INTERFACE = Toks.T_INTERFACE,
        T_INSTEADOF = Toks.T_INSTEADOF,
		T_EXTENDS = Toks.T_EXTENDS,
		T_IMPLEMENTS = Toks.T_IMPLEMENTS,
		T_OBJECT_OPERATOR = Toks.T_OBJECT_OPERATOR,
		T_DOUBLE_ARROW = Toks.T_DOUBLE_ARROW,
		T_LIST = Toks.T_LIST,
		T_ARRAY = Toks.T_ARRAY,
        T_CALLABLE = Toks.T_CALLABLE,
		T_CLASS_C = Toks.T_CLASS_C,
        T_TRAIT_C = Toks.T_TRAIT_C,
		T_METHOD_C = Toks.T_METHOD_C,
		T_FUNC_C = Toks.T_FUNC_C,
		T_LINE = Toks.T_LINE,
		T_FILE = Toks.T_FILE,
        T_DIR = Toks.T_DIR,
		T_COMMENT = Toks.T_COMMENT,
		T_DOC_COMMENT = Toks.T_DOC_COMMENT,
		T_OPEN_TAG = Toks.T_OPEN_TAG,
		T_OPEN_TAG_WITH_ECHO = Toks.T_OPEN_TAG_WITH_ECHO,
		T_CLOSE_TAG = Toks.T_CLOSE_TAG,
		T_WHITESPACE = Toks.T_WHITESPACE,
		T_START_HEREDOC = Toks.T_START_HEREDOC,
		T_END_HEREDOC = Toks.T_END_HEREDOC,
		T_DOLLAR_OPEN_CURLY_BRACES = Toks.T_DOLLAR_OPEN_CURLY_BRACES,
		T_CURLY_OPEN = Toks.T_CURLY_OPEN,
		T_DOUBLE_COLON = Toks.T_DOUBLE_COLON,
	}

	//public enum Tokens
	//{
	//  ERROR = -1,
	//  EOF = Toks.EOF,

	//  T_GET = -3,
	//  T_SET = -4,
	//  T_CALL = -5,
	//  T_TOSTRING = -6,
	//  T_CONSTRUCT = -7,
	//  T_DESTRUCT = -8,
	//  T_PARENT = -9,
	//  T_SELF = -10,
	//  T_AUTOLOAD = -11,
	//  T_INT_TYPE = -12,
	//  T_STRING_TYPE = -13,
	//  T_BOOL_TYPE = -14,
	//  T_DOUBLE_TYPE = -15,
	//  T_ASSERT = -16,
	//  T_STRING_E = -17,
	//  T_LINE_COMMENT = -18,
	//  T_TRUE = -19,
	//  T_FALSE = -20,
	//  T_WAKEUP = -21,
	//  T_SLEEP = -22,

	//  // commands:
	//  ParseDecimalNumber = -25,
	//  ParseHexadecimalNumber = -26,
	//  ParseDouble = -27,
	//  DoubleQuotedString = -28,
	//  SingleQuotedString = -29,
	//  OctalCharCode = -30,
	//  HexCharCode = -31,
	//  UnicodeCharCode = -32,
	//  UnicodeCharName = -33,
	//  EscapedCharacter = -34,


	//  T_LINQ_SELECT = 2,
	//  T_LINQ_BY = 3,
	//  T_LINQ_WHERE = 4,
	//  T_LINQ_DESCENDING = 5,
	//  T_LINQ_ASCENDING = 6,
	//  T_LINQ_ORDERBY = 7,
	//  T_LINQ_GROUP = 8,
	//  T_LINQ_FROM = 9,
	//  T_LINQ_IN = 10,

	//  T_LGENERIC = 11,
	//  T_RGENERIC = 12,

	//  // todo: match token numbers against PHP
	//  T_GOTO = 13,
	//  T_IMPORT = 14,
	//  T_NAMESPACE = 15,
	//  T_NAMESPACE_C = 16,
	//  T_NAMESPACE_NAME = 17,
	//  T_BINARY_DOUBLE = 18,
	//  T_BINARY_HEREDOC = 19,
	//  T_UNICODE_CAST = 20,

	//  T_LPAREN = (int)'(',
	//  T_RPAREN = (int)')',
	//  T_LBRACKET = (int)'[',
	//  T_RBRACKET = (int)']',
	//  T_LBRACE = (int)'{',
	//  T_RBRACE = (int)'}',
	//  T_SEMI = (int)';',
	//  T_COLON = (int)':',
	//  T_COMMA = (int)',',
	//  T_DOT = (int)'.',
	//  T_PIPE = (int)'|',
	//  T_CARET = (int)'^',
	//  T_AMP = (int)'&',
	//  T_PLUS = (int)'+',
	//  T_MINUS = (int)'-',
	//  T_SLASH = (int)'/',
	//  T_MUL = (int)'*',
	//  T_EQ = (int)'=',
	//  T_PERCENT = (int)'%',
	//  T_EXCLAM = (int)'!',
	//  T_TILDE = (int)'~',
	//  T_DOLLAR = (int)'$',
	//  T_LT = (int)'<',
	//  T_GT = (int)'>',
	//  T_QUESTION = (int)'?',
	//  T_AT = (int)'@',
	//  T_DOUBLE_QUOTES = (int)'"',
	//  T_BACKQUOTE = (int)'`',

	//  T_REQUIRE_ONCE = 258,
	//  T_REQUIRE = 259,
	//  T_EVAL = 260,
	//  T_INCLUDE_ONCE = 261,
	//  T_INCLUDE = 262,
	//  T_LOGICAL_OR = 263,
	//  T_LOGICAL_XOR = 264,
	//  T_LOGICAL_AND = 265,
	//  T_PRINT = 266,
	//  T_SR_EQUAL = 267,
	//  T_SL_EQUAL = 268,
	//  T_XOR_EQUAL = 269,
	//  T_OR_EQUAL = 270,
	//  T_AND_EQUAL = 271,
	//  T_MOD_EQUAL = 272,
	//  T_CONCAT_EQUAL = 273,
	//  T_DIV_EQUAL = 274,
	//  T_MUL_EQUAL = 275,
	//  T_MINUS_EQUAL = 276,
	//  T_PLUS_EQUAL = 277,
	//  T_BOOLEAN_OR = 278,
	//  T_BOOLEAN_AND = 279,
	//  T_IS_NOT_IDENTICAL = 280,
	//  T_IS_IDENTICAL = 281,
	//  T_IS_NOT_EQUAL = 282,
	//  T_IS_EQUAL = 283,
	//  T_IS_GREATER_OR_EQUAL = 284,
	//  T_IS_SMALLER_OR_EQUAL = 285,
	//  T_SR = 286,
	//  T_SL = 287,
	//  T_INSTANCEOF = 288,
	//  T_UNSET_CAST = 289,
	//  T_BOOL_CAST = 290,
	//  T_OBJECT_CAST = 291,
	//  T_ARRAY_CAST = 292,
	//  T_STRING_CAST = 293,
	//  T_DOUBLE_CAST = 294,
	//  T_INT_CAST = 295,
	//  T_DEC = 296,
	//  T_INC = 297,
	//  T_CLONE = 298,
	//  T_NEW = 299,
	//  T_EXIT = 300,
	//  T_IF = 301,
	//  T_ELSEIF = 302,
	//  T_ELSE = 303,
	//  T_ENDIF = 304,
	//  T_LNUMBER = 305,
	//  T_DNUMBER = 306,
	//  T_STRING = 307,
	//  T_STRING_VARNAME = 308,
	//  T_VARIABLE = 309,
	//  T_NUM_STRING = 310,
	//  T_INLINE_HTML = 311,
	//  T_CHARACTER = 312,
	//  T_BAD_CHARACTER = 313,
	//  T_ENCAPSED_AND_WHITESPACE = 314,
	//  T_CONSTANT_ENCAPSED_STRING = 315,
	//  T_ECHO = 316,
	//  T_DO = 317,
	//  T_WHILE = 318,
	//  T_ENDWHILE = 319,
	//  T_FOR = 320,
	//  T_ENDFOR = 321,
	//  T_FOREACH = 322,
	//  T_ENDFOREACH = 323,
	//  T_DECLARE = 324,
	//  T_ENDDECLARE = 325,
	//  T_AS = 326,
	//  T_SWITCH = 327,
	//  T_ENDSWITCH = 328,
	//  T_CASE = 329,
	//  T_DEFAULT = 330,
	//  T_BREAK = 331,
	//  T_CONTINUE = 332,
	//  T_FUNCTION = 333,
	//  T_CONST = 334,
	//  T_RETURN = 335,
	//  T_TRY = 336,
	//  T_CATCH = 337,
	//  T_THROW = 338,
	//  T_USE = 339,
	//  T_GLOBAL = 340,
	//  T_PUBLIC = 341,
	//  T_PROTECTED = 342,
	//  T_PRIVATE = 343,
	//  T_FINAL = 344,
	//  T_ABSTRACT = 345,
	//  T_STATIC = 346,
	//  T_VAR = 347,
	//  T_UNSET = 348,
	//  T_ISSET = 349,
	//  T_EMPTY = 350,
	//  T_CLASS = 352,
	//  T_INTERFACE = 353,
	//  T_EXTENDS = 354,
	//  T_IMPLEMENTS = 355,
	//  T_OBJECT_OPERATOR = 356,
	//  T_DOUBLE_ARROW = 357,
	//  T_LIST = 358,
	//  T_ARRAY = 359,
	//  T_CLASS_C = 360,
	//  T_METHOD_C = 361,
	//  T_FUNC_C = 362,
	//  T_LINE = 363,
	//  T_FILE = 364,
	//  T_COMMENT = 365,
	//  T_DOC_COMMENT = 366,
	//  T_OPEN_TAG = 367,
	//  T_OPEN_TAG_WITH_ECHO = 368,
	//  T_CLOSE_TAG = 369,
	//  T_WHITESPACE = 370,
	//  T_START_HEREDOC = 371,
	//  T_END_HEREDOC = 372,
	//  T_DOLLAR_OPEN_CURLY_BRACES = 373,
	//  T_CURLY_OPEN = 374,
	//  T_DOUBLE_COLON = 375
	//}
}