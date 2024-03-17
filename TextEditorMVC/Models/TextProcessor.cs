using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC
{
    internal static class TextProcessor
    {
        public static Tuple<int, int> GetPosition(string text, int position)
        {
            int lineNumber = 1;
            int charPosition = 1; // Позиция символа в строке (начинается с 1)

            // Найти номер строки и позицию в строке
            for (int i = 0; i < position && i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    lineNumber++;
                    charPosition = 1; // Сбросить позицию, так как мы переходим на новую строку
                }
                else
                {
                    charPosition++;
                }
            }

            return new Tuple<int, int>(charPosition, lineNumber);
        }

		public static string CreateTextFromTokens(List<Token> tokens)
		{
			string result = String.Empty;

			foreach (Token info in tokens)
			{
				if (info.Text == "\\n")
				{
					result += '\n';
				}
				else
				{
					result += info.Text;
				}
			}

			return result;
		}
	}
}
