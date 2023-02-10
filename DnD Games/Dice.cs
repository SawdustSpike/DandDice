using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Games
{
    public class Dice
    {
        public static int[] RollDice()
        {
            var d = new int[7];
            Random rnd = new Random();
    
            for (int i = 0; i< 7; i++)
                {
                d[i] = rnd.Next(7);
                }
            Array.Sort(d);
            return d;
        }
        public static string ScoreDice(int[] d)
        {
            if(d.Contains(2) && d.Contains(3) && d.Contains(4) && d.Contains(5))
            {
                if(d.Contains(1) || d.Contains(6))
                {
                    return "STR         ";
                }
            }
            var scores = new List<string>();
            var res = new Dictionary<int, int>();
            for(int i = 0; i < 7; i++)
            {
                if (res.ContainsKey(d[i]))
                {
                    res[d[i]]++;
                    if (res[d[i]] == 2)
                    {
                        if (scores.Contains("Pair        "))
                        {
                            scores.Remove("Pair        ");
                            scores.Add("TwoPair     ");
                        }
                        else scores.Add("Pair        ");
                    }
                    if (res[d[i]] == 3)
                    {
                        if(scores.Contains("TwoPair     "))
                        {
                            scores.Remove("TwoPair     ");
                            scores.Add("FullHouse   ");
                        }
                        else
                        {
                            scores.Remove("Pair        ");
                            scores.Add("ThreeOfAKind");
                        }
                       
                    }
                    if (res[d[i]] == 4)
                    {
                        if (scores.Contains("FullHouse   ")) scores.Remove("FullHouse   ");
                        else  scores.Remove("ThreeOfAKind"); 
                        
                        scores.Add("FourOfAKind ");
                    }
                    if (res[d[i]] == 5)
                    {
                        return "FiveOfAKind ";
                        
                    }
                }
                else
                {
                    res.Add(d[i], 1);                   
                }
               

            }
            if (scores.Contains("FourOfAKind ")) return "FourOfAKind ";
            else if (scores.Contains("FullHouse   ")) return "FullHouse   ";
            else if (scores.Contains("ThreeOfAKind")) return "ThreeOfAKind";
            else if (scores.Contains("TwoPair     ")) return "TwoPair     ";
            else if (res.Count > 4) return "None        ";
            else if (scores.Contains("Pair        ")) return "Pair        ";
            else return "None";
            //{
            //    return scores[0];
            //}
            //else return "Something Went Wrong";
            //else if(scores.Contains("Pair        ") && scores.Contains("ThreeOfAKind"))
            //{
            //    return "FullHouse   ";
            //}
            //else if(scores.Count == 2)
            //{
            //    return "TwoPair     ";
            //}
            //else return "None        ";  
            
        }
    }
}
