using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] AttackSources;
    private int AttackIndex = 0;

    public AudioSource rollWallSource;
    public AudioSource[] RollSources;
    private int RollIndex = 0;


    public AudioSource[] HitSources;
    private int HitIndex = 0;

    public AudioSource deathSource1;
    public AudioSource deathSource2;

    public AudioSource oceanMusic;




    public void PlayAttackSound()
    {
  
        AttackSources[AttackIndex].Play();
        AttackIndex++;

        if (AttackIndex == AttackSources.Length)
        {
            AttackIndex = 0;
        }

    }

    public void PlayRollSound()
    {
        RollSources[RollIndex].Play();
        RollIndex++;

        if (RollIndex == RollSources.Length) 
        { 
            RollIndex = 0; 
        }

    }
    public void PlayRollWallSound()
    {
        rollWallSource.Play();

    }

    public void PlayHitSound()
    {

        HitSources[HitIndex].Play();
        HitIndex++;

        if(HitIndex == HitSources.Length)
        {
            HitIndex = 0;
        }
    }

    public void PlayDeathSound1()
    {
        deathSource1.Play();

    }
    public void PlayDeathSound2()
    {
        deathSource2.Play();
    }

    public void PlayOceanMusic()
    {
        oceanMusic.volume = 0;
        oceanMusic.Play();


        StartCoroutine(FadeIn(oceanMusic, 7f));


        
    }


    public void StopOceanMusic()
    {
        StartCoroutine(FadeOut(oceanMusic, 7f));


    }



    public IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        float endVolume = 0.2f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float elapsed = Time.time - startTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }

    public IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = 0.2f;
        float endVolume = 0f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float elapsed = Time.time - startTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }
}

