using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform InnenLo;
    public float detectionRange = 500f;
    public int numberOfBullets = 10;
    public int pluszminusz = 2;
    public float spreadAngle = 100f;
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 1f;
    public bool Loves = false;
    bool sima = true;
    private AudioSource audioSource;
    public AudioClip shotaud;


    private Color originalColor;

    void Start()
    {
        //player = GlobalTarolo.Instance.Player.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = new Color(255, 255, 255, 255); // Az eredeti sz�n elment�se
        audioSource = GetComponent<AudioSource>();
        if (player == null) player = GlobalTarolo.Instance.Player.transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange && !Loves)
        {
            spriteRenderer.color = originalColor;
            Loves = true;
            StartCoroutine(AttackRoutine());
        }
    }

    public void Shoot()
    {
        StartCoroutine(FireBullets());
    }
    private IEnumerator AttackRoutine()
    {
        // K�ken villog� hat�s
        for (int i = 0; i < 20; i++)
        {
            if (sima) spriteRenderer.color = Color.blue;
            else spriteRenderer.color = originalColor;
            sima = !sima;
            yield return new WaitForSeconds(flashDuration/20);
        }
        spriteRenderer.color = originalColor;

        // L�ved�kek kil�v�se
        StartCoroutine(FireBullets());
    }

    private IEnumerator FireBullets()
    {
        int newnumber = numberOfBullets + UnityEngine.Random.Range(0-pluszminusz, pluszminusz);
        float startAngle = transform.eulerAngles.z - spreadAngle / 2;
        float angleStep = spreadAngle / (newnumber - 1);
        audioSource.PlayOneShot(shotaud);
        for (int i = 0; i < newnumber; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject bullet = Instantiate(bulletPrefab, InnenLo.position, rotation);
            Destroy(bullet, 5f);
            // A l�ved�khez hozz�rendelt Bullet szkript kezeli a mozg�s�t

            // A l�ved�kek k�z�tti kis k�sleltet�s
            yield return new WaitForSeconds(0.001f);
        }
        Loves = false;
    }
}
