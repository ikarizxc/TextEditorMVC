using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TextEditorMVC
{
    internal class LexerController
    {
        Lexer lexer = new Lexer();

        public Lexer Lexer { get { return lexer; } }

        public bool LexicalAnalysis(string code)
        {
            if (code != null)
            {
                return lexer.LexicalAnalysis(code);
            }
            return false;
        }

        public ObservableCollection<OutputToken> GetLexemes()
        {
            if (lexer.TokenList.Count > 0)
            {
                ObservableCollection<OutputToken> lexemes = new ObservableCollection<OutputToken>();

                foreach (Token token in lexer.TokenList)
                {
                    Tuple<int, int> pos = TextProcessor.GetPosition(lexer.Code, token.Position);

                    OutputToken item = new OutputToken()
                    {
                        Line = pos.Item2,
                        Symbol = pos.Item1,
                        Code = token.TokenType.Code,
                        TokenType = token.TokenType.Name,
                        Token = token.Text,
                    };

                    lexemes.Add(item);
                }

                return lexemes;
            }
            return new ObservableCollection<OutputToken>();
        }

        public ObservableCollection<OutputError> GetErrors()
        {
            if (lexer.Error != null)
            {
                Tuple<int, int> pos = TextProcessor.GetPosition(lexer.Code, lexer.Error.Position);

                OutputError outputError = new OutputError()
                {
                    Line = pos.Item2,
                    Symbol = pos.Item1,
                    Message = lexer.Error.Message
                };

                return new ObservableCollection<OutputError>() { outputError };
            }
            return new ObservableCollection<OutputError>();
        }
    }
}
