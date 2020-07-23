namespace wordProcessing
{
    class WordProcess
    {
        // удаление слов длиной менее какого-либо числа символов
        public string deletingWords(string text, int length)
        {
            if (text != null)
            {
                string[] textArr = text.Split(' ');

                text = null;

                for (int i = 0; i < textArr.Length; i++)
                {
                    if (textArr[i].Length < length)
                    {
                        textArr[i] = "";
                    }
                    text += " " + textArr[i];
                }

                return text;
            }
            return null;
        }

        // удаление знаков препинания
        public string deletingCharacters(string text, string characters)
        {
            return text.Replace(characters, string.Empty);
        }
    }
}
