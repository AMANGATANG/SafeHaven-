using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform[] sections;
    public float parallaxSpeed;
    private float veiwZone = 10f;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

    public float backgroundSize;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        sections = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            sections[i] = transform.GetChild(i);
        }
            leftIndex = 0;
            rightIndex = sections.Length - 1;
    }
    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * parallaxSpeed;
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x < (sections[leftIndex].transform.position.x + veiwZone))
        {
            ScrollLeft();
        }
        if (cameraTransform.position.x > (sections[rightIndex].transform.position.x - veiwZone))
        {
            SrcollRight();
        }
    }
    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        sections[rightIndex].position = Vector2.right * (sections[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = sections.Length - 1;
        }
    }
    private void SrcollRight()
    {
        int lastLeft = leftIndex;
        sections[leftIndex].position = Vector2.right * (sections[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == sections.Length)
        {
            leftIndex = 0;
        }
    }
}
