using System;
using System.Collections.Generic;
using System.Linq;

//======================== MAIN CLASS ===================================
class JocCruce
{
    static void Main(string[] args)
    {
        //string player1 = "Diana";
        //string player2 = "Anton";
        //string player3 = "Luca";
        //string player4 = "Mihai";

        Console.WriteLine("\n\t### CRUCE GAME by Diana Boglut ###\n");

        Console.WriteLine("Enter your name: ");
        Console.Write("Player 1: ");
        string player1 = Console.ReadLine();
        Console.Write("Player 2: ");
        string player2 = Console.ReadLine();
        Console.Write("Player 3: ");
        string player3 = Console.ReadLine();
        Console.Write("Player 4: ");
        string player4 = Console.ReadLine();

        Game game = new Game(player1, player2, player3, player4);
        game.Start();
    }
}
//======================== CARD CLASS ===================================
public class Card
{
    public string Color { get; set; } // The color "Rosu", "Duba", "Ghinda", "Verde"
    public int Number { get; set; } // 2,3,4,9,10,11(as)

    public Card(string color, int number)
    {
        Color = color;
        Number = number;
    }

    public override string ToString()
    {
        ConsoleColor originalColor = Console.ForegroundColor;

        if (Color == "Rosu")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (Color == "Verde")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (Color == "Duba")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        else if (Color == "Ghinda")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        string result = $"{Number} -> {Color}";
        Console.ForegroundColor = originalColor;
        return result;
    }
}
//======================== DECK CLASS ===================================
public class Deck
{
    private List<Card> Cards;

    public Deck()
    {
        Cards = new List<Card>();
        string[] colors = { "Rosu", "Duba", "Verde", "Ghinda" };
        int[] numbers = { 2, 3, 4, 9, 10, 11 };

        foreach (var suit in colors)
        {
            foreach (var num in numbers)
            {
                Cards.Add(new Card(suit, num)); 
            }
        }
        Shuffle();
    }
    private void Shuffle()
    {
        Random rnd = new Random();
        Cards = Cards.OrderBy(_ => rnd.Next()).ToList();
    }
    public List<Card> Deal(int count)
    {
        List<Card> hand = Cards.Take(count).ToList();
        Cards.RemoveRange(0, count); 
        return hand;
    }
}
//======================== PLAYER CLASS ================================
public class Player
{
    public string Name { get; set; }
    public List<Card> Hand { get; set; }
    public int Bid { get; set; } 
    public int Points { get; set; }

    public Player(string name)
    {
        Name = name;
        Bid = 0;
        Points = 0;
        Hand = new List<Card>();
    }
    public void ReceiveCards(List<Card> cards)
    {
        Hand = cards;
    }
    public Card PlayCard(int index)
    {
        if (index < 0 || index >= Hand.Count)
        {
            throw new ArgumentException("Invalid Index");
        }
        var card = Hand[index];
        Hand.RemoveAt(index);
        return card;
    }
}
//======================== GAME CLASS ===================================
public class Game
{
    public event Action<Player> OnKeyPressHandled;  

    private List<Player> Players;
    private Deck GameDeck;
    private string Tromf;
    private Player CurrentStartingPlayer;

    public Game(string player1, string player2, string player3, string player4)
    {
        Players = new List<Player>
        {
            new Player(player1),
            new Player(player2),
            new Player(player3),
            new Player(player4)
        };
        GameDeck = new Deck();
    }

    /*----------------------------------------------------------------*/
    /*---------------------- Start the Game Method -------------------*/
    /*----------------------------------------------------------------*/
    public void Start()
    {
        Console.Clear();
        Console.WriteLine("\t\t=== Starting the Game ===\n");

        DealCards();
        PerformBidding();
        PlayRounds();
        CalculateScores();

        Console.WriteLine("\n\t\t   === End of the game! ===");
    }

    /*----------------------------------------------------------------*/
    /*------------------ Dealing the cards Method --------------------*/
    /*----------------------------------------------------------------*/
    public void DealCards()
    {
        Console.WriteLine("==== Dealing the cards ====\n");
        int index = 1;

        foreach (var player in Players)
        {
            player.ReceiveCards(GameDeck.Deal(6));
            if (index == 1 || index == 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{index}. {player.Name} received 6 cards.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{index}. {player.Name} received 6 cards.");
                Console.ResetColor();
            }
            index++;
        }

        Console.WriteLine("\nTeams: Player 1 and Player 3");
        Console.WriteLine("       Player 2 and Player 4");
        Console.WriteLine("\n-------------------------------");
        Console.WriteLine("-------------------------------\n");
    }

    /*----------------------------------------------------------------*/
    /*----------------------- Bidding Method -------------------------*/
    /*----------------------------------------------------------------*/
    public void PerformBidding()
    {
        int highestBid = 0;
        Player highestBidder = null;
        int index = 1;

        foreach (var player in Players)
        {
            Console.WriteLine("\n==== Performing Bidding ====\n");

            DisplayPlayerCards(player);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n{index}. {player.Name}, is bidding (0-4): ");
            Console.ResetColor();

            int bid = int.Parse(Console.ReadLine());
            player.Bid = bid;

            if (bid > highestBid)
            {
                highestBid = bid;
                highestBidder = player;
            }
            HandleKeyPress(player); 

            index++;
        }
     
        if (highestBidder != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Player {highestBidder.Name} won the lottery with the highest bid: {highestBid}");
            Console.ResetColor();

            DisplayPlayerCards(highestBidder);

            Console.Write("Choose the tromf color: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Tromf = Console.ReadLine();
            Console.ResetColor();

            CurrentStartingPlayer = highestBidder;
            HandleKeyPress(highestBidder);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"The Tromf is: {Tromf}");
            Console.ResetColor();


        }
    }

    /*----------------------------------------------------------------*/
    /*---------------- Display Player's cards Method -----------------*/
    /*----------------------------------------------------------------*/
    private void DisplayPlayerCards(Player player)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"\t{player.Name}'s cards: ");
        Console.ResetColor();

        int width = 20; 
        int height = player.Hand.Count + 2;

        Console.Write("\t");
        Console.WriteLine(new string('~', width));

        int index = 0;
        foreach (var c in player.Hand)
        {
            if (c.Color == "Rosu")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (c.Color == "Verde")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (c.Color == "Duba")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (c.Color == "Ghinda")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (c.Color == Tromf)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            string cardText = $"{index}: {c}";
            int padding = (width - 15) / 2;
            string paddedCard = new string(' ', padding) + cardText;
            Console.WriteLine($"\t|{paddedCard.PadRight(width - 2)}|");

            index++;
        }

        Console.ResetColor();
        Console.Write("\t");
        Console.WriteLine(new string('~', width));
    }

    /*----------------------------------------------------------------*/
    /*----------------------- Handle Key Press -----------------------*/
    /*----------------------------------------------------------------*/
    private void HandleKeyPress(Player player)
    {
        Console.WriteLine("\nPress 'C' to continue...");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        while (keyInfo.Key != ConsoleKey.C)
        {
            Console.WriteLine("Invalid key. Press 'C' to continue...");
            keyInfo = Console.ReadKey(true);
        }

        OnKeyPressHandled?.Invoke(player);

        Console.Clear();
    }

    /*----------------------------------------------------------------*/
    /*---------------------- Play Rounds Method ----------------------*/
    /*----------------------------------------------------------------*/
    public void PlayRounds()
    {
        Console.WriteLine("\n==== Playing Rounds ====\n");

        for (int round = 1; round <= 6; round++)
        {
            List<(Player player, Card card)> cardsPlayed = new List<(Player player, Card card)>();

            var playerOrder = GetPlayerOrder(CurrentStartingPlayer);
            Card firstCard = null;

            foreach (var player in playerOrder)
            {
                Console.WriteLine($"---- ROUND {round} ----");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{Players[0].Name} and {Players[2].Name} are in the same team.");
                Console.WriteLine($"{Players[1].Name} and {Players[3].Name} are in the same team.\n");
                Console.ResetColor();

                DisplayPlayerCards(player);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{player.Name} choose the card to play (0-{player.Hand.Count - 1}): ");
                Console.ResetColor();

                int cardIndex;

                while (true)
                {
                    cardIndex = int.Parse(Console.ReadLine());
                    if (cardIndex >= 0 && cardIndex < player.Hand.Count)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Error.WriteLine("Invalid input! Enter a valid card index.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{player.Name}, choose the card to play (0-{player.Hand.Count - 1}): ");
                    Console.ResetColor();
                }

                var card = player.Hand[cardIndex];

                if (player.Name == CurrentStartingPlayer.Name)
                {
                    firstCard = card;
                }
                else
                {
                    while (true)
                    {
                        bool hasLeadingColor = player.Hand.Any(c => c.Color == firstCard.Color);
                        bool hasTromf = player.Hand.Any(c => c.Color == Tromf);

                        if (card.Color == firstCard.Color || (card.Color == Tromf && !hasLeadingColor) ||  (!hasLeadingColor && !hasTromf)) 
                        {
                            break; 
                        }

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Error.WriteLine("ERROR: You must play a card matching the rules!");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{player.Name}, choose another card to play (0-{player.Hand.Count - 1}): ");
                        Console.ResetColor();

                        cardIndex = int.Parse(Console.ReadLine());
                        card = player.Hand[cardIndex];
                    }

                }

                card = player.PlayCard(cardIndex);
                cardsPlayed.Add((player, card));

                HandleKeyPress(player);

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"The Tromf is: {Tromf}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (var (p, c) in cardsPlayed)
                {
                    Console.WriteLine($"!! {p.Name} has played: {c}\n");
                }
                Console.ResetColor();
            }

            var winner = DetermineRoundWinner(cardsPlayed);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{winner.Name} won the round!");
            Console.ResetColor();

            int points = 0;
            foreach (var (p, c) in cardsPlayed)
            {
                if (c.Number != 9)
                {
                    points += c.Number;
                }
            }
            winner.Points += points;
            Console.WriteLine($"{winner.Name} has {winner.Points} points");
            CurrentStartingPlayer = winner;

            Console.WriteLine("-------------------------------\n");
        }
    }

    /*----------------------------------------------------------------*/
    /*---------------------- Player order Method ---------------------*/
    /*----------------------------------------------------------------*/
    private List<Player> GetPlayerOrder(Player startingPlayer)
    {
        int startIndex = Players.IndexOf(startingPlayer);
        var playerOrder = new List<Player>();

        for (int i = 0; i < Players.Count; i++)
        {
            playerOrder.Add(Players[(startIndex + i) % Players.Count]);
        }

        return playerOrder;
    }

    /*----------------------------------------------------------------*/
    /*---------------------- Round Winner Method ---------------------*/
    /*----------------------------------------------------------------*/
    public Player DetermineRoundWinner(List<(Player player, Card card)> cardsPlayed)
    {
        var tromfCards = cardsPlayed.Where(c => c.card.Color == Tromf).ToList();
        if (tromfCards.Any())
        {
            return tromfCards.OrderByDescending(c => c.card.Number).First().player;
        }
        var leadingColor = cardsPlayed[0].card.Color;
        var sameSuitCards = cardsPlayed.Where(c => c.card.Color == leadingColor).ToList();
        return sameSuitCards.OrderByDescending(c => c.card.Number).First().player;
    }

    /*----------------------------------------------------------------*/
    /*--------------- The score method implementation ----------------*/
    /*----------------------------------------------------------------*/
    private void CalculateScores()
    {
        int team1Score = 0;
        int team2Score = 0;
        Console.WriteLine("==== Calculate score ====\n");

        foreach (var player in Players)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{player.Name} has {player.Points} points.");
            Console.ResetColor();
        }

        foreach (var player in Players)
        {
            if (player == Players[0] || player == Players[2])
            {
                team1Score += player.Points;
            }
            else
            {
                team2Score += player.Points;
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{Players[0].Name} and {Players[2].Name} score: {team1Score}");
        Console.WriteLine($"{Players[1].Name} and {Players[3].Name} score: {team2Score}");
        Console.ResetColor();

        Console.WriteLine("\n-------------------------------\n");
    }
}
