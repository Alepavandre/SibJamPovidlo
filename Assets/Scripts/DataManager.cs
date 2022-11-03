using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int time = 0;
    public int hp = 100;
    
    void Awake()
    {
        Instance = this;
    }
}
