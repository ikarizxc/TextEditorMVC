using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorMVC
{
    internal class File
    {
        string fileName = "";
        string path = "";
        string content = "";

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
            }
        }

        UndoRedoManager undoRedoManager = new UndoRedoManager();
        public UndoRedoManager UndoRedoManager { get { return undoRedoManager; } }


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            File other = (File)obj;
            return Path.Equals(other.Path);
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public bool CreateFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Выберите местоположение и введите имя файла";
            saveFileDialog.DefaultExt = ".txt";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;
                this.path = filePath;
                this.fileName = path.Split("\\").Last();

                this.SaveFile();

                undoRedoManager.SaveState(content);

                return true;
            }

            return false;
        }

        public bool OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string fileContent = System.IO.File.ReadAllText(filePath);

                    this.path = filePath;
                    this.fileName = filePath.Split("\\").Last();
                    this.content = fileContent;

                    undoRedoManager.SaveState(content);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public void OpenFile(string filePath)
        {
            this.path = filePath;
            this.fileName = filePath.Split("\\").Last();
            this.Content = System.IO.File.ReadAllText(filePath);
        }

        public void SaveFile()
        {
            if (path != null)
            {
                System.IO.File.WriteAllText(path, content);
            }
        }

        public void SaveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                this.path = filePath;
                this.fileName = path.Split("\\").Last();

                if (this.content != null)
                {
                    System.IO.File.WriteAllText(filePath, this.content);
                }
                else
                {
                    System.IO.File.WriteAllText(filePath, "");
                }
            }
        }
    }
}
