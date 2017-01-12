using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct DieSide
    {
        public int Value;
        public Vector3 Direction;
    }

    public class Die : MonoBehaviour
    {
        [SerializeField]
        private DieSide[] _sides;

        public int Value { get { return GetUpFacingSide().Value; } }
        public int MaxValue { get { return _sides.Max(s => s.Value); } }
        public int MinValue { get { return _sides.Min(s => s.Value); } }
        public int Size { get { return _sides.Length; } }

        private DieSide GetUpFacingSide()
        {
            Vector3 up = transform.up;
            float smallestAngle = Vector3.Angle(_sides.FirstOrDefault().Direction, up);
            int smallestIndex = 0;
            for (int i = 1; i < _sides.Length; i++)
            {
                float angle = Vector3.Angle(_sides[i].Direction, up);
                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    smallestIndex = i;
                }
            }
            
            Debug.Log(string.Format("Value: {0}, angle: {1}", _sides[smallestIndex].Value, smallestAngle));
            return _sides[smallestIndex];
        }
    }
}