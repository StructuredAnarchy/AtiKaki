using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 5; // A j�t�kos eg�szs�ge
    public float invincibilityDuration = 2f; // Sebezhetetlens�g id�tartama
    public AudioClip damageSound; // Sebz�d�s hangja
    public AudioClip deathSound; // Hal�l hangja
    private AudioSource audioSource;

    private bool isInvincible = false;

    public Slider UI_HP;
    public GameObject GO_UI;

    void Start()
    {
        UI_HP.maxValue = health;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void TakeDamage(int damage)
    {
        
        if (isInvincible) return; // Ha a j�t�kos sebezhetetlen, ne vegyen sebz�st

        health -= damage; // Cs�kkenti az eg�szs�get a sebz�ssel
        RefreshUI();
        PlaySound(damageSound);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BecomeInvincible());
        }
    }

    private void RefreshUI()
    {
        UI_HP.value = health;
    }





    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        // Villog�s kezdete
        GetComponent<Animator>().SetTrigger("Hit");
        
        // Villog�s v�ge
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        // Itt lehet a hal�l logik�j�t megval�s�tani, p�ld�ul a j�t�k �jraind�t�sa vagy a hal�l anim�ci�ja
        GO_UI.gameObject.SetActive(true);
        PlaySound(deathSound);
        StartCoroutine(Kilepes());
        Debug.Log("A j�t�kos meghalt.");
    }
    private IEnumerator Kilepes()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("KurvaMenu");

    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(1); // Alap�rtelmezett sebz�s
            Destroy(hitInfo.gameObject); // A l�ved�k megsemmis�t�se
        }
    }
}

