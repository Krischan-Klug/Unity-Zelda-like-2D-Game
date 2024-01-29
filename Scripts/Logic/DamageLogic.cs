using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLogic : MonoBehaviour
{
    //Knockback
    public void Knockback(Vector2 hitPosition, Vector2 sendPosition, Rigidbody2D hitRb, float knockbackStrenght)
    {
        Vector2 knockbackDirection = (hitPosition - sendPosition).normalized;
        StartCoroutine(KnockbackCo(hitRb, knockbackDirection, knockbackStrenght));
    }

    private IEnumerator KnockbackCo(Rigidbody2D hitRb, Vector2 knockbackDirection, float knockbackStrenght)
    {
        yield return null;
        hitRb.velocity = knockbackDirection;
        hitRb.AddForce(knockbackDirection * knockbackStrenght, ForceMode2D.Force);
        Debug.Log("KNOCK ENEMY!");
        yield return new WaitForSeconds(0.4f);
        hitRb.velocity = Vector2.zero;
    }

    public int MaxLifeCountLimit(int lifeCount, int maxLifeCount)
    {
        if (lifeCount > maxLifeCount)
        {
            
            lifeCount = maxLifeCount;
            int newLifeCount = maxLifeCount;
            return newLifeCount;

        }
        return lifeCount;
    }

    public int AddLife(int lifeCount, int lifeToAdd)
    {
        int newLifeCount = lifeCount + lifeToAdd;
        return newLifeCount;

    }
    public int DoDamage(int lifeCount, int damage)
    {
        int newLifeCount = lifeCount - damage;
        return newLifeCount;
    }
}
