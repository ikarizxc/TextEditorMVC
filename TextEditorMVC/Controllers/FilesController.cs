using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TextEditorMVC
{
    internal class FilesController
    {
        ObservableCollection<File> files = new();
        File openedFile = new File();

        public ObservableCollection<File> Files
        {
            get { return files; }
        }
        public File OpenedFile
        {
            get { return openedFile; }
            set { openedFile = value; }
        }

        public bool CreateFile()
        {
            File tempFile = new File();
            if (tempFile.CreateFile())
            {
                files.Add(tempFile);
                openedFile = tempFile;

                return true;
            }

            return false;
        }

        public bool OpenFile()
        {
            File tempFile = new File();
            if (tempFile.OpenFile())
            {
                if (files.Contains(tempFile) != true)
                {
                    files.Add(tempFile);
                    openedFile = tempFile;

                    return true;
                }
            }

            return false;
        }

        public bool OpenFile(string path)
        {
            File tempFile = new File();
            tempFile.OpenFile(path);

            if (files.Contains(tempFile) != true)
            {
                files.Add(tempFile);
                openedFile = tempFile;

                return true;
            }

            return false;
        }

        public void SaveFile()
        {
            if (openedFile == null)
            {
                return;
            }

            openedFile.SaveFile();
        }

        public void SaveAsFile()
        {
            if (openedFile == null)
            {
                return;
            }

            openedFile.SaveAsFile();
        }

        public void CloseFile()
        {
            files.Remove(openedFile);
            if (files.Count == 0)
            {
                openedFile = new File();
            }
            else
            {
                openedFile = files[files.Count - 1];
            }
        }

        public void OpenedFileTextChanged(string text)
        {
            openedFile.Content = text;
            OpenedFile.UndoRedoManager.SaveState(text);
        }

        public void OpenedFileTextUndo()
        {
            string? text = OpenedFile.UndoRedoManager.Undo();
            if (text != null)
            {
                openedFile.Content = text;
            }
        }

        public void OpenedFileTextRedo()
        {
            string? text = OpenedFile.UndoRedoManager.Redo();
            if (text != null)
            {
                openedFile.Content = text;
            }
        }

        public bool IsContentChanged()
        {
            if (OpenedFile.Content == ReadContentFromPath(OpenedFile.Path))
            {
                return false;
            }
            return true;
        }

        public string ReadContentFromPath(string path)
        {
            return System.IO.File.ReadAllText(path);
        }
    }
}
