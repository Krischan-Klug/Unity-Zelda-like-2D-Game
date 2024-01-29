using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    private PlayerMovement pm;

    public TMP_Text textBoxText;
    public GameObject textBox;

    private bool inRange = false;

    public string text = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && pm.interact && !pm.isInteracting)
        {
            textBox.SetActive(!textBox.activeSelf);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        textBoxText.text = text;
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.SetActive(false);
        inRange = false;
    }
}
