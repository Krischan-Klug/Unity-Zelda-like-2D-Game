using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    move,
    attack,
    roll,
    water,
    boat,
    hit, 
}

public enum GroundState
{
    Grounded,
    InWater,
    None
}

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float walkSpeed = 3f;
    public float MoveX;
    public float MoveY;
    public Vector2 Movement;
    public float rollForce = 2f;

    public PlayerState currentState;
    public GroundState groundState;

    private bool attacking = false;
    private bool rolling = false;
    public bool interact = false;

    public bool isInteracting = false;
    private bool stopMoving = true;

    public LayerMask groundLayer;
    public LayerMask waterLayer;
    public Transform stateTrigger; 

    private PlayerLogic logic;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioManager audioManager;


    void Start()
    {
        audioManager = GameObject.FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
        logic = FindAnyObjectByType<PlayerLogic>().GetComponent<PlayerLogic>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
        currentState = PlayerState.idle;
        //sword fix
        Movement = Movement + new Vector2(0.1f, 0.1f);
    }

    void Update()
    {
        GetInput();
        Attack();
        Move();
        Roll();
    }

    void Move()
    {
        if (Movement != Vector2.zero && currentState != PlayerState.hit)
        {

            if (logic.isDead == false)
             {
                stopMoving = false;
                rb.velocity = Movement.normalized * speed;
                animator.SetFloat("moveY", MoveY);
                animator.SetFloat("moveX", MoveX);

                animator.SetBool("moving", true);

             }

            if (currentState == PlayerState.idle)
             {
                currentState = PlayerState.move;
             }

        }

        MoveExit();

    }

    //Fixed Physic bugs and state bugs
    private void MoveExit()
    {

        if (Movement == Vector2.zero && stopMoving == false)
        {

            stopMoving = true;
            animator.SetBool("moving", false);

            //if (currentState == PlayerState.move || currentState != PlayerState.roll || currentState == PlayerState.attack ||currentState == PlayerState.water || currentState == PlayerState.boat)
            if (currentState != PlayerState.roll && currentState != PlayerState.hit)

            {
                rb.velocity = Vector2.zero;

                //if (currentState != PlayerState.water && currentState != PlayerState.boat && currentState != PlayerState.attack)
                if (currentState == PlayerState.move)
                {
                    currentState = PlayerState.idle;
                }
            }
        }
    }


    void GetInput()
    {
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
        attacking = Input.GetKeyDown(KeyCode.E);
        rolling = Input.GetKeyDown(KeyCode.Q);
        interact = Input.GetKeyDown(KeyCode.F); 

        Movement = new Vector2(MoveX, MoveY);

    }


    private void Attack()
    {
        if (attacking && currentState == PlayerState.idle || attacking && currentState == PlayerState.move)
        {
            StartCoroutine(AttackAnimation());
            audioManager.PlayAttackSound();
        }

    }
    private IEnumerator AttackAnimation()
    {
        currentState = PlayerState.attack;
        animator.SetBool("attacking", true);
        yield return null;

        speed = 2.5f;
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(0.4f);

        //water and boat Coroutine delay fix
        if (currentState != PlayerState.water)
        {
            currentState = PlayerState.idle;
            speed = walkSpeed;
        }

    }


    private void Roll()
    {
        if (rolling && currentState == PlayerState.move)
        {
            StartCoroutine(RollAnimation());
            audioManager.PlayRollSound();
        }

        if (currentState == PlayerState.roll)
        {
            rb.AddForce(Movement.normalized * rollForce, ForceMode2D.Impulse);
        }

    }

    private IEnumerator RollAnimation()
    {
        rb.velocity = Vector2.zero;
        currentState = PlayerState.roll;
        animator.SetBool("rolling", true);
        yield return null;

        animator.SetBool("rolling", false);
        yield return new WaitForSeconds(0.5f);

        //water and boat Coroutine delay fix
        if (currentState == PlayerState.roll)
        {
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( currentState == PlayerState.roll )
        {
            audioManager.PlayRollWallSound();
        }
    }


    public void InteractDelay()
    {
        StartCoroutine(InteractDelayCo());
    }

    private IEnumerator InteractDelayCo()
    {
        isInteracting = true;
        yield return new WaitForSeconds(1f);
        isInteracting = false;
    }


    public void Hit()
    {
        StartCoroutine(HitCo());
        audioManager.PlayHitSound();
    }

    private IEnumerator HitCo()
    {
        currentState = PlayerState.hit;
        yield return new WaitForSeconds(0.4f);

        if (currentState == PlayerState.hit)
        {
            currentState = PlayerState.idle;
        }
    }
}
