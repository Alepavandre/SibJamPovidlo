using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private float durationAfterRestart;
    [SerializeField] private float durationShake;
    [SerializeField] private float startPosRange;
    [SerializeField] private float endPosRange;
    [SerializeField] private new Camera camera;
    
    
    
    private float cameraPosX;
    private float cameraPosY;
    
    //private Camera _camera;
    private Vector3 _originalPosition;

    void Start()
    {
        //_camera = Camera.main;
        _originalPosition = camera.transform.position;
    }

    public void Shake()
    {
        StartCoroutine(_Shake());
    }

    IEnumerator _Shake()
    {
        float timeLeft = Time.time;

        while ((timeLeft + durationShake) > Time.time)
        {
            cameraPosX = Random.Range(startPosRange, endPosRange);
            cameraPosY = Random.Range(startPosRange, endPosRange);
            camera.transform.position = new Vector3(cameraPosX, cameraPosY, _originalPosition.z); 
            yield return new WaitForSeconds(durationAfterRestart);
        }
        camera.transform.position = _originalPosition;
    }
}