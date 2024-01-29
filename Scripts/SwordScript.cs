using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwordScript : MonoBehaviour
{
    private Rigidbody2D hitEnemyRb;
    private Enemy hitEnemy;
    private DamageLogic damageLogic;

    [SerializeField] private float knockbackStrenght = 200f;
    [SerializeField] private int swordDamage = 3;

    private void Start()
    {
        damageLogic = FindAnyObjectByType<DamageLogic>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 enemyPosition;
        Vector2 playerPosition;

        if (collision.CompareTag("breakable"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            playerPosition = transform.position;
            enemyPosition = collision.gameObject.transform.position;
            hitEnemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            hitEnemy = collision.gameObject.GetComponent<Enemy>();

            hitEnemy.hitSource.Play();
            damageLogic.Knockback(enemyPosition, playerPosition, hitEnemyRb, knockbackStrenght);
            hitEnemy.lifeCount = damageLogic.DoDamage(hitEnemy.lifeCount, swordDamage);

        }
    }



}
