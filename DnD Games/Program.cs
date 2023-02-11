


using DnD_Games;
//var results = new Dictionary<string, double>();

//for(int i = 0; i < 1000000; i++)
//{
//    var dice = Dice.RollDice();
//    var score = Dice.ScoreDice(dice);
//    if(results.ContainsKey(score)) results[score]++;
//    else
//    {
//        results.Add(score, 1);
//    }
//}
//foreach(var kvp in results)
//{
//    Console.WriteLine(kvp.Key + " = " + kvp.Value/10000 + "%");
//}
Console.WriteLine("Welcome to Dices and Dragons.");
int p = 0;
var par = false;
while (par == false)
{
    Console.WriteLine("How Many People are Playing?");
    bool bam = Int32.TryParse(Console.ReadLine(), out p);
    par = (bam && p>0);
    if (par) break;
    Console.WriteLine("That's not a valid number, smartass.");
}
var players = Game.IndividualRoll(p);
Console.WriteLine($"We can make due with {p} players. Rolling dice now.");
Game.DisplayFirstRolls(players);
Console.WriteLine("Place your bets now. Hit Enter When You're Ready To Continue.");
Console.ReadLine();
players = Game.FlopRoll(players);
Game.DisplayFlop(players);
Console.WriteLine("Place your bets now. Hit Enter When You're Ready To Continue.");
players = Game.TurnRoll(players);
Game.DisplayTurn(players);
Console.WriteLine("Once Again, It is Time To Place Your Bets. Again, Enter to Continue.");
players = Game.RiverRoll(players);
Game.DisplayRiver(players);
Console.WriteLine("Players, This is the Last Time to Place Bets. Hit Enter To Discover the Winner.");
Console.ReadLine();
Game.FindWinner(players);
Console.ReadLine();


