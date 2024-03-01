using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC
{
    internal class Token
    {
        TokenType type;
        string text;
        int position;

        public int Position
        { 
            get { return position; }
        }
        public TokenType TokenType 
        { 
            get { return type; } 
        }
        public string Text
        {
            get { return text; }
        }

        public Token(TokenType type, string text, int position)
        {
            this.type = type;
            this.text = text;
            this.position = position;
        }
    }
}
