using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PokerRandomDefense.GamePlay;
using System.Linq;
using VContainer;
using VContainer.Unity;
using PokerRandomDefense.GamePlay.Stats;
using System;

public class MarketTest
{
    private class TestDeck : IDeck
    {
        public int Count => throw new NotImplementedException();

        public Card Draw()
        {
            Card card = new Card(Card.CardSuit.Clubs, 0);
            card.Price = 4;
            return card;
        }

        public void Insert(Card card)
            => throw new NotImplementedException();
    }

    IDeck deck;
    GameStats gameStats;
    Market market;

    [Test]
    public void BuyCardInMarketTest()
    {
        deck = new TestDeck();
        gameStats = new GameStats();
        market = new Market(gameStats, deck);

        gameStats.Gold = 20;
        int gold = 20;

        foreach (var c in market.CardArray)
        {
            Assert.NotNull(c);
        }
        Assert.Throws<IndexOutOfRangeException>(() => market.Buy(6));

        var test1 = market.CardArray[0];
        var card = market.Buy(0);
        gold -= card.Price;

        Assert.NotNull(card);
        Assert.That(test1 == card);
        Assert.That(gold == gameStats.Gold);

        card = market.Buy(0);
        Assert.IsNull(card);
        Assert.That(gold == gameStats.Gold);

        gameStats.Gold = 0;
        Assert.Throws<Market.NotEnoughGoldException>(() => market.Buy(1));
    }

    [Test]
    public void RerollMarketTest()
    {
        deck = new Deck();
        gameStats = new GameStats();
        market = new Market(gameStats, deck);

        gameStats.Gold = 100;
        market.Buy(0);
        Assert.That(deck.Count == 52 - 5);

        Assert.Null(market.CardArray[0]);
        market.Reroll();
        Assert.NotNull(market.CardArray[0]);

        var test = market.CardArray[1];
        Assert.That(deck.Count == 52 - 10);
        market.Reroll();
        Assert.That(test != market.CardArray[1]);
        Assert.That(deck.Count == 52 - 15);

        gameStats.Gold = 0;
        Assert.Throws<Market.NotEnoughGoldException>(() => market.Reroll());
    }
}
