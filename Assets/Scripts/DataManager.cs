using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct Need
{
    public int index;
    public Sprite sprite;
    public Transform place;
    public ItemPickUp item;
}

[System.Serializable]
public struct Dialog
{
    public string replica;
    public int unit;
    public float replicDuration;
}

[System.Serializable]
public struct LevelStats
{
    public int itemsCount;
    public int duration;
    public int needsDelay;
    public int timerRequest;
    public int timerCritical;
    public Dialog[] dialogs;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int currentTime = 0;
    public float levelTime = 0f;
    public int needsDelay = 20;
    public Need[] needs;
    private ItemPickUp[] items;
    public Transform itemPrefab;
    public LevelStats[] levels;
    public int level = -1;
    public int timerRequest = 10;
    public int timerCritical = 15;
    public Unit[] units;
    private readonly List<Unit> unitsNoNeed = new List<Unit>();
    public List<int> familiarItems = new List<int>();
    private Transform[] itemsTransforms;
    public GameObject gameoverScreen;
    public bool canThrow = true;
    public bool roadToGameOver = false;
    public bool isGameOver = false;
    public ShakeCamera shaker;

    public Text[] comments;

    public AudioClip ambient;
    public AudioClip rythm;
    public AudioSource source;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UnpackItems();
        //StartCoroutine(nameof(NewNeed));
        NewLevel();
        StartCoroutine(nameof(Timer));
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
            }
        }
    }

    private void NewNeedsList()
    {
        int n = items.Length;
        needs = new Need[n];
        for (int i = 0; i < n; i++)
        {
            needs[i].index = items[i].index;
            needs[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            needs[i].place = items[i].transform;
            needs[i].item = items[i];
        }
    }

    private void UnpackItems()
    {
        int n = itemPrefab.childCount;
        items = new ItemPickUp[n];
        for (int i = 0; i < n; i++)
        {
            items[i] = itemPrefab.GetChild(i).GetComponent<ItemPickUp>();
        }
    }

    private void SpawnNewItems()
    {
        ClearItems();
        int n = levels[level].itemsCount;
        itemsTransforms = new Transform[n];
        for (int i = 0; i < n; i++)
        {
            //Debug.Log("spawn: " + i.ToString());
            itemsTransforms[i] = Instantiate(needs[i].item.gameObject, needs[i].place.position, Quaternion.identity, transform).transform;
            //newItem.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void ClearItems()
    {
        int n = transform.childCount - 1;
        //Debug.Log(n);
        for (int i = n; i >= 0; i--)
        {
            //Debug.Log("clear: " + i.ToString());
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void NewLevel()
    {
        StopCoroutine(nameof(NewNeed));
        levelTime = Time.time;
        //Debug.Log(levelTime);
        level++;
        needsDelay = levels[level].needsDelay;
        timerRequest = levels[level].timerRequest;
        timerCritical = levels[level].timerCritical;
        RefreshUnitsStates(2);
        RefreshUnitsList();
        NewNeedsList();
        SpawnNewItems();
        canThrow = true;
        shaker.Shake();
        source.clip = rythm;
        source.Play();
        StartCoroutine(nameof(NewNeed));
    }

    private void NewDialog()
    {
        StopCoroutine(nameof(NewNeed));
        RefreshUnitsStates(1);
        RefreshUnitsList();
        ClearItems();
        canThrow = false;
        source.clip = ambient;
        source.Play();
    }

    private void RefreshUnitsStates(int newState)
    {
        foreach (var unit in units)
        {
            unit.ChangeState(newState);
        }
    }

    private IEnumerator Timer()
    {
        for (int i = 0; i < levels.Length - 1; i++)
        {
            float time = 0f;
            while (time < levels[level].duration)
            {
                time = Time.time - levelTime;
                //Debug.Log(time);
                yield return new WaitForSeconds(1f);
            }
            NewDialog();
            int n = levels[level].dialogs.Length;
            for (int j = 0; j < n; j++)
            {
                // ���������� ������� � ������� �����
                //units[levels[level].dialogs[j].unit].bubbleCommentText.text = levels[level].dialogs[j].replica;
                comments[levels[level].dialogs[j].unit].text = levels[level].dialogs[j].replica;
                yield return new WaitForSeconds(levels[level].dialogs[j].replicDuration);
                comments[levels[level].dialogs[j].unit].text = "";
            }
            NewLevel();
        }
    }

    public bool IsThisItemFamiliar(int index)
    {
        //bool result = familiarItems.Contains(index);
        foreach (var item in familiarItems)
        {
            if (item == index)
            {
                return true;
            }
        }
        familiarItems.Add(index);
        //Debug.Log("Index: " + index.ToString());
        //transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        itemsTransforms[index].GetChild(0).gameObject.SetActive(true);
        return false;
    }

    public void CheckUnitsStates()
    {
        int n = 0;
        foreach (var unit in units)
        {
            if (unit.State == 0)
            {
                n++;
            }
        }
        if (n == 4)
        {
            GameOver();
        }
        if (n > 0)
        {
            timerRequest = 1;
            timerCritical = 1;
            needsDelay = 1;
            StopCoroutine(nameof(Timer));
            roadToGameOver = true;
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        gameoverScreen.SetActive(true);
        StopCoroutine(nameof(Timer));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
