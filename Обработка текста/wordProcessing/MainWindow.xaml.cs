using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace wordProcessing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WordProcess wordProcess = new WordProcess();// подключение класса обработки текста
        Files files = new Files();// подключение класса работы с файлами

        private List<string> texts = new List<string>(); // Список документов

        public MainWindow()
        {
            InitializeComponent();

            //Обработка группы ввода-вывода
            openFileBtn.Click += async (s, e) => await openFile();
            saveTextBtn.Click += async (s, e) => await saveFile();
            lengthTb.PreviewTextInput += (s, e) =>  e.Handled = !char.IsDigit(e.Text, 0);
            charactersTb.PreviewTextInput += (s, e) =>
            {
                if ((e.Text == "?") || (e.Text == "!") || (e.Text == ":") || (e.Text == ";")
                    || (e.Text == "...") || (e.Text == "-") || (e.Text == "'") || (e.Text == ",") || (e.Text == ".") && (e.Text.Length == 1))
                {
                    e.Handled = false;
                }
                else e.Handled = true;
            };
        }

        //Открытие файла
        private async Task openFile() 
        {  
            var res = await files.openFileDialogAsync();

            foreach (var item in res)
            {
                if (item != null)
                {
                    texts.Add(item);
                    statusLable.Content = "Статус: файлы успешно открыты";
                    saveTextBtn.IsEnabled = true;
                }
                else statusLable.Content = "Статус: возникла ошибка при обработке файла";
            }            
        }

        //Сохранение файла
        private async Task saveFile() 
        {
            List<string> newList = new List<string>();

            if ((bool)isSelectLengthTB.IsChecked || (bool)isCharactersTB.IsChecked)
            {
                foreach (var item in texts)
                {
                    newList.Add(item);
                }                
                texts.Clear();

                if ((bool)isSelectLengthTB.IsChecked)
                { 
                    if (lengthTb != null)
                    {
                        int length = Convert.ToInt32(lengthTb.Text);

                        foreach (var item in newList)
                        {
                            string res = await Task.Run(() => wordProcess.deletingWords(item, length));
                            if (res != null)
                            {
                                texts.Add(res);
                                statusLable.Content = "Статус: Удаление слов длинной больше " + length + " выполнено";
                            }
                            else statusLable.Content = "Статус: возникла ошибка";
                        }
                    }
                }               

                if ((bool)isCharactersTB.IsChecked)
                {
                    newList.Clear();
                    foreach (var item in texts)
                    {
                        newList.Add(item);
                    }
                    texts.Clear();

                    if (charactersTb != null)
                    {
                        string characters = charactersTb.Text;

                        foreach (var item in newList)
                        {
                            string res = await Task.Run(() => wordProcess.deletingCharacters(item, characters));

                            if (res != null)
                            {
                                texts.Add(res);
                                statusLable.Content = "Статус: Символ " + characters + " удален из текста";
                            }
                            else statusLable.Content = "Статус: возникла ошибка";
                        }                        
                    }
                    else statusLable.Content = "Статус: возникла ошибка";
                }
            }
            

            foreach (var item in texts)
            {
                var result = await files.saveFileDialogAsync(item);

                if (result != null)
                {
                    statusLable.Content = "Статус:" + result;
                }
                else statusLable.Content = "Статус: возникла ошибка при обработке файла";
            }           
            
            saveTextBtn.IsEnabled = false;
            newList.Clear();
            texts.Clear();
        }
    }
}
