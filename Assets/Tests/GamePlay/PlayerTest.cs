using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokerRandomDefense.GamePlay;
using NUnit.Framework;
using System;
using VContainer;
using VContainer.Unity;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine.TestTools;
using System.Linq;

public class PlayerTest
{
    private class TestLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStats>(Lifetime.Singleton);
            builder.Register<Deck>(Lifetime.Scoped).As<IDeck>();
            builder.Register<Market>(Lifetime.Scoped);
        }
    }

    GameStats gameStats;
    Player player;
    LifetimeScope scope;

    [SetUp]
    public void SetUp()
    {
        var lifeTime = new GameObject("LifeTimeScope");
        scope = lifeTime.AddComponent<TestLifeTimeScope>();
        scope.Build();

        var go = new GameObject("Player");
        player = go.AddComponent<Player>();

        scope.Container.Inject(player);
        gameStats = scope.Container.Resolve<GameStats>();
    }

    [UnityTest]
    public IEnumerator SellCard_WorkCorrectly_WhenCalled()
    {
        yield return null;

        var card1 = new Card(Card.CardSuit.Clubs, 0);
        card1.Price = 4;

        player.CardArray[0] = card1;
        gameStats.Gold.Value = 0;

        player.SellCard(0);
        Assert.AreEqual(2, gameStats.Gold);
        Assert.Null(player.CardArray[0]);

        var card2 = new Card(Card.CardSuit.Clubs, 0);
        card2.Price = 8;
        player.CardArray[1] = card2;
        player.SellCard(1);
        Assert.AreEqual(6, gameStats.Gold);
        Assert.Null(player.CardArray[1]);

        Assert.Throws<IndexOutOfRangeException>(() => player.SellCard(player.CardArray.Count()));
    }
}
