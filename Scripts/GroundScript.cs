using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private GroundLogic waterLogic;

    void Start()
    {
        waterLogic = FindAnyObjectByType<GroundLogic>().GetComponent<GroundLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StateTrigger"))
        {
            waterLogic.LeaveWater();
        }
    }
}
