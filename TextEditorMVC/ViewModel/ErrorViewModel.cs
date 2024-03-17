using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC
{
    internal class ErrorViewModel
    {
        public int Line { get; set; }
        public int Symbol { get; set; }
        public string Message { get; set; }
    }
}
