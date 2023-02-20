using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependency : MonoBehaviour
{
    public static Dependency Instance;

    public GameObject   GamePlayCanvas;
    public GameObject   MenuCanvas;
    public Board        Board;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
