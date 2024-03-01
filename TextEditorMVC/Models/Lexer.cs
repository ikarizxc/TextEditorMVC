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
        List<Token> tokenList = new List<Token>();
        List<Error> errorList = new List<Error>();

        public string Code { get { return code; } }
        public List<Token> TokenList { get { return tokenList; } }
        public List<Error> ErrorList { get { return errorList; } }

        private void ResetAll()
        {
            this.position = 0;
            tokenList = new List<Token>();
            errorList = new List<Error>();
        }

        public bool LexicalAnalysis(string code)
        {
            this.code = code;
            ResetAll();

            while (this.NextToken())
            {

            }
            
            if (errorList.Count == 0)
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

            for (int i = 0; i < TokenTypeList.list.Count; i++)
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
                    this.tokenList.Add(token);
                    return true;
                }
            }
            errorList.Add(new(this.position, $"На позиции {this.position} обнаружена ошибка"));
            this.position += 1;
            return true;
        }
    }
}
