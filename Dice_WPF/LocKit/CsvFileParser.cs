using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace LocKit
{
    public class CsvFileParser : IFileParser
    {
        private char[] _splitCharacters;
        private ParsedDictionary _parsedDictionary;

        public Encoding FileEncoding { get; set; }

        public ParsedDictionary ResultDictionary
        {
            get { return _parsedDictionary; }
        }

        public CsvFileParser(char[] splitCharacters, Encoding encoding)
        {
            _splitCharacters = splitCharacters.Clone() as char[];
            FileEncoding = encoding;
        }

        public CsvFileParser(char[] splitCharacters)
            : this(splitCharacters, Encoding.UTF8)
        {
        }

        public void ParseFile(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream, FileEncoding))
                {
                    string allData = reader.ReadToEnd();
                    string[] lines = allData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length <= 1)
                    {
                        throw new NotSupportedException();
                    }
                    string[] line = lines[0].Split(_splitCharacters, StringSplitOptions.None);
                    int languagesCount = line.Length - 1;
                    _parsedDictionary = new ParsedDictionary(languagesCount);
                    for (int i = 1; i < line.Length; i++)
                    {
                        _parsedDictionary.AvailableLanguages[i - 1] = line[i];
                    }

                    for (int i = 1; i < lines.Length; i++)
                    {
                        line = lines[i].Split(_splitCharacters, StringSplitOptions.None);
                        string key = line[0];
                        string[] translations = new string[languagesCount];
                        Array.Copy(line, 1, translations, 0, languagesCount);
                        _parsedDictionary.SetTranslation(key, translations);
                    }
                }
            }
        }
    }
}
