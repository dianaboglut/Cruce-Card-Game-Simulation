# Cruce-Game
This project simulates a 4-player card game called Cruce. The game is played in pairs, with two teams of two players each. The game consists of 6 rounds where each player places a card. The game features a bidding system where players predict how many "big points" they expect to score. The game includes various card values and suits, and the trump suit is determined by the highest bid.

This project does not implement an AI for playing or winning the game; it is designed to assist in managing the 6 rounds and calculate the results based on the cards played and the points won.

## Game Overview
- Players: 4 players, divided into 2 teams (pairs).
- Cards: The deck consists of 24 cards, with values 2, 3, 4, 9, 10, and 11 (As). The suits are Rosu, Duba, Verde, and Ghinda.
- Rounds: The game consists of 6 rounds, where each player places a card per round.
- Auction: Players bid for the number of "big points" they estimate they will score. The available bids are 0, 1, 2, 3, or 4 big points.
- Big Points: 1 big point = 33 small points. Small points are calculated by adding the values of the cards won (excluding 9 cards).
- Tromf Suit: The color of the first card placed determines the trump suit for the game, which is chosen by the player who bids the most.

## How to Play
### Step 1: Biding
- Players bid for the number of big points they expect to achieve.
- The highest bidder determines the trump suit for the game, based on the color of the first card placed.

### Step 2: Playing the Game
- Each player places one card in each of the 6 rounds.
- Points are calculated based on the cards won by each team.

### Step 3: Scoring
- Small points are awarded for the cards won, except for the 9s.
- The total points are calculated and compared to the initial bid.

## Features
- Simulate the 6 rounds of card placement.
- Assist in calculating the results based on the cards played.
- Handle the bidding process and determine the trump suit.
- Calculate points based on the cards won (excluding 9 cards).

## Installation & Setup
### Requirements
Visual Studio 2019 or any C compiler.

![image](https://github.com/user-attachments/assets/5597b4a3-5491-4953-bf27-88752d0bb21a)
![image](https://github.com/user-attachments/assets/8719cca6-4c2b-4a35-aa75-5418adbc1643)
![image](https://github.com/user-attachments/assets/c97a8505-553e-4aae-94e4-23b3f594aef8)
![image](https://github.com/user-attachments/assets/600b29b6-8a88-4f00-860e-e57727c3fe36)
![image](https://github.com/user-attachments/assets/6b12de08-e2c1-4293-82b6-21a8dd0f8f9f)
![image](https://github.com/user-attachments/assets/3070dfc8-b11b-4380-9db8-e509f5820027)


