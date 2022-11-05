using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int State { get; private set; }

    //public int timerRequest = 10;
    //public int timerCritical = 15;
    public int currentNeed = -1;

    public Text bubbleNeedText;
    public Text bubbleCommentText;
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
        State = 3;
        bubbleNeedText.text = currentNeed.ToString();
        yield return new WaitForSeconds(DataManager.Instance.timerRequest);
        StartCoroutine(nameof(Critical));
    }

    private IEnumerator Critical()
    {
        State = 4;
        bubbleNeedText.text = currentNeed.ToString() + ", СРОЧНА";
        yield return new WaitForSeconds(DataManager.Instance.timerCritical);
        Death();
    }

    private void Death()
    {
        State = 0;
        bubbleCommentText.text = "X__X";
        //Debug.Log(name + " died");
    }

    public void DoRequest()
    {
        currentNeed = Random.Range(0, DataManager.Instance.needs);
        StartCoroutine(nameof(Request));
    }

    public void StopRequest()
    {
        StopCoroutine(nameof(Request));
        bubbleNeedText.text = "";
    }

    public void StopCritical()
    {
        StopCoroutine(nameof(Critical));
        bubbleNeedText.text = "";
    }

    public void ChangeState(int newState)
    {
        if (State != 0)
        {
            State = newState;
        }
    }

    public void GetItem(int item)
    {
        if (item == currentNeed)
        {
            StopRequest();
            StopCritical();
            bubbleCommentText.text = "Так то лучше!";
            StopCoroutine(nameof(ClearText));
            StartCoroutine(nameof(ClearText));
            currentNeed = -1;
            State = 2;
        }
        else
        {
            // Comment about wrong item
            bubbleCommentText.text = "Это не то, дурында!";
            StartCoroutine(nameof(ClearText));
            Debug.Log("Wrong Item!");
        }
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSeconds(3f);
        bubbleCommentText.text = "";
    }
}
