using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TextEditorMVC;

namespace TextEditorMVC
{
    internal class Lexer
    {
        string code = String.Empty;
        int position = 0;
        List<Token> tokens = new List<Token>();
        List<Error> errors = new List<Error>();

        public string Code { get { return code; } }
        public List<Token> Tokens { get { return tokens; } }
        public List<Error> Errors { get { return errors; } }

        private void ResetAll()
        {
            this.position = 0;
            tokens = new List<Token>();
            errors = new List<Error>();
        }

        public bool LexicalAnalysis(string code)
        {
            this.code = code;
            ResetAll();

            while (this.NextToken())
            {

            }
            
            if (errors.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NextToken()
        {
            if (this.position >= this.code.Length)
            {
                return false;
            }

            for (int i = 1; i < TokenTypeList.list.Count; i++)
            {
                var tokenType = TokenTypeList.list[i];
                var regex = new Regex("^" + tokenType.Regex);
                var currentCode = this.code.Substring(this.position);
                if (regex.IsMatch(currentCode))
                {
                    var tokenText = regex.Match(currentCode).Value;

                    if (tokenText == "\n" || tokenText == "\r\n")
                    {
                        tokenText = "\\n";
                    }

                    var token = new Token(tokenType, tokenText, this.position, tokenText.Length);
                    this.position += tokenText.Length;
                    this.tokens.Add(token);
                    return true;
                }
            }
            errors.Add(new(this.position, $"На позиции {this.position + 1} обнаружена ошибка"));
            this.position += 1;
            return true;
        }
    }
}
