namespace Dice.Model
{
    public class DiceSet
    {
        public enum DiceSetType
        {
            Ones,
            Twos,
            Threes,
            Fours,
            Fives,
            Sixes,
            Pair,
            TwoPairs,
            ThreeOfKind,
            FourOfKind,
            FiveOfKind,
            SixOfKind,
            FullHouse,
            EvenNumbers,
            OddNumbers,
            ThreePairs,
            TwoThreeOfKind,
            FourOfKindPlusPair,
            SmallStraight,
            BigStraight,
            OneOfEach,
            Minimum,
            Maximum,
            Chance,
            SmallPyramid,
            BigPyramid
        }

        private DiceSetType _type;


    }
}