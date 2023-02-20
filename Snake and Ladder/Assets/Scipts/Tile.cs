using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isLadderStart = false;
    public bool isSnakeMouth = false;
    public int index;
    public int finalTile;

    public void SetIndex(int i)
    {
        index = i;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i).ToString();
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SetLadderStart(int finalTile, int i)
    {
        isLadderStart = true;
        this.finalTile = finalTile;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "LS" + (i + 1).ToString();
    }

    public void SetLadderEnd(int i)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "LE" + (i + 1).ToString();
    }

    public void SetSnakeMouth(int snakeTail, int i)
    {
        this.finalTile = snakeTail;
        isSnakeMouth = true;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SS" + (i + 1).ToString();
    }

    public void SetSnakeTail(int i)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SE" + (i + 1).ToString();
    }
}
