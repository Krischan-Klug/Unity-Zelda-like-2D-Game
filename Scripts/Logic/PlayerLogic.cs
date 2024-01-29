using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    [Header("Main")]
    public int coinCount;
    public int lifeCount;
    public int maxLifeCount = 3;
    public bool isDead = false;

    [Header("References")]
    public DamageLogic damageLogic;
    public AudioManager audioManager;
    public PlayerMovement pm;

    public Animator animator;
    public GameObject gameOverUI;
    public TMP_Text lifeText;
    public TMP_Text rupeeText;

    private void Start()
    {
        lifeCount = maxLifeCount;     
    }

    private void Update()
    {
        UIUpdate();
        CheckGameover();
        //Limit lifecount
        lifeCount = damageLogic.MaxLifeCountLimit(lifeCount, maxLifeCount);
    }
    //FUNCTIONS
    public void AddCoin(int coinsToAdd)
    {
        coinCount = coinCount + coinsToAdd;
    }

    private void UIUpdate()
    {
        lifeText.text = lifeCount.ToString();
        rupeeText.text = coinCount.ToString();
    }

    private void CheckGameover()
    {
        if (lifeCount == 0 && !isDead)
        {
            StartCoroutine(GameOverCo());
        }
    }
    private IEnumerator GameOverCo()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1);

        audioManager.PlayDeathSound1();
        yield return new WaitForSeconds(2);

        gameOverUI.SetActive(true);
        audioManager.PlayDeathSound2();
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Overworld");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
