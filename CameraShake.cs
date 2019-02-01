using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    public Camera main_Cam;
    [SerializeField]
    private float shakeFactor = 0;

    void Awake()
    {
        if (main_Cam == null)
            main_Cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shake(0.01f, 0.2f);
        }
    }
    public void Shake(float amount_, float duration_)
    {
        shakeFactor = amount_;
        InvokeRepeating("BeginShake", 0.0f, 0.01f);
        Invoke("StopShake", duration_);
    }
    private void BeginShake()
    {
        if(shakeFactor > 0)
        {
            Vector3 camPos = main_Cam.transform.position;

            float offSetX = Random.value * shakeFactor * 2 - shakeFactor;
            float offSetY = Random.value * shakeFactor * 2 - shakeFactor;

            camPos.x += offSetX;
            camPos.y += offSetY;

            main_Cam.transform.position = camPos;
        }
    }
    private void StopShake()
    {
        CancelInvoke("BeginShake");

        main_Cam.transform.localPosition = Vector3.zero;
    }
}
