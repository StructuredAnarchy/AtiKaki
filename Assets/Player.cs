using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 5; // A játékos egészsége
    public float invincibilityDuration = 2f; // Sebezhetetlenség idõtartama
    public AudioClip damageSound; // Sebzõdés hangja
    public AudioClip deathSound; // Halál hangja
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
        
        if (isInvincible) return; // Ha a játékos sebezhetetlen, ne vegyen sebzést

        health -= damage; // Csökkenti az egészséget a sebzéssel
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
        // Villogás kezdete
        GetComponent<Animator>().SetTrigger("Hit");
        
        // Villogás vége
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        // Itt lehet a halál logikáját megvalósítani, például a játék újraindítása vagy a halál animációja
        GO_UI.gameObject.SetActive(true);
        PlaySound(deathSound);
        StartCoroutine(Kilepes());
        Debug.Log("A játékos meghalt.");
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
            TakeDamage(1); // Alapértelmezett sebzés
            Destroy(hitInfo.gameObject); // A lövedék megsemmisítése
        }
    }
}

