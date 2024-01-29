using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class SlimeScript : Enemy
{
    public float speed = 3f; // Geschwindigkeit des Gegners
    public Transform target; // Der Spieler, dem der Gegner folgen soll
    public float followRadius = 6f; // Der maximale Abstand, in dem der Gegner dem Spieler folgen kann
    private Rigidbody2D EnemyRb;

    new void Start()
    {
        base.Start();
        EnemyRb = GetComponent<Rigidbody2D>();
    }
    new void Update()
    {
        base.Update();


    }

    private void Follow()
    {
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude <= followRadius)
        {
            direction.Normalize();

            // Verwende Rigidbody.MovePosition anstelle von transform.Translate
            EnemyRb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }
}