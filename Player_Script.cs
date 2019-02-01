using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    private float fallMultiplyer = 2.5f;
    private float lowJumpMultiplyer = 2.0f;
    public GameObject projectialPref;
    public GameObject Enemy_Fast;
    public GameObject Enemy_Slow;
    private Animator player_Anim;

    [SerializeField]
    public Rigidbody2D rigB;

    [SerializeField]
    private int Player_HP_Max;
    [SerializeField]
    private int Player_HP_Current;
    [SerializeField]
    private int Projectile_Damage;
    [SerializeField]
    private int melee_Damage;

    [SerializeField]
    private float walk_Speed;
    [SerializeField]
    private float _drag;

    [SerializeField]
    private bool isfacedRight;
    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    Transform checkground;
    [SerializeField]
    Transform firePoint;

    [Range(1, 10)]
    public float jump_Speed;
    [Range(1, 5)]
    public float KnockBack;
    private float KnockBack_length;
    [Range(1, 5)]
    public float KnockBack_count;
    public bool KnockBack_right;

    [SerializeField]
    private bool canMove;
    Vector2 HorizontalMove;
    private float HorizMovement;
    private Enemy_Slow Enemy_S;
    private Enemy_Fast Enemy_F;
    void Start()
    {
        walk_Speed = 12.5f;
        Player_HP_Max = 12;
        Player_HP_Current = 6;
        Projectile_Damage = 1;
        melee_Damage = 2;
        _drag = 2.5f;
        KnockBack_length = 0.2f;
        isfacedRight = true;
        canMove = true;
        Enemy_S = Enemy_Slow.GetComponent<Enemy_Slow>();
        Enemy_F = Enemy_Fast.GetComponent<Enemy_Fast>();
        player_Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    void Awake()
    {
        rigB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player HP: " + Player_HP_Current);
        Debug.Log("IsGrounded: " + isGrounded);
        isGrounded = Physics2D.Linecast(transform.position, checkground.position, 1 << LayerMask.NameToLayer("Ground"));
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        player_Anim.SetBool("IsGrounded",isGrounded);

        if (canMove)
        {
            movemenet(HorizontalAxis);
            flipCharacter(HorizontalAxis);
            ShootProjectial();
            meleeAttack();
            jump();
            death();
        }
    }

    private void movemenet(float H_move_)
    {
        Vector2 H_move = new Vector2(H_move_, 0.0f);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        player_Anim.SetBool("IsMoving", H_move != Vector2.zero);
        

        if (left == true)
        {
            rigB.AddForce(H_move * walk_Speed);
            rigB.drag = _drag;
            Debug.Log("Left");
        }
        else if (right == true)
        {
            rigB.AddForce(H_move * walk_Speed);
            rigB.drag = _drag;
            Debug.Log("Right");
        }
    }
    private void jump()
    {
        //This will check if the player can Jump and will allow the player to jump

        if (Input.GetKey(KeyCode.Space) == true && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jump_Speed;
            if (rigB.velocity.y < 0)
            {
                rigB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
                Debug.Log("Player has jumped");
            }
            else if (rigB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rigB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplyer - 1) * Time.deltaTime;
            }
        }
    }
    private void death()
    {
        if (Player_HP_Current <= 0)
        {
            //This will take the player to the GameOver Screen
            Debug.Log("Player is Dead.");
            canMove = false;
        }
        else
        {
            Debug.Log("player is Alive");
        }
        //If the player's health is at zero. 
        //Go to Game Over Screen.
    }
    private void damageCalculator(int damageTaken)
    {
        Player_HP_Current -= damageTaken;
    }
    private void flipCharacter(float HorizontalMove_)
    {
        if (HorizontalMove_ > 0 && !isfacedRight || HorizontalMove_ < 0 && isfacedRight)
        {
            isfacedRight = !isfacedRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
    private void meleeAttack()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            player_Anim.Play("Attack");
        }
    }
    private void ShootProjectial()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            Instantiate(projectialPref, firePoint.position, firePoint.rotation);
        }
    }
    public int returnRangeDamage()
    {
        return Projectile_Damage;
    }
    public int returnMeleeDamage()
    {
        return melee_Damage;
    }
    public void setCanMove(bool switch_)
    {
        canMove = switch_;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy_Slow_Atk")
        {
            damageCalculator(Enemy_S.returnAttackPower());
            KnockBack_count = KnockBack_length;
            if (collision.transform.position.x < transform.position.x)
            {
                KnockBack_right = false;
                Player_KnockBack();
            }
            else
            {
                KnockBack_right = true;
                Player_KnockBack();
            }
        }
        else if(collision.gameObject.tag == "Enemy_Fast_Atk")
        {
            damageCalculator(Enemy_S.returnAttackPower());
            KnockBack_count = KnockBack_length;
            if (collision.transform.position.x < transform.position.x)
            {
                KnockBack_right = false;
                Player_KnockBack();
            }
            else
            {
                KnockBack_right = true;
                Player_KnockBack();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HP_Pack")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Player_HP_Current < Player_HP_Max)
                {
                    Debug.Log("You gain 6 HP");
                    Player_HP_Current += 3;
                    if (Player_HP_Current > Player_HP_Max)
                        Player_HP_Current = Player_HP_Max;
                    Destroy(collision.gameObject); 
                }
            }
        }
    }
    public void Player_KnockBack()
    {
        if(KnockBack_count > 0)
        {
          if(KnockBack_right)
                rigB.velocity = new Vector2(-KnockBack, KnockBack);
          if (!KnockBack_right)
                rigB.velocity = new Vector2(KnockBack, KnockBack);
            KnockBack_count -= Time.deltaTime;
        }
    }
}

   
