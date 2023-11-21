using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HP = 3; // Az ellens�g hit pontjai
    public float knockbackForce = 100f; // Az ellens�g h�tra es�s�nek ereje
    public float knockbackDuration = 0.2f; // Mennyi ideig tart a h�tra es�s
    public SpriteRenderer spriteRenderer; // Hivatkoz�s a SpriteRenderer komponensre
    public Slider UI_HP;
    public Image UI_HP_bg;
    public Image UI_HP_fill;



    private Rigidbody2D rb;
    private Color originalColor;
    private bool isKnockedBack = false;
    bool JobbraEsik = true;
    void Start()
    {
        UI_HP.maxValue = HP;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Az eredeti sz�n elment�se
        //Refresh();
    }

    public void TakeDamage(int damage)
    {
        if (isKnockedBack) return; // Ha m�r h�tra esik, ne vegyen be tov�bbi sebz�st
        HP -= damage; // Cs�kkentj�k az HP-t
        Refresh();
        if (this.gameObject.tag == "Enemy") GetComponent<EnemyAttack>().Shoot();

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashColor());
            StartCoroutine(Knockback());
        }
    }

    private void Refresh()
    {
        UI_HP.value = HP;
        UI_HP_bg.color = new Color(0, 0, 0, 255);
        UI_HP_fill.color = new Color(255, 0, 0, 255);
    }

    private IEnumerator FlashColor()
    {
        spriteRenderer.color = Color.red; // �tv�lt�s piros sz�nre
        yield return new WaitForSeconds(0.1f); // V�runk egy r�vid ideig
        spriteRenderer.color = originalColor; // Vissza�ll�tjuk az eredeti sz�nt
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;
        rb.AddForce((JobbraEsik ? 1 : -1 ) * transform.right * knockbackForce); // H�tra es�s
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero; // Meg�ll�tjuk a mozg�st
        isKnockedBack = false;
    }

    private void Die()
    {
        // Ide j�het a hal�l logika, p�ld�ul anim�ci�, hang, stb.
        Destroy(gameObject); // Az ellens�g megsemmis�t�se
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("lovedek"))
        {
            TakeDamage(1); // Alap�rtelmezetten 1 sebz�st okoz
            JobbraEsik = (hitInfo.gameObject.transform.position.x < transform.position.x);
            Destroy(hitInfo.gameObject); // Megsemmis�ti a l�ved�ket
        }
    }
}