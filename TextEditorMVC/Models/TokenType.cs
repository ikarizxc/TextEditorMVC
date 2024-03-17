using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TextEditorMVC
{
    internal class TokenType
    {
        string name;
        int code;
        string regex;

        public string Regex
        {
            get { return regex; }
        }
        public int Code
        {
            get { return code; }
        }
        public string Name
        {
            get { return name; }
        }

        public TokenType(string name, int code, string regex)
        {
            this.name = name;
            this.code = code;
            this.regex = regex;
        }
    }

    static internal class TokenTypeList
    {
        public static List<TokenType> list = new List<TokenType>()
        {
            { new TokenType("DEF", 0, "") },
            { new TokenType("ключевое слово - 'const'", 1, "const") },
            { new TokenType("ключевое слово - 'f32'", 2, "f32") },
            { new TokenType("идентификатор", 3, "[a-zA-Z_][a-zA-Z0-9_]*") },
            { new TokenType("вещественное число", 4, "[+-]?\\d*\\.?\\d+") },
            { new TokenType("оператор принадлежности к типу - ':'", 5, ":") },
            { new TokenType("оператор присваивания - '='", 6, "=") },
            { new TokenType("пробел", 7, "[ \\t]") },
            { new TokenType("конец оператора - ';'", 8, "[;]") },
            { new TokenType("перенос на новую строку", 9, "[\\r]\\n") },
        };  
    }
}
