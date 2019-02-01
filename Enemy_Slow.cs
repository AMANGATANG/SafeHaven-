using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Slow : MonoBehaviour
{

    [SerializeField]
    private Animator Enemy_Anim;
    [SerializeField]
    private int max_HP;
    [SerializeField]
    private int current_HP;
    [SerializeField]
    private int attackPower;
    [SerializeField]
    private bool isFaceRight;
    [Range(1, 5)]
    public float moveSpeed;

    private Player_Script target;
    private Transform target_Position;
    
    void Start()
    {
         moveSpeed = 1.10f;
         attackPower = 2;
         max_HP = 6;
         current_HP = max_HP;

        isFaceRight = false;
        target_Position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>();
    }
    // Update is called once per frame
    void Update()
    {
        //Update is Enemy is dead and AI path.

        Death();
        MoveState();
        Flip();

    }
    public void Flip()
    {
        if (target.transform.position.x > gameObject.transform.position.x && isFaceRight || target.transform.position.x < gameObject.transform.position.x && !isFaceRight)
        {
            isFaceRight = !isFaceRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    public void Death()
    {
        Debug.Log("Enemy HP:" + current_HP);
        if (current_HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Attack()
    {
        //This will play an animation that will also have a trigger collider
        // to be enabled.
        Enemy_Anim.Play("Skele_Attack");
    }
    public void damageTakenCalculator(int damage)
    {
        current_HP -= damage;
    }
    public void MoveState()
    {
            if (Vector2.Distance(transform.position, target_Position.position) > 2f)
            {

                transform.position = Vector2.MoveTowards(transform.position, target_Position.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                    //play Attack Method.
                    Debug.Log("Attack player.");
                    Attack();
                    //reset Timer
            }   
    }
    public int returnAttackPower()
    {
        return attackPower;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "player_Range")
        {
            Debug.Log("Enemy hit with projectile.");
            //Damage calculator 
            damageTakenCalculator(target.returnRangeDamage());
        }
        else if (collision.gameObject.tag == "player_Melee")
        {
            Debug.Log("Enemy hit with melee.");
            //Damage calculator 
            damageTakenCalculator(target.returnMeleeDamage());
        }
    }
}
