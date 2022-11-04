using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int State { get; private set; }

    public int timerRequest = 10;
    public int timerCritical = 15;
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
        yield return new WaitForSeconds(timerRequest);
        StartCoroutine(nameof(Critical));
    }

    private IEnumerator Critical()
    {
        State = 4;
        yield return new WaitForSeconds(timerCritical);
        Death();
    }

    private void Death()
    {
        State = 0;
        Debug.Log(name + " died");
    }

    public void DoRequest()
    {
        StartCoroutine(nameof(Request));
    }

    public void StopRequest()
    {
        StopCoroutine(nameof(Request));
    }

    public void StopCritical()
    {
        StopCoroutine(nameof(Critical));
    }

    public void ChangeState(int newState)
    {
        if (State != 0)
        {
            State = newState;
        }
    }
}
