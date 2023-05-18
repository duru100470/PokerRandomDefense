using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;

namespace PokerRandomDefense.GamePlay.Stats
{
    public class GameStats
    {
        public Data<int> Health { get; set; } = new Data<int>(20);
        public Data<int> Wave { get; set; } = new Data<int>(1);
        public Data<int> Gold { get; set; } = new Data<int>(20);
    }
}