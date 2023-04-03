using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UserInputSender : MonoBehaviour
{
    [Inject]
    private Player _player;
    [Inject]
    private Market _market;

    private void Update()
    {
        
    }
}
