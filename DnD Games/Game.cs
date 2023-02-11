using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Games
{
    internal class Game
    {
        public static List<int[]> IndividualRoll(int j)
        {
            var x = new List<int[]>();
            Random rnd = new Random();
            for (int k = 0; k < j; k++)
            {
                var d = new int[7];
                for (int i = 0; i < 2; i++)
                {
                    d[i] = rnd.Next(1, 7);
                }                
                x.Add(d);
            }
            return x;
        }
        public static List<int[]> FlopRoll(List<int[]> x)
        {
            Random rnd = new Random();
            int j = rnd.Next(1, 7);
            int k = rnd.Next(1, 7);
            int l = rnd.Next(1, 7);
            for (int i = 0; i < x.Count; i++)
            {
                x[i][2] = j;
                x[i][3] = k;
                x[i][4] = l;
                
            }
            return x;
        }

        public static List<int[]> TurnRoll(List<int[]> x)
        {
            Random rnd = new Random();
            int j = rnd.Next(1,7);

            for (int i = 0; i < x.Count; i++)
            {
                x[i][5] = j;
                
            }
            return x;
        }
        public static List<int[]> RiverRoll(List<int[]> x)
        {
            Random rnd = new Random();
            int j = rnd.Next(1,7);

            for (int i = 0; i < x.Count; i++)
            {
                x[i][6] = j;
            }
            return x;
        }
        public static void DisplayFirstRolls(List<int[]> x)
        {
            for (int i = 0; i < x.Count; i++)
            {
                Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                Console.ReadLine();
                Console.WriteLine($"You Rolled a {x[i][0]} {x[i][1]}");
                Console.WriteLine();
            }
        }
        public static void DisplayFlop(List<int[]> x)
        {
            Console.WriteLine($"Three Shared dice were rolled. A {x[0][2]},  {x[0][3]}, {x[0][4]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]}, {x[i][1]}, {x[i][2]}, {x[i][3]}, {x[i][4]}");
                    Console.WriteLine();
                }
            }
        }
        public static void DisplayTurn(List<int[]> x)
        {
            Console.WriteLine($"One more die was rolled. A {x[0][5]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]}, {x[i][1]},  {x[i][2]}, {x[i][3]}, {x[i][4]}, {x[0][5]}");
                    Console.WriteLine();
                }
            }
        }
        public static void DisplayRiver(List<int[]> x)
        {
            Console.WriteLine($"One more die was rolled. A {x[0][6]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]}, {x[i][1]}, {x[i][2]}, {x[i][3]}, {x[i][4]}, {x[0][5]}, {x[0][6]}");
                    Console.WriteLine();
                }
            }
        }
        public static string ScoreDice(int[] d)
        {
            if (d.Contains(2) && d.Contains(3) && d.Contains(4) && d.Contains(5))
            {
                if (d.Contains(1) || d.Contains(6))
                {
                    return "3Straight";
                }
            }
            var scores = new List<string>();
            var res = new Dictionary<int, int>();
            for (int i = 0; i < 7; i++)
            {
                if (res.ContainsKey(d[i]))
                {
                    res[d[i]]++;
                    if (res[d[i]] == 2)
                    {
                        if (scores.Contains("7Pair"))
                        {
                            scores.Remove("7Pair");
                            scores.Add("6Two Pair");
                        }
                        else scores.Add("7Pair");
                    }
                    if (res[d[i]] == 3)
                    {
                        if (scores.Contains("6Two Pair"))
                        {
                            scores.Remove("6Two Pair");
                            scores.Add("4Full House");
                        }
                        else
                        {
                            scores.Remove("7Pair");
                            scores.Add("5Three Of A Kind");
                        }

                    }
                    if (res[d[i]] == 4)
                    {
                        if (scores.Contains("4Full House")) scores.Remove("4Full House");
                        else scores.Remove("5Three Of A Kind");

                        scores.Add("2Four Of A Kind");
                    }
                    if (res[d[i]] == 5)
                    {
                        return "1Five Of A Kind";

                    }
                }
                else
                {
                    res.Add(d[i], 1);
                }
            }
            if (scores.Contains("2Four Of A Kind")) return "2 Four Of A Kind";
            else if (scores.Contains("4Full House")) return "4Full House";
            else if (scores.Contains("5Three Of A Kind")) return "5Three Of A Kind";
            else if (scores.Contains("6Two Pair")) return "6Two Pair";
            else if (scores.Contains("7Pair")) return "7Pair";
            else return "None";
        }
        public static string FindWinner(List<int[]> x)
        {
            var res = new List<Player>();

            var c = 1;
            foreach (var y in x)
            {
                Array.Sort(y);
                var pl = new Player();
                pl.Score = Game.ScoreDice(y);
                pl.Dice = y;
                pl.Name = $"Player {c}";
                res.Add(pl);
                c++;
            }
            List<Player> winners = res.OrderBy(o => o.Score).ThenBy(o => o.Dice[6]).ThenBy(o => o.Dice[5]).ThenBy(o => o.Dice[4]).ThenBy(o => o.Dice[3]).ThenBy(o => o.Dice[2]).ThenBy(o => o.Dice[1]).ThenBy(o => o.Dice[0]).ToList();

            return $"The Winner is {winners[0].Name} with a {winners[0].Score.Remove(0, 1)}, {winners[0].Dice[6]} high. Now take your winnings and leave in peace.";
            //return res;
        }
        //public static string FindWinner(List<Player> x)
        //{
        //    List<Player> winners = x.OrderBy(o => o.Score).ThenBy(o => o.Dice[6]).ThenBy(o => o.Dice[5]).ThenBy(o => o.Dice[4]).ThenBy(o => o.Dice[3]).ThenBy(o => o.Dice[2]).ThenBy(o => o.Dice[1]).ThenBy(o => o.Dice[0]).ToList();
           
        //    return $"The Winner is {winners[0].Name} with a {winners[0].Score.Remove(0,1)}, {winners[0].Dice[6]} high. Now take your winnings and leave in peace.";
        //}
    }
}
