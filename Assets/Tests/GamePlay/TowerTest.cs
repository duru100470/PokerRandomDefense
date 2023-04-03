using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PokerRandomDefense.GamePlay;
using UnityEngine;
using UnityEngine.TestTools;

public class TowerTest
{
    [Test]
    public void Insert_MultipleCard_AreInsertedSuccessfully()
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

    [UnityTest]
    public IEnumerator Rank_Cards_ReturnValidRank([ValueSource(nameof(rankTestCases))] object[] testCase)
    {
        var go = new GameObject("tower");
        var tower = go.AddComponent<Tower>();

        yield return null;

        List<Card> cards = (List<Card>)testCase[0];
        foreach (var card in cards)
            tower.Insert(card);

        Assert.AreEqual(((int, int))testCase[1], tower.Rank);
    }

    static object[] rankTestCases = new object[]
    {
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 0),
                new Card(Card.CardSuit.Hearts, 2),
                new Card(Card.CardSuit.Clubs, 4),
                new Card(Card.CardSuit.Diamonds, 9),
                new Card(Card.CardSuit.Spades, 12)
            },
            (0, 13)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 10)
            },
            (0, 10)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 0),
                new Card(Card.CardSuit.Hearts, 6),
                new Card(Card.CardSuit.Clubs, 4),
                new Card(Card.CardSuit.Diamonds, 6),
                new Card(Card.CardSuit.Spades, 3)
            },
            (1, 6)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 5),
                new Card(Card.CardSuit.Hearts, 5)
            },
            (1, 5)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 6),
                new Card(Card.CardSuit.Hearts, 3),
                new Card(Card.CardSuit.Clubs, 4),
                new Card(Card.CardSuit.Diamonds, 6),
                new Card(Card.CardSuit.Spades, 3)
            },
            (2, 6)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 9),
                new Card(Card.CardSuit.Hearts, 3),
                new Card(Card.CardSuit.Clubs, 3),
                new Card(Card.CardSuit.Diamonds, 6),
                new Card(Card.CardSuit.Spades, 3)

            },
            (3, 3)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 3),
                new Card(Card.CardSuit.Hearts, 4),
                new Card(Card.CardSuit.Clubs, 5),
                new Card(Card.CardSuit.Diamonds, 6),
                new Card(Card.CardSuit.Spades, 7)
            },
            (4, 7)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 0),
                new Card(Card.CardSuit.Hearts, 9),
                new Card(Card.CardSuit.Clubs, 10),
                new Card(Card.CardSuit.Diamonds, 11),
                new Card(Card.CardSuit.Spades, 12)
            },
            (4, 13)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 10),
                new Card(Card.CardSuit.Clubs, 4),
                new Card(Card.CardSuit.Clubs, 9),
                new Card(Card.CardSuit.Clubs, 6),
                new Card(Card.CardSuit.Clubs, 7)
            },
            (5, 10)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 10),
                new Card(Card.CardSuit.Hearts, 10),
                new Card(Card.CardSuit.Diamonds, 10),
                new Card(Card.CardSuit.Diamonds, 3),
                new Card(Card.CardSuit.Spades, 3)
            },
            (6, 10)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Clubs, 6),
                new Card(Card.CardSuit.Hearts, 6),
                new Card(Card.CardSuit.Diamonds, 6),
                new Card(Card.CardSuit.Diamonds, 3),
                new Card(Card.CardSuit.Spades, 6)
            },
            (7, 6)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Hearts, 7),
                new Card(Card.CardSuit.Hearts, 3),
                new Card(Card.CardSuit.Hearts, 4),
                new Card(Card.CardSuit.Hearts, 5),
                new Card(Card.CardSuit.Hearts, 6)
            },
            (8, 7)
        },
        new object[]
        {
            new List<Card>
            {
                new Card(Card.CardSuit.Hearts, 9),
                new Card(Card.CardSuit.Hearts, 12),
                new Card(Card.CardSuit.Hearts, 11),
                new Card(Card.CardSuit.Hearts, 10),
                new Card(Card.CardSuit.Hearts, 0)
            },
            (8, 13)
        },
    };
}
