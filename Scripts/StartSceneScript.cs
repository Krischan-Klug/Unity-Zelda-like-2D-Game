using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public GameObject logo;
    public GameObject twoD;
    public GameObject pressStart;
    private AudioSource startSound;
    
    
    private bool readyStart = false;
    // Start is called before the first frame update
    void Start()
    {
        startSound = GetComponent<AudioSource>();
        StartCoroutine(LoadUI());

;
        
    }

    // Update is called once per frame
    void Update()
    {
        StartSceneStart();
    }

    public void StartSceneStart()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) )
        {
            if (readyStart)
            {
                StartCoroutine(EnterWorld());
                
            }
        }
    }



    private IEnumerator EnterWorld()
    {
        yield return null;
        startSound.Play();

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Overworld");

    }

    private IEnumerator LoadUI()
    {
        yield return new WaitForSeconds(2.5f);
        logo.SetActive(true);

        yield return new WaitForSeconds(2f);
        twoD.SetActive(true);

        yield return new WaitForSeconds(2f);
        pressStart.SetActive(true);
        readyStart = true;

    }
}
