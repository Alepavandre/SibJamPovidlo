using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int currentTime = 0;
    public int needsDelay = 20;
    public int hp = 100;
    public int[] needs;
    public int level = 1;
    public Unit[] units;
    
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    IEnumerator NewNeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(needsDelay);
            int n = Random.Range(0, units.Length);
            units[n].DoRequest();
        }
    }
}
