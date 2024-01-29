using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class GroundLogic : MonoBehaviour
{
    private PlayerLogic PlayerLogic;
    private DamageLogic damageLogic;

    private GameObject Player;
    private PlayerMovement pm;
    private Animator animator;

    private float timer = 0f;
    [SerializeField] private float timerDuration = 24f;
    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerLogic = FindAnyObjectByType<PlayerLogic>().GetComponent<PlayerLogic>();
        damageLogic = FindAnyObjectByType<DamageLogic>().GetComponent<DamageLogic>();   

        Player = GameObject.FindGameObjectWithTag("Player");
        pm = Player.GetComponent<PlayerMovement>();
        animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDrowning();

    }

    private void CheckDrowning()
    {
        timerText.text = timerDuration.ToString();
        if (pm.groundState == GroundState.InWater)
        {

            timerDuration = timerDuration - Time.deltaTime;
            if (timer >= timerDuration)
            {
                PlayerLogic.lifeCount = damageLogic.DoDamage(PlayerLogic.lifeCount, 1);

            }
        }
    }

    public void LeaveWater()
    {
        //Trigger position bug fix
        Player.transform.position = new Vector2(Player.transform.position.x + (pm.Movement.x / 5), Player.transform.position.y + (pm.Movement.y / 5));

        pm.currentState = PlayerState.idle;
        pm.groundState = GroundState.Grounded;

        animator.SetBool("swimming", false);
        timerDuration = 24f;
        pm.speed = pm.walkSpeed;
    }

    public void EnterWater()
    {
        pm.currentState = PlayerState.water;
        pm.groundState = GroundState.InWater;

        animator.SetBool("swimming", true);
        pm.speed = 2.2f;
    }
}
