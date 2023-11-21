using System;
using System.Collections;
using UnityEngine;

public class harcigomba : MonoBehaviour
{
    
    public float minTav;
    public float maxTav;


    public float knockbackStrength = 5f; // Az ellentászló erõ nagysága
    bool sebzodik = false;

    public Transform player; // A játékos Transformja, be kell állítani az Inspectorban
    public float moveSpeed = 5f; // A mozgás sebessége
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovingTowardPlayer = false;

    private void Start()
    {
        //player = GlobalTarolo.Instance.Player.transform;
        rb = GetComponent<Rigidbody2D>();
        sebzodik = false;
        if (player == null) player = GlobalTarolo.Instance.Player.transform;
    }

    private void Update()
    {
        // Számold ki a távolságot a játékos és a 'Harcigomba' között
        float distance = Vector2.Distance(player.position, transform.position);
        //Debug.Log(player.position.x - transform.position.x);
        
        // Ha a távolság 500 és 2000 között van, mozogj a játékos felé
        if (distance > minTav && distance < maxTav)
        {
            Vector2 direction = new Vector2(player.position.x - transform.position.x, transform.position.y).normalized;
            movement = direction;
            isMovingTowardPlayer = true;
        }
        else if (distance <= minTav && isMovingTowardPlayer)
        {
            // Ha már mozogtál a játékos felé, és a távolság 500 alá esett, folytasd a mozgást
            // Az aktuális mozgási irány megtartásával
        }        
        else
        {
            movement = Vector2.zero; // Ha nem a megadott tartományban van, állj meg
            isMovingTowardPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        if(!sebzodik)
            MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Feltételezve, hogy a játékosnak van egy "Player" tag-je
        {
            sebzodik = true;
            StartCoroutine(AttackRoutineg());
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            knockbackDir.y = 0f; // Nullázza a függõleges komponenst, hogy csak vízszintesen legyen visszalökõdés

            // A 'Harcigomba' ellentászlása
            rb.velocity = new Vector2(-knockbackDir.x * knockbackStrength, rb.velocity.y);

            // Játékos ellentászlása
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null) // Ellenõrizzük, hogy van-e Rigidbody2D a játékosoaaaaaan
            {
                playerRb.velocity = new Vector2(knockbackDir.x * knockbackStrength, playerRb.velocity.y);
                collision.gameObject.SendMessage("TakeDamage", 1);
            }
        }
    }
    private IEnumerator AttackRoutineg()
    {
        
        yield return new WaitForSeconds(1f);
        sebzodik = false;
    }
}