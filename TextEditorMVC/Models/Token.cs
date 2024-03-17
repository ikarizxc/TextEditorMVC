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
		int length;

		public int Position
		{
			get { return position; }
		}
		public TokenType TokenType
		{
			set { type = value; }
			get { return type; }
		}
		public string Text
		{
			get { return text; }
		}
		public int Length
		{
			get { return length; }
		}

		public Token(TokenType type, string text, int position, int length)
		{
			this.type = type;
			this.text = text;
			this.position = position;
			this.length = length;
		}
	}
}
