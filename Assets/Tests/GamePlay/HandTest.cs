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

public class HandTest
{
    private Hand hand;
    private Market market;
    private GameStats gameStats;

    [Test]
    public void SellCardTest()
    {
        gameStats = new GameStats();
        market = new Market(null, new Deck());
        hand = new Hand(gameStats);

        var card1 = new Card(Card.CardSuit.Clubs, 0);
        card1.Price = 4;

        hand.CardArray[0] = card1;
        gameStats.Gold = 0;

        hand.Sell(0);
        Assert.AreEqual(2, gameStats.Gold);
        Assert.Null(hand.CardArray[0]);

        var card2 = new Card(Card.CardSuit.Clubs, 0);
        card2.Price = 8;
        hand.CardArray[1] = card2;
        hand.Sell(1);
        Assert.AreEqual(6, gameStats.Gold);
        Assert.Null(hand.CardArray[1]);

        Assert.Throws<IndexOutOfRangeException>(() => hand.Sell(hand.CardArray.Count()));
    }
}
