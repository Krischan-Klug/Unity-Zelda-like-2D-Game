using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoatLogic : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement pm;
    public Camera playerCamera;

    private float groundCamSize = 6f;
    private float boatCamSize = 7.5f;
    public float camTransitionTime = 2f;

    private bool enterRange = false;

    private Transform playerStand;
    public CompositeCollider2D groundCollider;
    private GameObject playerTrigger;
    private Animator playerAnimator;
    private Animator boatAnimator;

    void Start()
    {
        playerTrigger = GameObject.Find("StateTrigger");
        playerStand = transform.Find("playerStand");
        groundCollider = GameObject.Find("Ground").GetComponent<CompositeCollider2D>();
        boatAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (pm != null)
        {
            if (pm.currentState == PlayerState.boat)
            {
                boatAnimator.SetFloat("moveX", pm.MoveX);
            }


            if (pm.interact && !pm.isInteracting)
            {
                if (enterRange && pm.currentState != PlayerState.boat)
                {
                    DriveBoat();
                }
                else if (pm.currentState == PlayerState.boat)
                {
                    ExitBoat();
                }
            }
        }


    }
    

    private void DriveBoat()
    {
        playerTrigger.SetActive(false);
        player.transform.position = playerStand.transform.position;
        transform.SetParent(player.transform);
        pm.speed = 7f;
        groundCollider.isTrigger = false;
        playerAnimator.SetBool("onBoat", true);
        pm.InteractDelay();
        playerCamera.orthographicSize = Mathf.Lerp(groundCamSize, boatCamSize, camTransitionTime);

        pm.currentState = PlayerState.boat;
    }


    private void ExitBoat()
    {
        pm.currentState = PlayerState.idle;

        transform.SetParent(null);
        pm.speed = pm.walkSpeed;
        groundCollider.isTrigger |= true;
        playerTrigger.SetActive(true);
        playerAnimator.SetBool("onBoat", false);
        pm.InteractDelay();
        playerCamera.orthographicSize = Mathf.Lerp(boatCamSize, groundCamSize, camTransitionTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            pm = player.GetComponent<PlayerMovement>();
            playerAnimator = player.GetComponent<Animator>();
            playerCamera = player.GetComponentInChildren<Camera>();

            enterRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            playerCamera = null;
            enterRange = false;
        }
    }

}
