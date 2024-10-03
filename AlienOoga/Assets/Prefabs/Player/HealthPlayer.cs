using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int health = 100;

    private float iFrameTimer = 0.5f;

    [SerializeField] private bool iFrame;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (iFrame == false)
        {
            if (other.CompareTag("BobHands"))
            {
                TakeDamage(10);
                StartCoroutine(InvincibilityFrames());
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {
        iFrame = true;
        yield return new WaitForSeconds(iFrameTimer);
        iFrame = false;
    }
}
