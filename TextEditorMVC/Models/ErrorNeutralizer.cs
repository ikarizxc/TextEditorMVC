using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextEditorMVC.Models
{
	internal class ErrorNeutralizer
	{
		List<Token> tokens;

		TokenType prevTokenType = TokenTypeList.list[0];
		int i = 0;

		public ErrorNeutralizer(List<Token> tokens)
		{
			this.tokens = tokens;
		}

		private void GoNext(List<TokenType> requiredTypes)
		{
			if (requiredTypes[0] == TokenTypeList.list[7])
			{
				if (tokens[i].TokenType == requiredTypes[0])
				{
					i++;
				}

				while (i < tokens.Count && tokens[i].TokenType == requiredTypes[0])
				{
					tokens.RemoveAt(i);
				}

				for (int j = 1; j < requiredTypes.Count; j++)
				{
					if (tokens[i].TokenType == requiredTypes[j])
					{
						prevTokenType = tokens[i].TokenType;
						i++;
						return;
					}
				}
				tokens.RemoveAt(i);
			}
			else
			{
				if (tokens[i].TokenType != requiredTypes[0])
				{
					tokens.RemoveAt(i);
				}
				else
				{
					prevTokenType = tokens[i].TokenType;
					i++;
				}
			}
		}

		public List<Token> NeutralizingErrors()
		{
			while (i < tokens.Count)
			{
				if (prevTokenType == TokenTypeList.list[0])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[1]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[1])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[3]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[2])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[6]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[3])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[5]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[4])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[8]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[5])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[2]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[6])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[4]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[8])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[1],
						TokenTypeList.list[9]
					};

					GoNext(requiredTypes);
				}
				else if (prevTokenType == TokenTypeList.list[9])
				{
					List<TokenType> requiredTypes = new List<TokenType>()
					{
						TokenTypeList.list[7],
						TokenTypeList.list[1],
						TokenTypeList.list[9]
					};

					GoNext(requiredTypes);
				}
			}
			return tokens;
		}
	}
}