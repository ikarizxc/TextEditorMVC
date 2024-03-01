using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace TextEditorMVC
{
    internal class UndoRedoManager
    {
        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();
        int maxUndoStackElements = 15;

        public void SaveState(string text)
        {
            if (undoStack.Count == 0)
            {
                undoStack.Push(text);
                redoStack.Clear();
            }
            else if (Math.Abs(text.Length - undoStack.First().Length) < 5)
            {
                return;
            }

            if (undoStack.Count > maxUndoStackElements)
            {
                undoStack.Pop();
            }

            undoStack.Push(text);
            redoStack.Clear();
        }

        public string? Undo()
        {
            if (undoStack.Count > 1)
            {
                redoStack.Push(undoStack.Pop());
                return ApplyState();
            }
            return null;
        }

        public string? Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(redoStack.Pop());
                return ApplyState();
            }
            return null;
        }

        private string? ApplyState()
        {
            if (undoStack.Count > 0)
            {
                return undoStack.Peek();
            }
            return null;
        }
    }
}
