using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public struct BoardSettings
{
    public int height;
    public int width;
    public int snake;
    public int ladder;
}

public class Board : MonoBehaviour
{
    private BoardSettings   settings;
    private GridLayoutGroup gridLayout;
    private List<Tile>      boardTiles;
    private List<int>       ladders;
    private List<int>       snakes;
    private Player          player;

    private const float WIDTH = 1620.0f;
    private const float HEIGHT = 1080.0f;
    private const float SPACING = 10.0f;

    public void Generate()
    {

        settings = new BoardSettings
        {
            height = Constants.height,
            width = Constants.width,
            snake = Constants.snake,
            ladder = Constants.ladder
        };


        GenerateBoard();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        GameObject playerObject = Instantiate(Resources.Load<GameObject>(Constants.playerPrefab), boardTiles[0].transform);
        playerObject.GetComponent<RectTransform>().sizeDelta = GetCellSize() / 2;
        player = playerObject.AddComponent<Player>();
        player.currentTile = boardTiles[0].index;
    }

    public void DiceClicked(int dice)
    {
        int targetTile = dice + player.currentTile;
        if(targetTile > settings.height * settings.width)
        {
            return;
        }


        Tile tile = GetTargetTile(targetTile);
        if(tile == null)
        {
            return;
        }

        player.transform.SetParent(tile.transform);
        player.currentTile = targetTile;
        player.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    private Tile GetTargetTile(int targetTile)
    {
        Tile tile = null;
        foreach (Tile t in boardTiles)
        {
            if (t.index == targetTile)
            {
                tile = t;
                if (t.isLadderStart || t.isSnakeMouth)
                {
                    return GetTargetTile(t.finalTile);
                }
            }
        }
        return tile;
    }

    public void SetBoardSettings(BoardSettings settings)
    {
        this.settings = settings;
    }

    private void GenerateBoard()
    {
        boardTiles                  = new List<Tile>();
        ladders                     = new List<int>();
        snakes                      = new List<int>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraint       = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.cellSize         = GetCellSize();
        gridLayout.constraintCount  = settings.width;

        for(int i=0; i<settings.height; i++)
        {
            for(int j=0; j<settings.width; j++)
            {
                GameObject tileObject   = Instantiate(Resources.Load<GameObject>(Constants.tilePrefab), transform);
                Tile tile               = tileObject.AddComponent<Tile>();
                tile.SetIndex(GetCellIndex(i, j));
                boardTiles.Add(tile);
            }
        }

        for(int i=0; i<settings.ladder; i++)
        {
            GenerateLadderAndSnakes(true);
        }

        for(int i=0; i<settings.ladder; i++)
        {
            foreach(Tile t in boardTiles)
            {
                if(t.index == ladders[i*2])
                {
                    t.SetLadderStart(ladders[i*2 + 1], i);
                    t.transform.GetComponent<Image>().color = new Color(116f/255f, 251/255f, 98/255f, 1);
                }
                if (t.index == ladders[i*2 + 1])
                {
                    t.SetLadderEnd(i);
                    t.transform.GetComponent<Image>().color = new Color(35/255f, 190/255f, 13/255f, 1);
                }
            }
        }

        for(int i=0; i<settings.snake; i++)
        {
            GenerateLadderAndSnakes(false);
        }

        for(int i=0; i<settings.snake; i++)
        {
            foreach(Tile t in boardTiles)
            {
                if (t.index == snakes[i * 2])
                {
                    t.SetSnakeMouth(snakes[i * 2 + 1], i);
                    t.transform.GetComponent<Image>().color = new Color(255/255f, 31/255f, 31/255f);
                }
                if (t.index == snakes[i * 2 + 1])
                {
                    t.SetSnakeTail(i);
                    t.transform.GetComponent<Image>().color = new Color(255/255f, 98/255f, 98/255f);
                }
            }
        }
    }



    private Vector2 GetCellSize()
    {
        float x = (WIDTH - (settings.width - 1) * SPACING) / settings.width;
        float y = (HEIGHT - (settings.height - 1) * SPACING) / settings.height;
        return new Vector2(x, y);
        //if (settings.width > settings.height)
        //{
        //    return (HEIGHT - (settings.width - 1) * SPACING) /  settings.width;
        //}
        //return (HEIGHT - (settings.height - 1) * SPACING) / settings.height;
    }

    private int GetCellIndex(int i, int j)
    {
        if (i % 2 != 0)
        {
            return ((i + 1) * settings.width) + - j;
        }
        else
        {
            return i * settings.width + j + 1;
        }
    }

    private void GenerateLadderAndSnakes(bool isLadder)
    {
        int lowerLevel = Random.Range(0, settings.height - 1);
        int upperLevel = Random.Range(lowerLevel + 1, settings.height);

        int lowerLevelPosition = Random.Range(lowerLevel == 0 ? 1 : 0, settings.width);
        int upperLevelPosition = Random.Range(0, upperLevel == settings.height - 1 ? settings.width - 1 : settings.width);

        int lowerCellIndex = GetCellIndex(lowerLevel, lowerLevelPosition);
        int upperCellIndex = GetCellIndex(upperLevel, upperLevelPosition);

        if (CheckIfLadderOrSnakeExist(lowerCellIndex, upperCellIndex))
        {
            GenerateLadderAndSnakes(isLadder);
            return;
        }

        if (isLadder)
        {
            ladders.Add(lowerCellIndex);
            ladders.Add(upperCellIndex);
        }
        else
        {
            snakes.Add(upperCellIndex);
            snakes.Add(lowerCellIndex);
        }
    }

    private bool CheckIfLadderOrSnakeExist(int i, int j)
    {
        return (ladders.Contains(i) || ladders.Contains(j) || snakes.Contains(i) || snakes.Contains(j));
    }
}
