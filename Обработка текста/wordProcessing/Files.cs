using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace wordProcessing
{
    class Files
    {
        // диалог выбора файла
        public async Task<List<string>> openFileDialogAsync()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (*.txt)|*.txt";
            dialog.Multiselect = true;
            bool result = (bool)dialog.ShowDialog();
            if (result)
            {   
                List<string> res = new List<string>();
                foreach (string file in dialog.FileNames)
                {
                    res.Add(await Task.Run(() => loadFile(file)));
                }
                
               
                
                return res;
            }
            return null;
        }

        // загрузка файла
        private async Task<string> loadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, encoding: Encoding.UTF8))
                {
                    return await sr.ReadToEndAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        // диалог сохранения файла
        public async Task<string> saveFileDialogAsync(string text)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                string res = await Task.Run(() => saveFile(filename,text));

                return res;
            }
            return null;
        }

        // сохранение файла
        private async Task<string> saveFile(string path,string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, encoding: Encoding.UTF8))
                {
                    await sw.WriteLineAsync(text);
                    return "Сохранение прошло успешно";
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
