using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TextEditorMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextEditorMVC
{
    internal class LanguageController
    {
        Lexer lexer = new Lexer();
        Parser parser;

        public bool Analyze(string code)
        {
            if (code != null)
            {
                if (lexer.LexicalAnalysis(code))
                {
                    parser = new Parser(lexer.Tokens);

                    if (parser.Parse())
                    {
                        return true;
                    }
                    else
                    { 
                        return false; 
                    }
                }
                else
                {
                    return false;
                }
			}
			return false;
		}

        public ObservableCollection<TokenViewModel> GetLexemes()
        {
            if (lexer.Tokens.Count > 0)
            {
                ObservableCollection<TokenViewModel> lexemes = new ObservableCollection<TokenViewModel>();

                foreach (Token token in lexer.Tokens)
                {
                    Tuple<int, int> pos = TextProcessor.GetPosition(lexer.Code, token.Position);

                    TokenViewModel item = new TokenViewModel()
                    {
                        Line = pos.Item2,
                        StartSymbol = pos.Item1,
                        EndSymbol = pos.Item1 + token.Length - 1,
                        Code = token.TokenType.Code,
                        TokenType = token.TokenType.Name,
                        Token = token.Text,
                    };

                    lexemes.Add(item);
                }

                return lexemes;
            }
            return new ObservableCollection<TokenViewModel>();
        }

        public ObservableCollection<ErrorViewModel> GetErrors()
        {
			ObservableCollection<ErrorViewModel> errors = new ObservableCollection<ErrorViewModel>();

			if (lexer.Errors.Count > 0)
            { 
                foreach (Error error in lexer.Errors)
                {
                    Tuple<int, int> pos = TextProcessor.GetPosition(lexer.Code, error.Position);

                    ErrorViewModel item = new ErrorViewModel()
                    {
                        Line = pos.Item2,
                        Symbol = pos.Item1,
                        Message = error.Message,
                    };

					errors.Add(item);
				}
            }

            if (parser != null && parser.Errors.Count > 0)
            {
				foreach (Error error in parser.Errors)
				{
					Tuple<int, int> pos = TextProcessor.GetPosition(lexer.Code, error.Position);

					ErrorViewModel item = new ErrorViewModel()
					{
						Line = pos.Item2,
						Symbol = pos.Item1,
						Message = error.Message,
					};

					errors.Add(item);
				}
			}
				
            if (errors.Count > 0)
            {
                return errors;
            }
            return new ObservableCollection<ErrorViewModel>();
        }
    }
}
