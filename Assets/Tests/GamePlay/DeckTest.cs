using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PokerRandomDefense.GamePlay;
using System.Linq;

public class DeckTest
{
    Deck deck;

    [SetUp]
    public void SetUp()
    {
        deck = new Deck();
    }

    [Test]
    public void CreateDeckTest()
    {
        Assert.NotNull(deck);

        deck.RefreshDeck();
        Assert.That(deck.Count == 52, $"Deck count is {deck.Count}");

        int[] numbers = Enumerable.Repeat<int>(0, 13).ToArray<int>();
        int[] suits = Enumerable.Repeat<int>(0, 4).ToArray<int>();

        for (int i = 0; i < 52; i++)
        {
            var card = deck.Draw();
            numbers[card.Index]++;
            suits[(int)card.Suit]++;
        }

        foreach (var i in numbers)
            Assert.That(i == 4, $"The number of num is {i}");
        foreach (var i in suits)
            Assert.That(i == 13, $"The number of suit is {i}");
    }

    [Test]
    public void DrawOrInsertDeckTest()
    {
        // Clear deck
        deck.RefreshDeck();

        var card1 = deck.Draw();
        var card2 = deck.Draw();
        var card3 = deck.Draw();
        var card4 = deck.Draw();

        Assert.NotNull(card1);
        Assert.NotNull(card2);
        Assert.NotNull(card3);
        Assert.NotNull(card4);

        Assert.That(deck.Count == 52 - 4, $"Deck count is {deck.Count}");

        deck.Insert(card1);
        deck.Insert(card2);

        Assert.That(deck.Count == 52 - 2, $"Deck count is {deck.Count}");
    }

    [Test]
    public void AutoRefreshDeckTest()
    {
        deck.RefreshDeck();

        for (int i = 0; i < 52; i++)
            deck.Draw();

        Assert.That(deck.Count == 0);

        var card = deck.Draw();

        Assert.NotNull(card);
        Assert.That(deck.Count == 51, $"Deck count is {deck.Count}");
    }
}
