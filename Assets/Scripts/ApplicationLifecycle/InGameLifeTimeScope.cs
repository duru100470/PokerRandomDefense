using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InGameLifeTimeScope : LifetimeScope
{
    [SerializeField]
    EnemyFactory enemyFactory;
    [SerializeField]
    ProjectileFactory projectileFactory;
    [SerializeField]
    Player player;
    [SerializeField]
    UserInputSender userInputSender;

    protected override void Configure(IContainerBuilder builder)
    {
        // Initialize objects
        builder.Register<Deck>(Lifetime.Scoped).As<IDeck>();
        builder.Register<Market>(Lifetime.Scoped);
        builder.RegisterComponent<EnemyFactory>(enemyFactory);
        builder.RegisterComponent<ProjectileFactory>(projectileFactory);
        builder.RegisterComponent<Player>(player);
        builder.RegisterComponent<UserInputSender>(userInputSender);

        // Initialize views
    }
}
