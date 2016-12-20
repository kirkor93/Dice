using System;

namespace Dice.Model
{
    public class Die
    {
        private readonly int _sides = 6;
        private readonly Random _random;

        public int LastValue { get; private set; }

        public Die(int initialValue)
        {
            _random = new Random();
            if (initialValue < 1 || initialValue > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(initialValue));
            }
            LastValue = initialValue;
        }

        public int Roll()
        {
            LastValue = _random.Next(1, _sides + 1);
            return LastValue;
        }
    }
}