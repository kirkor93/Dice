using System;
using System.Collections.Generic;

namespace LocKit
{
    public class ParsedDictionary
    {
        private string[] _languages;
        private Dictionary<string, string[]> _records;

        public string[] AvailableLanguages
        {
            get { return _languages; }
        }

        public ParsedDictionary(int languagesCount)
        {
            _records = new Dictionary<string, string[]>();
            _languages = new string[languagesCount];
        }

        public void SetTranslation(string key, string[] translations)
        {
            if (translations.Length != _languages.Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (HasKey(key))
            {
                string[] line = _records[key];
                for (int i = 0; i < line.Length; i++)
                {
                    line[i] = translations[i];
                }
            }
            else
            {
                _records.Add(key, translations);
            }
        }

        public bool HasKey(string key)
        {
            return _records.ContainsKey(key);
        }

        public string GetTranslation(string key, int language)
        {
            return _records[key][language];
        }
    }
}
