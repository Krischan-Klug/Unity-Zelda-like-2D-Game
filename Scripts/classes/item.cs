using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [Header("Main")]
    public int itemCount;
    public string audioSourceName;

    [Header("References")]
    public PlayerLogic PlayerLogic;
    public DamageLogic DamageLogic;
    public AudioSource pickupSound;


    public void Start()
    {
        PlayerLogic = FindAnyObjectByType<PlayerLogic>().GetComponent<PlayerLogic>();
        DamageLogic = FindAnyObjectByType<DamageLogic>().GetComponent<DamageLogic>();
        pickupSound = GameObject.Find(audioSourceName).GetComponent<AudioSource>();
    }



}
