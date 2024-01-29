using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeItem : item
{

    new void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupTrigger"))
        {
            PlayerLogic.AddCoin(itemCount);
            pickupSound.Play();
            Destroy(gameObject);

        }
    }
}
