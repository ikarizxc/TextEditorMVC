using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC
{
    internal class OutputToken
    {
        public int Line { get; set; }
        public int StartSymbol { get; set; }
        public int EndSymbol { get; set; }
        public int Code { get; set; }
        public string TokenType { get; set; }
        public string Token { get; set; }
    }
}
