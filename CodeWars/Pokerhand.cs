using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars {
    public enum Result {
        Win,
        Loss,
        Tie
    }

    public class PokerHand {
        private long Strength;

        private Card HighestCard;

        public Card[] Hand;

        public PokerHand(string hand) {
            Hand = hand.Split(' ').Select(s => new Card(s)).ToArray();
            HighestCard = Hand.First(c => c.Value == Hand.Max(cd => cd.Value));
            Strength = CalculateStrength();
        }

        private long CalculateStrength() {
            if (IsStraightFlush()) return 0x100000000 * HighestCard.Value;
            if (IsFourOfAKind()) return 0x10000000 * (long)GetHighestValueOfKind(4);
            if (IsFullHouse()) return 0x1000000 * GetHighestValueOfKind(3);
            if (IsFlush()) return 0x100000 * HighestCard.Value;
            if (IsStraight()) return 0x10000 * HighestCard.Value;
            if (IsThreeOfAKind()) return 0x1000 * GetHighestValueOfKind(3);
            if (IsTwoPair()) return 0x100 * GetHighestValueOfKind(2);
            if (IsOnePair()) return 0x10 * GetHighestValueOfKind(2);
            return HighestCard.Value;
        }

        private int GetHighestValueOfKind(int count) {
            return Hand.GroupBy(c => c.Value).Where(g => g.Count() == count).Max(g => g.Key);
        }
        private bool IsStraightFlush() {
            return IsFlush() && IsStraight();
        }

        private bool IsFlush() {
            return Hand.GroupBy(c => c.Suit).Count() == 1;
        }

        private bool IsStraight() {
            var vals = Hand.Select(c => c.Value).OrderBy(i => i);
            return vals.Zip(vals.Skip(1), (x, y) => x - y).Count(x => x == -1) == 4;
        }

        private bool IsFourOfAKind() {
            return Hand.GroupBy(c => c.Value).Max(g => g.Count()) == 4;
        }

        private bool IsThreeOfAKind() {
            return Hand.GroupBy(c => c.Value).Max(g => g.Count()) == 3;
        }

        private bool IsFullHouse() {
            return IsThreeOfAKind() && IsOnePair();
        }

        private bool IsOnePair() {
            return Hand.GroupBy(c => c.Value).Count(g => g.Count() == 2) != 0;
        }

        private bool IsTwoPair() {
            return Hand.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 2;
        }

        public Result CompareWith(PokerHand hand) {
            if (hand.Strength > this.Strength) {
                return Result.Loss;
            }
            if (hand.Strength == this.Strength) {
                return TieBreaker(hand);
            }
            return Result.Win;
        }

        private Result TieBreaker(PokerHand hand) {
            var thisHand = this.Hand.Select(c => c.Value).OrderByDescending(i => i);
            var thatHand = hand.Hand.Select(c => c.Value).OrderByDescending(i => i);
            foreach (var couple in thisHand.Zip(thatHand, (i1, i2) => new { This = i1, That = i2 })) {
                if (couple.This > couple.That) return Result.Win;
                if (couple.That > couple.This) return Result.Loss;
            }
            return Result.Tie;
        }
    }

    public class Card {
        public string Suit { get; private set; }
        public int Value { get; private set; }
        public Card(string card) {
            Suit = card.Substring(1, 1);
            Value = GetValue(card.Substring(0, 1));
        }

        private int GetValue(string num) {
            switch (num) {
                case "A":
                    return 14;
                case "K":
                    return 13;
                case "Q":
                    return 12;
                case "J":
                    return 11;
                case "T":
                    return 10;
                default:
                    return int.Parse(num);
            }
        }
    }
}

