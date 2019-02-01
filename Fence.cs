using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField]
    private int fence_HP_Max;
    [SerializeField]
    private int fence_HP_Current;
    // Start is called before the first frame update
    void Start()
    {
        fence_HP_Max = 30;
        fence_HP_Current = 1;
    }
    private void Update()
    {
        Debug.Log("fence HP: " + fence_HP_Current);
        Death();
    }
    void Death()
    {
        if(fence_HP_Current <= 0)
        {
            Debug.Log("Fence is down!");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            fence_HP_Current -= 1;
        }
    }
}
