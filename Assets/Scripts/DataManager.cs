using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int currentTime = 0;
    public int needsDelay = 20;
    //public int hp = 100;
    public int needs;
    public int level = 1;
    public int timerRequest = 10;
    public int timerCritical = 15;
    public Unit[] units;
    private readonly List<Unit> unitsNoNeed = new List<Unit>();


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshUnitsList();
        StartCoroutine(nameof(NewNeed));
    }

    IEnumerator NewNeed()
    {
        while (true)
        {
            if (unitsNoNeed.Count != 0)
            {
                int n = Random.Range(0, unitsNoNeed.Count);
                unitsNoNeed[n].DoRequest();
            }
            RefreshUnitsList();
            yield return new WaitForSeconds(needsDelay);
        }
    }

    private void RefreshUnitsList()
    {
        unitsNoNeed.Clear();
        foreach (var unit in units)
        {
            if (unit.State == 2)
            {
                unitsNoNeed.Add(unit);
                //Debug.Log(unitsNoNeed.Count);
            }
        }
    }
}
