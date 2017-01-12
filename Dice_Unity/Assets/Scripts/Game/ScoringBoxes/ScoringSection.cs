using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScoringBoxes
{
    [Serializable]
    public struct ScoreBonus
    {
        public int ScoreRequired;
        public int BonusPoints;
    }

    public class ScoringSection : MonoBehaviour
    {
        [SerializeField]
        private ScoreBonus[] _bonuses;

        [SerializeField]
        private ScoringBox.BoxType[] _boxesToSpawn;
        [SerializeField]
        private GameObject _boxPrefab;

        private List<ScoringBox> _assignedBoxes = new List<ScoringBox>();
        private bool _initialized = false;

        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            //spawning boxes for points
            RectTransform rect = GetComponent<RectTransform>();
            float dy = rect.rect.height / (_boxesToSpawn.Length + 1);
            for (int i = 0; i < _boxesToSpawn.Length; i++)
            {
                GameObject spawnedBox = Instantiate(_boxPrefab);
                RectTransform sBoxTransform = spawnedBox.GetComponent<RectTransform>();
                sBoxTransform.SetParent(rect, false);
                sBoxTransform.anchoredPosition = new Vector2(sBoxTransform.anchoredPosition.x, sBoxTransform.anchoredPosition.y - i * dy);

                ScoringBox sBoxScoringBox = spawnedBox.GetComponent<ScoringBox>();
                sBoxScoringBox.Type = _boxesToSpawn[i];

                _assignedBoxes.Add(sBoxScoringBox);
            }

            _initialized = true;
        }

        private void Awake()
        {
            Initialize();
        }
    }
}


