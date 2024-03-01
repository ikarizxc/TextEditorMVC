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
            { new TokenType("CONST", 0, "const") },
            { new TokenType("FLOAT32", 1, "f32") },
            { new TokenType("IDENTIFICATOR", 2, "[a-zA-Z][a-zA-Z]*") },
            { new TokenType("NUMBER", 3, "[+-]?\\d*\\.?\\d+") },
            { new TokenType("COLON", 4, ":") },
            { new TokenType("ASSIGN OPERATOR", 5, "=") },
            { new TokenType("SPACE", 6, "[ \\t]") },
            { new TokenType("END OF OPERATOR", 7, "[;]") },
            { new TokenType("NEW LINE", 8, "[\\r]\\n") },
        };  
    }
}
