using System;
using System.Collections.Generic;

namespace _05_deck_of_cards_oop
{
    public class Card
    {
        public string suit;
        public string stringVal;
        public int val;

        public Card(string cardSuit, string cardStringVal, int cardVal)
        {
            suit = cardSuit;
            stringVal = cardStringVal;
            val = cardVal;
        }
    }

    public class Deck
    {
        public List<Card> cards = new List<Card>();
        public List<Card> discardedCards = new List<Card>();

        public Deck()
        {
            // initialize deck of cards
            string[] suits = {"Heart", "Spade", "Diamond", "Club"};
            string[] values = {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
            string cardValue;
            foreach (string suit in suits)
            {
                for (int j=0; j<13; j++)
                {
                    cardValue = values[j];
                    Card newCard = new Card(suit, cardValue, j+1);
                    cards.Add(newCard);
                }
            }
        }

        public void ShowDeck()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine("Card: " + card.suit + ", " + card.stringVal + " (" + card.val + ")");
            }
        }

        public void ShowDiscard()
        {
            foreach (Card card in discardedCards)
            {
                Console.WriteLine("Card: " + card.suit + ", " + card.stringVal + " (" + card.val + ")");
            }
        }

        public Card PullCard()
        {
            Card topCard = cards[0];
            cards.Remove(topCard);
            return topCard;
        }

        public void Shuffle()
        {
            // iterate through each card and assign a random position for the card object
            Random rand = new Random();
            for (int cardPosition=0; cardPosition<cards.Count; cardPosition++)
            {
                int maxPosition = cards.Count;
                int randomPosition = rand.Next(0, maxPosition);
                Card cardAtPosition = cards[cardPosition];
                cards.RemoveAt(cardPosition);
                cards.Insert(randomPosition, cardAtPosition);
            }
        }

        public void CountCardTypes()
        {
            string[] suits = { "Heart", "Spade", "Diamond", "Club" };
            string[] values = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            string cardValue;
            foreach (string suit in suits)
            {
                for (int j = 0; j < 13; j++)
                {
                    cardValue = values[j];
                    int count = 0;
                    foreach (Card card in cards)
                    {
                        if (card.suit == suit && card.stringVal == cardValue)
                        {
                            count += 1;
                        }
                    }
                    Console.WriteLine("Card: " + suit + " (" + cardValue + ")" + " ... occurs " + count + " time(s)");
                }
            }
        }

        public void AddDiscard(Card discardedCard)
        {
            discardedCards.Add(discardedCard);
        }

        public Deck Reset()
        {
            cards.Clear();
            Deck newDeck = new Deck();
            return newDeck;
        }
    }

    public class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();

        public Player(string _name)
        {
            name = _name;
        }

        public Card Draw(Deck drawFromDeck)
        {
            Card drawnCard = drawFromDeck.PullCard();
            hand.Add(drawnCard);
            return drawnCard;
        }

        public void Draw_N_Cards(Deck drawFromDeck, int numCards)
        {
            for (int drawNum = 0; drawNum < numCards; drawNum++)
            {
                Draw(drawFromDeck);
            }
        }


        public void ShowHand()
        {
            foreach (Card card in hand)
            {
                Console.WriteLine(name + "'s card: " + card.suit + ", " + card.stringVal + " (" + card.val + ")");
            }
        }

        public void Discard(Deck discardToDeck, int cardAtIndex)
        {
            Card discardedCard = hand[cardAtIndex];
            discardToDeck.AddDiscard(discardedCard);
            hand.Remove(discardedCard);
            return;
        }
    }
}


