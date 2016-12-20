using System;

namespace LocKit
{
    public class Translator
    {
        private ParsedDictionary _parsedDictionary;
        private int _currentLanguage;

        public int LanguageIndex
        {
            get { return _currentLanguage; }
            set
            {
                if (value >= 0 && value < _parsedDictionary.AvailableLanguages.Length)
                {
                    _currentLanguage = value;
                }
            }
        }

        public string Language
        {
            get { return _parsedDictionary.AvailableLanguages[_currentLanguage]; }
            set
            {
                int index = Array.IndexOf(_parsedDictionary.AvailableLanguages, value);
                if (index >= 0)
                {
                    _currentLanguage = index;
                }
            }
        }

        public Translator(string filePath, IFileParser fileParser)
        {
            fileParser.ParseFile(filePath);
            _parsedDictionary = fileParser.ResultDictionary;
            if (_parsedDictionary.AvailableLanguages.Length > 0)
            {
                _currentLanguage = 0;
            }
        }

        public string GetTranslation(string key)
        {
            return _parsedDictionary.GetTranslation(key, _currentLanguage);
        }

        public bool HasKey(string key)
        {
            return _parsedDictionary.HasKey(key);
        }
    }
}
