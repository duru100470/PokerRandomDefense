using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using PokerRandomDefense.Infrastructure;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PokerRandomDefense.DI
{
    public class InGameScope : LifetimeScope
    {
        [SerializeField]
        private ObjectPoolingSystem objectPoolingSystem;
        [SerializeField]
        private Player player;
        [SerializeField]
        private UserInputSender userInputSender;
        [SerializeField]
        private WaveSystem waveSystem;

        protected override void Configure(IContainerBuilder builder)
        {
            // Initialize objects
            builder.Register<Deck>(Lifetime.Scoped).As<IDeck>();
            builder.Register<Market>(Lifetime.Scoped);
            builder.RegisterComponent<ObjectPoolingSystem>(objectPoolingSystem);
            builder.RegisterComponent<Player>(player);
            builder.RegisterComponent<UserInputSender>(userInputSender);
            builder.RegisterComponent<WaveSystem>(waveSystem);

            // Initialize views
        }
    }
}