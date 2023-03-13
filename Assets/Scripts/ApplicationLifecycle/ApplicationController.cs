using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using PokerRandomDefense.GamePlay.Stats;

namespace PokerRandomDefense.DI
{
    public class ApplicationController : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStats>(Lifetime.Singleton);
        }
    }
}
