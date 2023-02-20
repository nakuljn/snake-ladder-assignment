using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    public void GenerateDice()
    {
        gameObject.SetActive(true);

        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            int randomDice = Random.Range(1, 7);
            Dependency.Instance.Board.DiceClicked(randomDice);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomDice.ToString();
        });
    }
}
