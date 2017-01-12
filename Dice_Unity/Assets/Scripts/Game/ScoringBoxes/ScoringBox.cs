using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace Game.ScoringBoxes
{
    public class ScoringBox : MonoBehaviour
    {
        public enum BoxType
        {
            Aces,
            Twos,
            Threes,
            Fours,
            Fives,
            Sixes,

            Pair,
            ThreeOfKind,
            FourOfKind,
            FiveOfKind,
            SixOfKind,
            TwoPairs,
            FullHouse,
            EvenNumbers,
            OddNumbers,
            TwoTwoTwo,
            ThreeThree,
            FourTwo,
            SmallStraight,
            LargeStraight,
            AllDifferent,
            Min,
            Max,
            Chance,
            SmallPyramid,
            LargePyramid,
        }

        public BoxType Type;

        public bool ScoreSet { get; private set; }
        public int Score { get; private set; }

        public int CountScore(Die[] dice, bool firstThrow)
        {
            int score = 0;
            switch (Type)
            {
                    case BoxType.Aces:
                    case BoxType.Twos:
                    case BoxType.Threes:
                    case BoxType.Fours:
                    case BoxType.Fives:
                    case BoxType.Sixes:
                    {
                        //first throw has no effect here
                        firstThrow = false;
                        int typeNum = (int) Type - (int) BoxType.Aces + 1;
                        score = CountOccurences(dice, typeNum) * typeNum - 3 * typeNum;
                        break;
                    }
                    case BoxType.Pair:
                    case BoxType.ThreeOfKind:
                    {
                        //first throw has no effect here
                        firstThrow = false;
                        goto case BoxType.FourOfKind;
                    }
                    case BoxType.FourOfKind:
                    case BoxType.FiveOfKind:
                    case BoxType.SixOfKind:
                    {
                        int searchedOccurences = (int)Type - (int)BoxType.Pair + 2;
                        for (int i = dice.FirstOrDefault().MaxValue; i > 0; i--)
                        {
                            if(CountOccurences(dice, i) >= searchedOccurences)
                            {
                                score = CalculateScoreForXOfKind(i, searchedOccurences, searchedOccurences);
                                break;
                            }
                        }
                        break;
                    }
                    case BoxType.TwoPairs:
                    {
                        //first throw has no effect here
                        firstThrow = false;
                        score = CalculateScoreForPairLikes(dice, 2, 2);
                        break;
                    }
                    case BoxType.FullHouse:
                    {
                        score = CalculateScoreForPairLikes(dice, 3, 2);
                        break;
                    }
                    case BoxType.EvenNumbers:
                    {
                        if(!CheckOccurencesInRange(dice, new [] { 1, 3, 5 }, new [] { 1, 1, 1 }))
                        {
                            score = dice.Sum(die => die.Value);
                        }
                        break;
                    }
                    case BoxType.OddNumbers:
                    {
                        if (!CheckOccurencesInRange(dice, new[] { 2, 4, 6 }, new[] { 1, 1, 1 }))
                        {
                            score = dice.Sum(die => die.Value);
                        }
                        break;
                    }
                    case BoxType.TwoTwoTwo:
                    {
                        score = CalculateScoreForPairLikes(dice, 2, 2, 2);
                        break;
                    }
                    case BoxType.ThreeThree:
                    {
                        score = CalculateScoreForPairLikes(dice, 3, 3);
                        break;
                    }
                    case BoxType.FourTwo:
                    {
                        score = CalculateScoreForPairLikes(dice, 4, 2);
                        break;
                    }
                    case BoxType.SmallStraight:
                    {
                        if (CheckOccurencesInRange(dice, 1, 5, 1))
                        {
                            score = 15;
                        }
                        break;
                    }
                    case BoxType.LargeStraight:
                    {
                        if (CheckOccurencesInRange(dice, 2, 6, 1))
                        {
                            score = 30;
                        }
                        break;
                    }
                    case BoxType.AllDifferent:
                    {
                        if (CheckOccurencesInRange(dice, 1, 6, 1))
                        {
                            score = 35;
                        }
                        break;
                    }
                    case BoxType.Min:
                    {
                        int sum = dice.Sum(die => die.Value);
                        if (sum <= 10)
                        {
                            score = 10 + (10 - sum) * 9;
                        }
                        break;
                    }
                    case BoxType.Max:
                    {
                        int sum = dice.Sum(die => die.Value);
                        if (sum >= 32)
                        {
                            score = 32 + (sum - 32) * 11;
                        }
                        break;
                    }
                    case BoxType.Chance:
                    {
                        //first throw has no effect here
                        firstThrow = false;
                        score = dice.Sum(die => die.Value);
                        break;
                    }
                    case BoxType.SmallPyramid:
                    {
                        break;
                    }
                    case BoxType.LargePyramid:
                    {
                        break;
                    }
                    default:
                    {
                        break;
                    }
            }

            if (firstThrow)
            {
                score *= 2;
            }

            return score;
        }

        private static int CalculateScoreForPairLikes(Die[] dice, params int[] requiredOccurences)
        {
            Array.Sort(requiredOccurences, (l, r) => r.CompareTo(l));

            int excludedValues = 0;
            int tempScore = 0;
            foreach (int occurence in requiredOccurences)
            {
                bool occurenceFound = false;
                for (int i = dice.FirstOrDefault().MaxValue; i > 0; i--)
                {
                    if (((1 << i) & excludedValues) != 0)
                    {
                        continue;
                    }

                    if (CountOccurences(dice, i) >= occurence)
                    {
                        tempScore += occurence * i;
                        occurenceFound = true;
                        excludedValues |= (1 << i);
                    }
                }
                if (!occurenceFound)
                {
                    tempScore = 0;
                    break;
                }
            }

            return tempScore;
        }

        private static int CalculateScoreForXOfKind(int dieValue, int occurences, int requiredOccurences)
        {
            if (occurences < requiredOccurences)
            {
                return 0;
            }

            int score = 0;
            if (dieValue <= 4)
            {
                score = dieValue * requiredOccurences;
            }
            else
            {
                switch (dieValue)
                {
                    case 5:
                    {
                        score = 50 + 5 * dieValue;
                        break;
                    }
                    case 6:
                    {
                        score = 100 + 10 * dieValue;
                        break;
                    }
                }
            }
            return score;
        }

        private static int CountOccurences(Die[] dice, int value)
        {
            return dice.Count(die => die.Value == value);
        }

        private static bool CheckOccurencesInRange(Die[] dice, int[] values, int[] minRequiredOcurences)
        {
            if (values.Length != minRequiredOcurences.Length)
            {
                Debug.LogError("values.Length must be equal to minRequiredOcurences.Length !");
                return false;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if(CountOccurences(dice, values[i]) < minRequiredOcurences[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckOccurencesInRange(Die[] dice, int minValue, int maxValue, int minRequiredOcurences)
        {
            for (int i = minValue; i < maxValue; i++)
            {
                if (CountOccurences(dice, i) < minRequiredOcurences)
                {
                    return false;
                }
            }

            return true;
        }

        public bool SetScore(Die[] dice, bool firstThrow)
        {
            if (ScoreSet)
            {
                return false;
            }

            Score = CountScore(dice, firstThrow);
            ScoreSet = true;

            return true;
        }
    }
}