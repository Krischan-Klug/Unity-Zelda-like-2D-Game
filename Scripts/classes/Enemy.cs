using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Mob Info")]
    [SerializeField] public int lifeCount;

    [Header("Mob Settings")]
    [SerializeField] private int maxLifeCount;
    [SerializeField] private int baseDamage;
    [SerializeField] private float knockbackStrenght = 150f;
    public AudioSource hitSource;

    [Header("References")]
    private DamageLogic damageLogic;
    public PlayerLogic PlayerLogic;
    private PlayerMovement pm;
    private Rigidbody2D playerRb;

    private Rigidbody2D enemyRb;

    private Vector2 mobPosition;
    private Vector2 playerPosition;




    // Start is called before the first frame update
    public void Start()
    {
        lifeCount = maxLifeCount;
        damageLogic = FindAnyObjectByType<DamageLogic>();
        enemyRb = GetComponent<Rigidbody2D>();
        PlayerLogic = FindAnyObjectByType<PlayerLogic>().GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    public void Update()
    {
        checkDeath();
        lifeCount = damageLogic.MaxLifeCountLimit(lifeCount, maxLifeCount);
    }

    private void checkDeath()
    {
        if (lifeCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Knockback for Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mobPosition = transform.position;
            playerPosition = collision.gameObject.transform.position;
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            pm = collision.gameObject.GetComponent<PlayerMovement>();

            damageLogic.Knockback(playerPosition, mobPosition, playerRb, knockbackStrenght);
            pm.Hit();
            PlayerLogic.lifeCount = damageLogic.DoDamage(PlayerLogic.lifeCount, baseDamage);
            enemyRb.velocity = Vector2.zero;
        }
    }
}
