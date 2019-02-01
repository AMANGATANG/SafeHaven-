using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectial_Script : MonoBehaviour {

    public Rigidbody2D projectile_Pref_RigB;
    [SerializeField]    
    private float speed = 25.0f;

	void Start () {
        projectile_Pref_RigB.velocity = transform.right * speed;
        Destroy(gameObject, 0.15f);
    }
    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Debug.Log(gameObject.name);
        if (hitinfo.gameObject.tag == "Enemy")
        {
            Debug.Log("hit Enemy");
            Destroy(gameObject);
        }
    }
}
