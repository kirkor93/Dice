using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [RequireComponent(typeof(Text))]
    public class Localizer : MonoBehaviour
    {
        public string Key;

        private Text _textField;

        public void Localize()
        {
            if (string.IsNullOrEmpty(Key))
            {
                Debug.LogError("No key in Localizer component!");
                return;
            }

            if (_textField == null)
            {
                _textField = GetComponent<Text>();
                if (_textField == null)
                {
                    Debug.LogError("Localizer can only work on objects with Text component!");
                    return;
                }
            }

            _textField.text = LocalizationManager.GetTranslation(Key);
        }

    }
}