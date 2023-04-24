using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UserInputSender : MonoBehaviour
{
    [Inject]
    private readonly Player _player;
    [Inject]
    private readonly Market _market;

    private int selected = 0;

    private void Update()
    {
        // For Debug
        if (Input.GetKeyDown(KeyCode.D))
            _market.Reroll();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _player.BuyCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _player.BuyCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _player.BuyCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _player.BuyCard(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            _player.BuyCard(4);

        if (Input.GetKeyDown(KeyCode.Q)) selected = 0;
        if (Input.GetKeyDown(KeyCode.W)) selected = 1;
        if (Input.GetKeyDown(KeyCode.E)) selected = 2;
        if (Input.GetKeyDown(KeyCode.R)) selected = 3;
        if (Input.GetKeyDown(KeyCode.T)) selected = 4;
        if (Input.GetKeyDown(KeyCode.Y)) selected = 5;
        if (Input.GetKeyDown(KeyCode.U)) selected = 6;

        if (Input.GetKeyDown(KeyCode.Z)) _player.InsertCard(0, selected);
        if (Input.GetKeyDown(KeyCode.X)) _player.InsertCard(1, selected);
        if (Input.GetKeyDown(KeyCode.C)) _player.InsertCard(2, selected);
        if (Input.GetKeyDown(KeyCode.V)) _player.InsertCard(3, selected);
        if (Input.GetKeyDown(KeyCode.B)) _player.InsertCard(4, selected);
    }
}
