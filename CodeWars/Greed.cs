using System;
using System.Linq;

namespace CodeWars
{
    public class Greed
    {
        public static int Score(int[] dice)
        {
            Console.WriteLine($"[ {string.Join(", ", dice)} ]");
            int ans = 0;
            var list = dice.ToList();
            var threes = list.GroupBy(i => i).Select(g => new[] {g.First(), g.Count()}).Where(g => g[1] >= 3);
            foreach (var group in threes)
            {
                for (int i = 0; i < 3; i++)
                {
                     list.Remove(group.ToList()[0]);
                }
                ans += ThreeScore(group.ToList()[0]);
            }

            return ans + list.Select(OneScore).Sum();
        }

        private static int ThreeScore(int i)
        {
            switch (i)
            {
                case 1:
                    return 1000;
                case 2:
                    return 200;
                case 3:
                    return 300;
                case 4:
                    return 400;
                case 5:
                    return 500;
                case 6:
                    return 600;
            }
            return 0;
        }

        private static int OneScore(int i)
        {
            switch (i)
            {
                case 1:
                    return 100;
                case 5:
                    return 50;
            }
            return 0;
        }
        
    }
}
