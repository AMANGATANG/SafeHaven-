using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private float xMax;
    [SerializeField] private float yMax;
    [SerializeField] private float xMin;
    [SerializeField] private float yMin;
    [SerializeField] private float yoffSet;
    [SerializeField] private float xoffSet;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x + xoffSet, xMin, xMax), Mathf.Clamp(player.position.y + yoffSet, yMin, yMax), transform.position.z);
    }
}
