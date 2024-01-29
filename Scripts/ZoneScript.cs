using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ZoneScript : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject music;

    public TMP_Text zoneText;
    public GameObject TextObject;

    public string zoneName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AudioTrigger"))
        {
            zoneText.text = zoneName;
            StartCoroutine(ZoneNameDelayCo());

            audioManager.StopOceanMusic();
            StartCoroutine(StartMusicDelayCo());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AudioTrigger"))
        {
            music.SetActive(false);
            audioManager.PlayOceanMusic();
            zoneText.text = "THE GREAT SEA";

            StartCoroutine(ZoneNameDelayCo());
        }
    }

    private IEnumerator StartMusicDelayCo()
    {
        yield return new WaitForSeconds(7f);
        music.SetActive(true);
    }

    private IEnumerator ZoneNameDelayCo()
    {
        TextObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        TextObject.SetActive(false);
    }

}
