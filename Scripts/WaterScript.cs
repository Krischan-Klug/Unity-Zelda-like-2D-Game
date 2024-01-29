using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class WaterScript : MonoBehaviour
{
    private GroundLogic groundLogic;

    
    // Start is called before the first frame update
    void Start()
    {
        groundLogic = FindAnyObjectByType<GroundLogic>().GetComponent<GroundLogic>();

    }

    // Update is called once per frame
    void Update()
    {     

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StateTrigger"))
        {
            groundLogic.EnterWater();
        }
                 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StateTrigger"))
        {
            
            groundLogic.LeaveWater();
        }

    }




}


