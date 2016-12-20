using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LocKit;

namespace Localization
{
    public static class LocalizationManager
    {
        private static Translator _translator = null;

        public static void Initialize()
        {
            _translator = new Translator(Path.Combine(Application.dataPath, "Resources/Translations.csv"), new CsvFileParser(new []{';'}));
        }

        public static string GetTranslation(string key)
        {
            if (_translator == null)
            {
                Initialize();
            }
            return _translator.HasKey(key) ? _translator.GetTranslation(key) : "---KEY_NOT_FOUND---";
        }

        public static void SetLanguage(string language)
        {
            if (_translator == null)
            {
                Initialize();
            }
            _translator.Language = language;
        }

        public static void SetLanguage(int languageIndex)
        {
            if (_translator == null)
            {
                Initialize();
            }
            _translator.LanguageIndex = languageIndex;
        }
    }

}