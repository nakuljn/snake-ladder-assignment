using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] Dice dice;

    public void Generate()
    {
        board.Generate();
        dice.GenerateDice();
    }
}
