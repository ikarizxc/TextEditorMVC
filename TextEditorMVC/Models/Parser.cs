using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC.Models
{
	internal class Parser
	{
		List<Token> tokens;
		List<Error> errors = new();

		TokenType prevTokenType = TokenTypeList.list[0];
		Token currentToken;
		int pos = 0;

        public List<Error> Errors { get { return errors; } }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

		private void GoNext(List<TokenType> requiredTypes)
		{
			if (requiredTypes[0] == TokenTypeList.list[7])
			{
				while (currentToken.TokenType == requiredTypes[0])
				{
					pos++;

					if (pos >= tokens.Count)
					{
						return;
					}

					currentToken = tokens[pos];
				}

				for (int i = 1; i < requiredTypes.Count; i++)
				{
					if (currentToken.TokenType == requiredTypes[i])
					{
						pos++;
						prevTokenType = currentToken.TokenType;
						return;
					}
				}
				errors.Add(new Error(currentToken.Position, $"Ожидается {requiredTypes[1].Name}"));
				prevTokenType = requiredTypes[1];
			}
			else
			{
				if (currentToken.TokenType != requiredTypes[0])
				{
					errors.Add(new Error(currentToken.Position, $"Ожидается {requiredTypes[0].Name}"));
					prevTokenType = requiredTypes[0];
				}
				else
				{
					prevTokenType = currentToken.TokenType;
					pos++;
				}
			}
		}

		private void CheckEndOfCode()
		{
			while (prevTokenType != TokenTypeList.list[8])
			{
				if (prevTokenType == TokenTypeList.list[1])
				{
					var requireToken = TokenTypeList.list[3];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[2])
				{
					var requireToken = TokenTypeList.list[6];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[3])
				{
					var requireToken = TokenTypeList.list[5];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[4])
				{
					var requireToken = TokenTypeList.list[8];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[5])
				{
					var requireToken = TokenTypeList.list[2];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[6])
				{
					var requireToken = TokenTypeList.list[4];
					errors.Add(new Error(currentToken.Position + currentToken.Length, $"Ожидается {requireToken.Name}"));
					prevTokenType = requireToken;
				}
				else if (prevTokenType == TokenTypeList.list[9])
				{
					break;
				}
			}
		}

		public bool Parse()
		{
			errors = new();
			prevTokenType = TokenTypeList.list[0];

			while (pos < tokens.Count)
			{
				currentToken = tokens[pos];

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

			CheckEndOfCode();

			if (errors.Count != 0)
			{
				return false;
			}

			return true;
		}
    }
}
