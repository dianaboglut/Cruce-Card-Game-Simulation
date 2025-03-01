//using System;
//using System.Collections.Generic;
//using System.Linq;

////======================== MAIN CLASS ===================================
//class JocCruce
//{
//    static void Main(string[] args)
//    {
//        //string player1 = "Diana";
//        //string player2 = "Anton";
//        //string player3 = "Luca";
//        //string player4 = "Mihai";

//        Console.WriteLine("\n\t### CRUCE GAME by Diana Boglut ###\n");

//        Console.WriteLine("Enter your name: ");
//        Console.Write("Player 1: ");
//        string player1 = Console.ReadLine();
//        Console.Write("Player 2: ");
//        string player2 = Console.ReadLine();
//        Console.Write("Player 3: ");
//        string player3 = Console.ReadLine();
//        Console.Write("Player 4: ");
//        string player4 = Console.ReadLine();

//        Game game = new Game(player1, player2, player3, player4);
//        game.Start();
//    }
//}
////======================== CARD CLASS ===================================
//public class Card
//{
//    public string Color { get; set; } // The color "Rosu", "Duba", "Ghinda", "Verde"
//    public int Number { get; set; } // 2,3,4,9,10,11(as)

//    public Card(string color, int number)
//    {
//        Color = color;
//        Number = number;
//    }

//    public override string ToString()
//    {
//        ConsoleColor originalColor = Console.ForegroundColor;

//        if (Color == "Rosu")
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//        }
//        else if (Color == "Verde")
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//        }
//        else if (Color == "Duba")
//        {
//            Console.ForegroundColor = ConsoleColor.DarkYellow;
//        }
//        else if (Color == "Ghinda")
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//        }

//        string result = $"{Number} -> {Color}";
//        Console.ForegroundColor = originalColor;
//        return result;
//    }
//}
////======================== DECK CLASS ===================================
//public class Deck
//{
//    private List<Card> Cards;

//    public Deck()
//    {
//        Cards = new List<Card>();
//        string[] colors = { "Rosu", "Duba", "Verde", "Ghinda" };
//        int[] numbers = { 2, 3, 4, 9, 10, 11 };

//        // Creating the deck with 24 cards 
//        foreach (var suit in colors)
//        {
//            foreach (var num in numbers)
//            {
//                Cards.Add(new Card(suit, num)); // Creating the "object" card
//            }
//        }
//        Shuffle();
//    }
//    // Method to shuffle the deck of cards for the begining. OrderBy method sorts the cards based on these random numbers, effectively randomizing their order.
//    // rnd.Next() generates a random number for each card in the list
//    private void Shuffle()
//    {
//        Random rnd = new Random();
//        Cards = Cards.OrderBy(_ => rnd.Next()).ToList();
//    }
//    // Giving cards to players
//    public List<Card> Deal(int count)
//    {
//        List<Card> hand = Cards.Take(count).ToList();
//        Cards.RemoveRange(0, count); // Removes the first "count" cards from the Cards list. This updates the Cards list to reflect that the dealt cards have been removed.
//        return hand;
//    }
//}
////======================== PLAYER CLASS ================================
//public class Player
//{
//    public string Name { get; set; }
//    public List<Card> Hand { get; set; }
//    public int Bid { get; set; } // Licitatie
//    public int Points { get; set; }

//    public Player(string name)
//    {
//        Name = name;
//        Bid = 0;
//        Points = 0;
//        Hand = new List<Card>();
//    }
//    public void ReceiveCards(List<Card> cards)
//    {
//        Hand = cards;
//    }
//    public Card PlayCard(int index)
//    {
//        if (index < 0 || index >= Hand.Count)
//        {
//            throw new ArgumentException("Invalid Index");
//        }
//        var card = Hand[index];
//        Hand.RemoveAt(index);
//        return card;
//    }
//}
////======================== GAME CLASS ===================================
//public class Game
//{
//    public event Action<Player> OnKeyPressHandled;   // Event triggered after key press

//    private List<Player> Players;
//    private Deck GameDeck;
//    private string Tromf;
//    private Player CurrentStartingPlayer;

//    public Game(string player1, string player2, string player3, string player4)
//    {
//        Players = new List<Player>
//        {
//            new Player(player1),
//            new Player(player2),
//            new Player(player3),
//            new Player(player4)
//        };
//        GameDeck = new Deck();
//    }

//    /*----------------------------------------------------------------*/
//    /*---------------------- Start the Game Method -------------------*/
//    /*----------------------------------------------------------------*/
//    public void Start()
//    {
//        Console.Clear();
//        Console.WriteLine("\t\t=== Starting the Game ===\n");

//        DealCards();
//        PerformBidding();
//        PlayRounds();
//        CalculateScores();

//        Console.WriteLine("\n\t\t   === End of the game! ===");
//    }

//    /*----------------------------------------------------------------*/
//    /*------------------ Dealing the cards Method --------------------*/
//    /*----------------------------------------------------------------*/
//    public void DealCards()
//    {
//        Console.WriteLine("==== Dealing the cards ====\n");
//        int index = 1;

//        foreach (var player in Players)
//        {
//            player.ReceiveCards(GameDeck.Deal(6));
//            if (index == 1 || index == 3)
//            {
//                Console.ForegroundColor = ConsoleColor.Cyan;
//                Console.WriteLine($"{index}. {player.Name} received 6 cards.");
//                Console.ResetColor();
//            }
//            else
//            {
//                Console.ForegroundColor = ConsoleColor.Blue;
//                Console.WriteLine($"{index}. {player.Name} received 6 cards.");
//                Console.ResetColor();
//            }
//            index++;
//        }

//        Console.WriteLine("\nTeams: Player 1 and Player 3");
//        Console.WriteLine("       Player 2 and Player 4");
//        Console.WriteLine("\n-------------------------------");
//        Console.WriteLine("-------------------------------\n");
//    }

//    /*----------------------------------------------------------------*/
//    /*----------------------- Bidding Method -------------------------*/
//    /*----------------------------------------------------------------*/
//    public void PerformBidding()
//    {
//        int highestBid = 0;
//        Player highestBidder = null;
//        int index = 1;

//        foreach (var player in Players)
//        {
//            Console.WriteLine("\n==== Performing Bidding ====\n");

//            DisplayPlayerCards(player);

//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.Write($"\n{index}. {player.Name}, is bidding (0-4): ");
//            Console.ResetColor();

//            int bid = int.Parse(Console.ReadLine()); // convertesc la int
//            player.Bid = bid;

//            if (bid > highestBid)
//            {
//                highestBid = bid;
//                highestBidder = player;
//            }
//            HandleKeyPress(player); // Wait for key press

//            index++;
//        }
//        // The player that has the highest bid will set the tromf and start the game
//        if (highestBidder != null)
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine($"Player {highestBidder.Name} won the lottery with the highest bid: {highestBid}");
//            Console.ResetColor();

//            DisplayPlayerCards(highestBidder);

//            Console.Write("Choose the tromf color: ");
//            Console.ForegroundColor = ConsoleColor.Magenta;
//            Tromf = Console.ReadLine();
//            Console.ResetColor();

//            CurrentStartingPlayer = highestBidder; // Set the starting player for the first round
//            HandleKeyPress(highestBidder);

//            Console.ForegroundColor = ConsoleColor.DarkRed;
//            Console.WriteLine($"The Tromf is: {Tromf}");
//            Console.ResetColor();


//        }
//        //Console.WriteLine("\n-------------------------------\n");
//    }

//    /*----------------------------------------------------------------*/
//    /*---------------- Display Player's cards Method -----------------*/
//    /*----------------------------------------------------------------*/
//    private void DisplayPlayerCards(Player player)
//    {
//        Console.ForegroundColor = ConsoleColor.DarkMagenta;
//        Console.WriteLine($"\t{player.Name}'s cards: ");
//        Console.ResetColor();

//        // Determine dimensions for the card display
//        int width = 20; // Width of the square
//        int height = player.Hand.Count + 2; // Number of cards + top and bottom border

//        // Print the top border
//        Console.Write("\t");
//        Console.WriteLine(new string('~', width));

//        // Print each card within the border
//        int index = 0;
//        foreach (var c in player.Hand)
//        {
//            if (c.Color == "Rosu")
//            {
//                Console.ForegroundColor = ConsoleColor.Magenta;
//            }
//            else if (c.Color == "Verde")
//            {
//                Console.ForegroundColor = ConsoleColor.Green;
//            }
//            else if (c.Color == "Duba")
//            {
//                Console.ForegroundColor = ConsoleColor.DarkYellow;
//            }
//            else if (c.Color == "Ghinda")
//            {
//                Console.ForegroundColor = ConsoleColor.Yellow;
//            }

//            // If the card is of the trump color, apply a tromf highlight
//            if (c.Color == Tromf)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//            }
//            string cardText = $"{index}: {c}";
//            // Center the card text within the width
//            int padding = (width - 15) / 2;
//            string paddedCard = new string(' ', padding) + cardText;
//            Console.WriteLine($"\t|{paddedCard.PadRight(width - 2)}|");

//            index++;
//        }

//        // Print the bottom border

//        Console.ResetColor();
//        Console.Write("\t");
//        Console.WriteLine(new string('~', width));
//    }

//    /*----------------------------------------------------------------*/
//    /*----------------------- Handle Key Press -----------------------*/
//    /*----------------------------------------------------------------*/
//    private void HandleKeyPress(Player player)
//    {
//        Console.WriteLine("\nPress 'C' to continue...");
//        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

//        while (keyInfo.Key != ConsoleKey.C)
//        {
//            Console.WriteLine("Invalid key. Press 'C' to continue...");
//            keyInfo = Console.ReadKey(true);
//        }

//        // Raise the OnKeyPressHandled event
//        OnKeyPressHandled?.Invoke(player);

//        Console.Clear();
//    }

//    /*----------------------------------------------------------------*/
//    /*---------------------- Play Rounds Method ----------------------*/
//    /*----------------------------------------------------------------*/
//    public void PlayRounds()
//    {
//        Console.WriteLine("\n==== Playing Rounds ====\n");

//        for (int round = 1; round <= 6; round++)
//        {
//            List<(Player player, Card card)> cardsPlayed = new List<(Player player, Card card)>(); // tuplu

//            // Determine the order of play, starting with the current starting player
//            var playerOrder = GetPlayerOrder(CurrentStartingPlayer);
//            Card firstCard = null;

//            foreach (var player in playerOrder)
//            {
//                Console.WriteLine($"---- ROUND {round} ----");

//                Console.ForegroundColor = ConsoleColor.Yellow;
//                Console.WriteLine($"\n{Players[0].Name} and {Players[2].Name} are in the same team.");
//                Console.WriteLine($"{Players[1].Name} and {Players[3].Name} are in the same team.\n");
//                Console.ResetColor();

//                DisplayPlayerCards(player);

//                Console.ForegroundColor = ConsoleColor.Yellow;
//                Console.Write($"{player.Name} choose the card to play (0-{player.Hand.Count - 1}): ");
//                Console.ResetColor();

//                int cardIndex;

//                while (true)
//                {
//                    cardIndex = int.Parse(Console.ReadLine());
//                    if (cardIndex >= 0 && cardIndex < player.Hand.Count)
//                    {
//                        break; // valid input
//                    }

//                    Console.ForegroundColor = ConsoleColor.DarkYellow;
//                    Console.Error.WriteLine("Invalid input! Enter a valid card index.");
//                    Console.ResetColor();
//                    Console.ForegroundColor = ConsoleColor.Yellow;
//                    Console.Write($"{player.Name}, choose the card to play (0-{player.Hand.Count - 1}): ");
//                    Console.ResetColor();
//                }

//                var card = player.Hand[cardIndex];

//                if (player.Name == CurrentStartingPlayer.Name)
//                {
//                    firstCard = card;
//                }
//                else
//                {
//                    while (true)
//                    {
//                        bool hasLeadingColor = player.Hand.Any(c => c.Color == firstCard.Color);
//                        bool hasTromf = player.Hand.Any(c => c.Color == Tromf);

//                        if (card.Color == firstCard.Color || // Matches leading color
//                            (card.Color == Tromf && !hasLeadingColor) || // Tromf allowed only if no leading color
//                            (!hasLeadingColor && !hasTromf)) // Any card allowed if no leading color or Tromf
//                        {
//                            break; // Valid card
//                        }

//                        Console.ForegroundColor = ConsoleColor.DarkYellow;
//                        Console.Error.WriteLine("ERROR: You must play a card matching the rules!");
//                        Console.ResetColor();

//                        Console.ForegroundColor = ConsoleColor.Yellow;
//                        Console.Write($"{player.Name}, choose another card to play (0-{player.Hand.Count - 1}): ");
//                        Console.ResetColor();

//                        cardIndex = int.Parse(Console.ReadLine());
//                        card = player.Hand[cardIndex];
//                    }

//                }

//                card = player.PlayCard(cardIndex);
//                cardsPlayed.Add((player, card));

//                HandleKeyPress(player); // Wait for key press

//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"The Tromf is: {Tromf}");
//                Console.ResetColor();

//                // Printing the cards that each player played in the current round. For debugging
//                Console.ForegroundColor = ConsoleColor.Cyan;
//                foreach (var (p, c) in cardsPlayed)
//                {
//                    Console.WriteLine($"!! {p.Name} has played: {c}\n");
//                }
//                Console.ResetColor();
//            }

//            // The winner of the round is the player that takes the cards  
//            var winner = DetermineRoundWinner(cardsPlayed);

//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine($"{winner.Name} won the round!");
//            Console.ResetColor();

//            // Total points for the winner of the round
//            int points = 0;
//            foreach (var (p, c) in cardsPlayed)
//            {
//                if (c.Number != 9)
//                {
//                    points += c.Number;
//                }
//            }
//            winner.Points += points;
//            Console.WriteLine($"{winner.Name} has {winner.Points} points");
//            // Set the winner as the starting player for the next round
//            CurrentStartingPlayer = winner;

//            Console.WriteLine("-------------------------------\n");
//        }
//    }

//    /*----------------------------------------------------------------*/
//    /*---------------------- Player order Method ---------------------*/
//    /*----------------------------------------------------------------*/
//    private List<Player> GetPlayerOrder(Player startingPlayer)
//    {
//        int startIndex = Players.IndexOf(startingPlayer);
//        var playerOrder = new List<Player>();

//        for (int i = 0; i < Players.Count; i++)
//        {
//            playerOrder.Add(Players[(startIndex + i) % Players.Count]);
//        }

//        return playerOrder;
//    }

//    /*----------------------------------------------------------------*/
//    /*---------------------- Round Winner Method ---------------------*/
//    /*----------------------------------------------------------------*/
//    public Player DetermineRoundWinner(List<(Player player, Card card)> cardsPlayed)
//    {
//        // Tromful beats any other suit, and in the same suit wins the higher value
//        // I filter the played cards so that I only have tromf colored cards
//        var tromfCards = cardsPlayed.Where(c => c.card.Color == Tromf).ToList();
//        // I'm checking to see if a tromf card was played in the round
//        if (tromfCards.Any())
//        {
//            // if it has been played then I will return the player who played the highest card (among the tromf s)
//            return tromfCards.OrderByDescending(c => c.card.Number).First().player;
//        }
//        // Determine the color of the first card played in the round. This is the card that will win the round if the trump was not played
//        var leadingColor = cardsPlayed[0].card.Color;
//        // Filter cards to include only cards that are the same as leadingColor
//        var sameSuitCards = cardsPlayed.Where(c => c.card.Color == leadingColor).ToList();
//        // If no trump card has been played then the highest card in the suit of the same suit color as the first card played wins
//        return sameSuitCards.OrderByDescending(c => c.card.Number).First().player;
//    }

//    /*----------------------------------------------------------------*/
//    /*--------------- The score method implementation ----------------*/
//    /*----------------------------------------------------------------*/
//    private void CalculateScores()
//    {
//        int team1Score = 0;
//        int team2Score = 0;
//        int winnerPoints = 0;
//        string winner;
//        Console.WriteLine("==== Calculate score ====\n");

//        foreach (var player in Players)
//        {
//            //if (player.Points > winnerPoints)
//            //{
//            //    winnerPoints = player.Points;
//            //    winner = player.Name;
//            //}

//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine($"{player.Name} has {player.Points} points.");
//            Console.ResetColor();
//        }

//        foreach (var player in Players)
//        {
//            if (player == Players[0] || player == Players[2])
//            {
//                team1Score += player.Points;
//            }
//            else
//            {
//                team2Score += player.Points;
//            }
//        }
//        Console.ForegroundColor = ConsoleColor.Yellow;
//        Console.WriteLine($"\n{Players[0].Name} and {Players[2].Name} score: {team1Score}");
//        Console.WriteLine($"{Players[1].Name} and {Players[3].Name} score: {team2Score}");
//        Console.ResetColor();

//        Console.WriteLine("\n-------------------------------\n");
//    }
//}


//// TODO: Sa tratez cazul in care exista strigare, adica carte de 4 si 3 de aceeasi culoare. Plus cazul in care strigarea e pe tromf
//// TODO: sa scad din puncte daca echipa nu face cat spune
