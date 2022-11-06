using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct Replics
{
    public string critical;
    public string thanks;
    public string wrongItem;
}

public class Unit : MonoBehaviour
{
    public int State { get; private set; }

    public int currentNeed = -1;
    //public Text bubbleNeedText;
    public Text bubbleCommentText;
    public Image bubbleNeed;
    public Replics replics;
    public Sprite[] sprites;
    public SpriteRenderer image;
    // Start is called before the first frame update
    void Start()
    {
        State = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Request()
    {
        ChangeState(3);
        //bubbleNeedText.text = currentNeed.ToString();
        DataManager.Instance.IsThisItemFamiliar(currentNeed);
        bubbleNeed.color = Color.white;
        bubbleNeed.sprite = DataManager.Instance.needs[currentNeed].sprite;
        yield return new WaitForSeconds(DataManager.Instance.timerRequest);
        StartCoroutine(nameof(Critical));
    }

    private IEnumerator Critical()
    {
        ChangeState(4);
        bubbleCommentText.text = replics.critical;
        yield return new WaitForSeconds(DataManager.Instance.timerCritical);
        Death();
    }

    private void Death()
    {
        ChangeState(0);
        bubbleCommentText.text = "X__X";
        DataManager.Instance.CheckUnitsStates();
    }

    public void DoRequest()
    {
        currentNeed = Random.Range(0, DataManager.Instance.levels[DataManager.Instance.level].itemsCount);
        //Debug.Log("current need: " + currentNeed.ToString());
        StartCoroutine(nameof(Request));
    }

    public void StopRequest()
    {
        StopCoroutine(nameof(Request));
        //bubbleNeedText.text = "";
        CleanBubbleNeed();
    }

    public void StopCritical()
    {
        StopCoroutine(nameof(Critical));
        //bubbleNeedText.text = "";
        CleanBubbleNeed();
    }

    public void ChangeState(int newState)
    {
        if (State != 0)
        {
            if (newState == 2 || newState == 1)
            {
                StopRequest();
                StopCritical();
                CleanBubbles();
            }
            State = newState;
            image.sprite = sprites[newState];
        }
    }

    public void GetItem(int item)
    {
        if (State == 0)
        {
            return;
        }
        if (item == currentNeed)
        {
            StopRequest();
            StopCritical();
            bubbleCommentText.text = replics.thanks;
            StopCoroutine(nameof(ClearText));
            StartCoroutine(nameof(ClearText));
            currentNeed = -1;
            ChangeState(2);
        }
        else
        {
            // Comment about wrong item
            bubbleCommentText.text = replics.wrongItem;
            StartCoroutine(nameof(ClearText));
        }
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSeconds(3f);
        bubbleCommentText.text = "";
    }

    private void CleanBubbleNeed()
    {
        bubbleNeed.color = new Color(0f, 0f, 0f, 0f);
    }

    public void CleanBubbles()
    {
        currentNeed = -1;
        CleanBubbleNeed();
        bubbleCommentText.text = "";
    }
}
