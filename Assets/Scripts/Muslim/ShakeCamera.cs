using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private float durationAfterRestart;
    [SerializeField] private float durationShake;
    [SerializeField] private float cameraPosX;
    [SerializeField] private float cameraPosY;
    
    private Camera _camera;
    private Vector3 _originalPosition;

    void Start()
    {
        _camera = Camera.main;
        _originalPosition = _camera.transform.position;
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
            cameraPosX = Random.Range(-0.3f, 0.3f);
            cameraPosY = Random.Range(-0.3f, 0.3f);
            _camera.transform.position = new Vector3(cameraPosX, cameraPosY, _originalPosition.z); 
            yield return new WaitForSeconds(durationAfterRestart);
        }
        _camera.transform.position = _originalPosition;
    }
}