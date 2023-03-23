using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PokerRandomDefense.GamePlay;
using UnityEngine;

public class TowerTest
{
    [Test]
    public void InsertCardTest()
    {
        Tower tower = new Tower();
        Card card1 = new Card(Card.CardSuit.Clubs, 0);
        Card card2 = new Card(Card.CardSuit.Hearts, 2);
        Card card3 = new Card(Card.CardSuit.Clubs, 4);
        Card card4 = new Card(Card.CardSuit.Diamonds, 9);
        Card card5 = new Card(Card.CardSuit.Spades, 12);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);

        Assert.AreEqual(3, tower.Cards.Count);

        tower.Insert(card4);

        // Redundancy check
        Assert.IsFalse(tower.TryInsert(card1));

        tower.Insert(card5);

        foreach (var card in tower.Cards)
        {
            Assert.NotNull(card);
        }

        Assert.IsFalse(tower.TryInsert(card1));
    }

    [Test]
    public void CardRankingTest()
    {
        Tower tower = new Tower();
        // High Card
        Card card1 = new Card(Card.CardSuit.Clubs, 0);
        Card card2 = new Card(Card.CardSuit.Hearts, 2);
        Card card3 = new Card(Card.CardSuit.Clubs, 4);
        Card card4 = new Card(Card.CardSuit.Diamonds, 9);
        Card card5 = new Card(Card.CardSuit.Spades, 12);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((0, 13), tower.GetRank());

        // High Card 2
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 10);

        tower.Insert(card1);

        Assert.AreEqual((0, 10), tower.GetRank());

        // One Pair
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 0);
        card2 = new Card(Card.CardSuit.Hearts, 6);
        card3 = new Card(Card.CardSuit.Clubs, 4);
        card4 = new Card(Card.CardSuit.Diamonds, 6);
        card5 = new Card(Card.CardSuit.Spades, 3);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((1, 6), tower.GetRank());

        // One Pair 2
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 5);
        card2 = new Card(Card.CardSuit.Hearts, 5);

        tower.Insert(card1);
        tower.Insert(card2);

        Assert.AreEqual((1, 5), tower.GetRank());

        // Two Pair
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 6);
        card2 = new Card(Card.CardSuit.Hearts, 3);
        card3 = new Card(Card.CardSuit.Clubs, 4);
        card4 = new Card(Card.CardSuit.Diamonds, 6);
        card5 = new Card(Card.CardSuit.Spades, 3);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((2, 6), tower.GetRank());

        // Three of a kind
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 9);
        card2 = new Card(Card.CardSuit.Hearts, 3);
        card3 = new Card(Card.CardSuit.Clubs, 3);
        card4 = new Card(Card.CardSuit.Diamonds, 6);
        card5 = new Card(Card.CardSuit.Spades, 3);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((3, 3), tower.GetRank());

        // Straight
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 3);
        card2 = new Card(Card.CardSuit.Hearts, 4);
        card3 = new Card(Card.CardSuit.Clubs, 5);
        card4 = new Card(Card.CardSuit.Diamonds, 6);
        card5 = new Card(Card.CardSuit.Spades, 7);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((4, 7), tower.GetRank());

        // Straight A K Q J 10
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 0);
        card2 = new Card(Card.CardSuit.Hearts, 9);
        card3 = new Card(Card.CardSuit.Clubs, 10);
        card4 = new Card(Card.CardSuit.Diamonds, 11);
        card5 = new Card(Card.CardSuit.Spades, 12);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((4, 13), tower.GetRank());

        // Flush
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 10);
        card2 = new Card(Card.CardSuit.Clubs, 4);
        card3 = new Card(Card.CardSuit.Clubs, 9);
        card4 = new Card(Card.CardSuit.Clubs, 6);
        card5 = new Card(Card.CardSuit.Clubs, 7);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((5, 10), tower.GetRank());

        // Full House
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 10);
        card2 = new Card(Card.CardSuit.Hearts, 10);
        card3 = new Card(Card.CardSuit.Diamonds, 10);
        card4 = new Card(Card.CardSuit.Diamonds, 3);
        card5 = new Card(Card.CardSuit.Spades, 3);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((6, 10), tower.GetRank());

        // Four of a Kind
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Clubs, 6);
        card2 = new Card(Card.CardSuit.Hearts, 6);
        card3 = new Card(Card.CardSuit.Diamonds, 6);
        card4 = new Card(Card.CardSuit.Diamonds, 3);
        card5 = new Card(Card.CardSuit.Spades, 6);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((7, 6), tower.GetRank());

        // Straight Flush
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Hearts, 7);
        card2 = new Card(Card.CardSuit.Hearts, 3);
        card3 = new Card(Card.CardSuit.Hearts, 4);
        card4 = new Card(Card.CardSuit.Hearts, 5);
        card5 = new Card(Card.CardSuit.Hearts, 6);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((8, 7), tower.GetRank());

        // Royal Straight Flush
        tower = new Tower();
        card1 = new Card(Card.CardSuit.Hearts, 9);
        card2 = new Card(Card.CardSuit.Hearts, 12);
        card3 = new Card(Card.CardSuit.Hearts, 11);
        card4 = new Card(Card.CardSuit.Hearts, 10);
        card5 = new Card(Card.CardSuit.Hearts, 0);

        tower.Insert(card1);
        tower.Insert(card2);
        tower.Insert(card3);
        tower.Insert(card4);
        tower.Insert(card5);

        Assert.AreEqual((8, 13), tower.GetRank());
    }
}
