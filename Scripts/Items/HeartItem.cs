using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : item
{
    new void Start()
    {
        base.Start();
        itemCount = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickupTrigger"))
        {
            PlayerLogic.lifeCount = DamageLogic.AddLife(PlayerLogic.lifeCount, itemCount);
            pickupSound.Play();
            Destroy(gameObject);
        }
    }
}
