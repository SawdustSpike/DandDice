


using DnD_Games;
var results = new Dictionary<string, double>();

for(int i = 0; i < 1000000; i++)
{
    var dice = Dice.RollDice();
    var score = Dice.ScoreDice(dice);
    if(results.ContainsKey(score)) results[score]++;
    else
    {
        results.Add(score, 1);
    }
}
foreach(var kvp in results)
{
    Console.WriteLine(kvp.Key + " = " + kvp.Value/10000 + "%");
}


