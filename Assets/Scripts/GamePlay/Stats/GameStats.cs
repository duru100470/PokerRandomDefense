using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;

namespace PokerRandomDefense.GamePlay.Stats
{
    public class GameStats
    {
        public ReactData<int> Health { get; set; } = new ReactData<int>(20);
        public ReactData<int> Wave { get; set; } = new ReactData<int>(1);
        public ReactData<int> Gold { get; set; } = new ReactData<int>(200);
    }
}