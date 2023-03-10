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
            Console.WriteLine($"Three Shared dice were rolled. Twas a {x[0][2]} {x[0][3]} {x[0][4]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]} {x[i][1]} {x[i][2]} {x[i][3]} {x[i][4]}");
                    Console.WriteLine();
                }
            }
        }
        public static void DisplayTurn(List<int[]> x)
        {
            Console.WriteLine($"One more die was rolled. The table now shows {x[0][5]} {x[0][2]} {x[0][3]} {x[0][4]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]} {x[i][1]} {x[i][2]} {x[i][3]} {x[i][4]} {x[0][5]}");
                    Console.WriteLine();
                }
            }
        }
        public static void DisplayRiver(List<int[]> x)
        {
            Console.WriteLine($"One more die was rolled. The table now shows {x[0][6]} {x[0][5]} {x[0][2]} {x[0][3]} {x[0][4]}");
            Console.WriteLine("Would you like to review your individual dice? Please Type Yes or No.");
            var a = Console.ReadLine();
            if (a.ToLower() == "yes")
            {
                for (int i = 0; i < x.Count; i++)
                {
                    Console.WriteLine($"Player {i + 1}, Hit Enter To See Your Dice.");
                    Console.ReadLine();
                    Console.WriteLine($"You have a {x[i][0]} {x[i][1]} {x[i][2]} {x[i][3]} {x[i][4]} {x[0][5]} {x[0][6]}");
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
            for (int i = 0; i < 5; i++)
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
        public static int[] BestFive(int[] d)
        {

            if (d.Contains(1) && d.Contains(2) && d.Contains(3) && d.Contains(4) && d.Contains(5)) return new int[] { 1, 2, 3, 4, 5 };           
            else if(d.Contains(6) && d.Contains(2) && d.Contains(3) && d.Contains(4) && d.Contains(5)) return new int[] { 2, 3, 4, 5, 6 };
            
            var res = new Dictionary<int, int>();
            for (int i = 0; i < 7; i++)
            {
                if (res.ContainsKey(d[i]))
                {
                    res[d[i]]++;                    
                }
                else
                {
                    res.Add(d[i], 1);
                }
            }
            if (res.ContainsValue(5))
            {
                var x = res.FirstOrDefault(x => x.Value == 5).Key;
                return new int[] { x, x, x, x, x };
            }
            else if (res.ContainsValue(4))
            {
                var x = res.FirstOrDefault(x => x.Value == 4).Key;
                var y = new int[5] {0, x, x, x, x, };
                for(int i = 6; i >=0; i--)
                {
                    if (!y.Contains(d[i]))
                    {
                        y[0] = d[i];
                        Array.Sort(y);
                        return y;
                    }
                }
            }
            else if(res.ContainsValue(3) && res.ContainsValue(2)) 
            {
                var x = res.FirstOrDefault(x => x.Value == 3).Key;
                var y = new int[5] { x, x, x, 0, 0 };
                var z = new List<int>();
                foreach (var kvp in res)
                {
                    if (kvp.Value == 2)
                    {
                        z.Add(kvp.Key);
                    }
                }
                z.Sort();

                        y[4] = z[z.Count()-1];
                        y[3] = z[z.Count() - 1];
                Array.Sort(y);
                        return y;  
            }

            else if (res.ContainsValue(3))
            {
                var z = new List<int>();
                foreach (var kvp in res)
                {
                    if (kvp.Value == 3)
                    {
                        z.Add(kvp.Key);
                    }
                }
                z.Sort();
                var y = new int[5];
                y[4] = z[z.Count() - 1];
                y[3] = z[z.Count() - 1];
                y[2] = z[z.Count() - 1];
                int x = 1;
                for (int i = 6; i >= 0; i--)
                {
                    if (!y.Contains(d[i]))
                    {
                        y[x] = d[i];
                        x--;
                        if (x < 0)
                        {                            
                            return y;
                        }
                        
                    }
                }
            }
            else if (res.ContainsValue(2))
            {
                var z = new List<int>();
                foreach (var kvp in res)
                {
                    if (kvp.Value == 2)
                    {
                        z.Add(kvp.Key);
                    }
                }
                var y = new int[5];
                y[4] = z[z.Count() - 1];
                y[3] = z[z.Count() - 1];
                if (z.Count > 1)
                {
                    y[2] = z[z.Count - 2];
                    y[1] = z[z.Count - 2];
                    for (int i = 6; i >= 0; i--)
                    {
                        if (!y.Contains(d[i]))
                        {
                            y[0] = d[i];
                            
                            return y;
                        }
                    }
                }
                else
                {
                    var co = 2;
                    for (int i = 6; i >= 0; i--)
                    {
                        if (!y.Contains(d[i]))
                        {
                            y[co] = d[i];
                            co--;
                            if (co < 0)
                            {
                                Array.Sort(y);
                                return y;
                            }
                        }
                    }
                }
            }
            return new int[5];

        }    
        public static void FindWinner(List<int[]> x)
        {
            var res = new List<Player>();

            var c = 1;
            foreach (var y in x)
            {
                Array.Sort(y);
                var pl = new Player();
                pl.Dice = Game.BestFive(y);
                pl.Score = Game.ScoreDice(pl.Dice);                
                pl.Name = $"Player {c}";
                res.Add(pl);
                c++;
            }
            List<Player> winners = res.OrderBy(o => o.Score).ThenBy(o => o.Dice[4]).ThenBy(o => o.Dice[3]).ThenBy(o => o.Dice[2]).ThenBy(o => o.Dice[1]).ThenBy(o => o.Dice[0]).ToList();
            if (winners[0].Dice.SequenceEqual(winners[1].Dice))
            {
                Console.WriteLine("By The Gods, a tie!");
                var tied = new List<Player>() { winners[0], winners[1] };
                for (int i = 2; i < winners.Count; i++)
                {
                    if (winners[0].Dice.SequenceEqual(winners[i].Dice))
                    {
                        tied.Add(winners[i]);
                    }
                }
                foreach (var p in tied)
                {
                    Console.WriteLine(p.Name);
                }
                Console.WriteLine($"have tied with a{winners[0].Score.Remove(0, 1)}, {winners[0].Dice[4]} high. Split the pot amoungst you.");

            }
            else
            {
                Console.WriteLine($"The Winner is {winners[0].Name} with a{winners[0].Score.Remove(0, 1)}, {winners[0].Dice[4]} high. Now take your winnings and leave in peace.");
            }
            Console.WriteLine("Winning hand was a" + winners[0].Dice[0] + winners[0].Dice[1] + winners[0].Dice[2] + winners[0].Dice[3] + winners[0].Dice[4]);
            Console.ReadLine();
            //return res;
        }
        //public static string FindWinner(List<Player> x)
        //{
        //    List<Player> winners = x.OrderBy(o => o.Score).ThenBy(o => o.Dice[6]).ThenBy(o => o.Dice[5]).ThenBy(o => o.Dice[4]).ThenBy(o => o.Dice[3]).ThenBy(o => o.Dice[2]).ThenBy(o => o.Dice[1]).ThenBy(o => o.Dice[0]).ToList();
           
        //    return $"The Winner is {winners[0].Name} with a {winners[0].Score.Remove(0,1)}, {winners[0].Dice[6]} high. Now take your winnings and leave in peace.";
        //}
    }
}
