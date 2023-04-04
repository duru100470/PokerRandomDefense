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
    public void Buy_WorkCorrectly_WhenCalled()
    {
        deck = new TestDeck();
        gameStats = new GameStats();
        market = new Market(gameStats, deck);

        gameStats.Gold.Value = 20;
        int gold = 20;

        foreach (var c in market.CardArray.Value)
        {
            Assert.NotNull(c);
        }
        Assert.Throws<IndexOutOfRangeException>(() => market.Buy(market.CardArray.Value.Count()));

        var test1 = market.CardArray.Value[0];
        var card = market.Buy(0);
        gold -= card.Price;

        Assert.NotNull(card);
        Assert.That(test1 == card);
        Assert.That(gold == gameStats.Gold.Value);

        card = market.Buy(0);
        Assert.IsNull(card);
        Assert.That(gold == gameStats.Gold.Value);

        gameStats.Gold.Value = 0;
        Assert.Throws<Market.NotEnoughGoldException>(() => market.Buy(1));
    }

    [Test]
    public void Reroll_WorkCorrectly_WhenCalled()
    {
        deck = new Deck();
        gameStats = new GameStats();
        market = new Market(gameStats, deck);

        gameStats.Gold.Value = 100;
        market.Buy(0);
        Assert.That(deck.Count == 52 - market.CardArray.Value.Count());

        Assert.Null(market.CardArray.Value[0]);
        market.Reroll();
        Assert.NotNull(market.CardArray.Value[0]);

        var test = market.CardArray.Value[1];
        Assert.That(deck.Count == 52 - market.CardArray.Value.Count() * 2);
        market.Reroll();
        Assert.That(test != market.CardArray.Value[1]);
        Assert.That(deck.Count == 52 - market.CardArray.Value.Count() * 3);

        gameStats.Gold.Value = 0;
        Assert.Throws<Market.NotEnoughGoldException>(() => market.Reroll());
    }
}
