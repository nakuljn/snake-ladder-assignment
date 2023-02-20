using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputWindow : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClickOk()
    {
        string inputHeight = transform.GetChild(1).GetComponentInChildren<TMP_InputField>().text;
        string inputWidth = transform.GetChild(2).GetComponentInChildren<TMP_InputField>().text;
        string inputSnake = transform.GetChild(3).GetComponentInChildren<TMP_InputField>().text;
        string inputLadder = transform.GetChild(4).GetComponentInChildren<TMP_InputField>().text;

        bool checkHeight = int.TryParse(inputHeight, out int height);
        bool checkWidth = int.TryParse(inputWidth, out int width);
        bool checkSnake = int.TryParse(inputSnake, out int snake);
        bool checkLadder = int.TryParse(inputLadder, out int ladder);

        if (!checkHeight || !checkWidth || !checkSnake || !checkLadder)
        {
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Enter Valid Input";
        }

        else if(height <= 0 || width <= 0 || snake < 0 || ladder < 0)
        {
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Enter Valid Input";
        }

        else if(height*width > 250)
        {
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Board size is limited to 250 tiles";
        }

        else if(snake + ladder >= height*width / 2)
        {
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Too Many Snakes and Ladder";
        }
        else
        {

            Constants.height = height;
            Constants.width = width;
            Constants.snake = snake;
            Constants.ladder = ladder;

            gameManager.Generate();
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "";
            Hide();
        }

        
    }

    public void OnClickCancel()
    {
        Hide();
    }

}
