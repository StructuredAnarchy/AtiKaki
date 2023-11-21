using System;
using System.Collections;
using UnityEngine;

public class harcigomba : MonoBehaviour
{
    
    public float minTav;
    public float maxTav;


    public float knockbackStrength = 5f; // Az ellent�szl� er� nagys�ga
    bool sebzodik = false;

    public Transform player; // A j�t�kos Transformja, be kell �ll�tani az Inspectorban
    public float moveSpeed = 5f; // A mozg�s sebess�ge
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
        // Sz�mold ki a t�vols�got a j�t�kos �s a 'Harcigomba' k�z�tt
        float distance = Vector2.Distance(player.position, transform.position);
        //Debug.Log(player.position.x - transform.position.x);
        
        // Ha a t�vols�g 500 �s 2000 k�z�tt van, mozogj a j�t�kos fel�
        if (distance > minTav && distance < maxTav)
        {
            Vector2 direction = new Vector2(player.position.x - transform.position.x, transform.position.y).normalized;
            movement = direction;
            isMovingTowardPlayer = true;
        }
        else if (distance <= minTav && isMovingTowardPlayer)
        {
            // Ha m�r mozogt�l a j�t�kos fel�, �s a t�vols�g 500 al� esett, folytasd a mozg�st
            // Az aktu�lis mozg�si ir�ny megtart�s�val
        }        
        else
        {
            movement = Vector2.zero; // Ha nem a megadott tartom�nyban van, �llj meg
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
        if (collision.gameObject.CompareTag("Player")) // Felt�telezve, hogy a j�t�kosnak van egy "Player" tag-je
        {
            sebzodik = true;
            StartCoroutine(AttackRoutineg());
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            knockbackDir.y = 0f; // Null�zza a f�gg�leges komponenst, hogy csak v�zszintesen legyen visszal�k�d�s

            // A 'Harcigomba' ellent�szl�sa
            rb.velocity = new Vector2(-knockbackDir.x * knockbackStrength, rb.velocity.y);

            // J�t�kos ellent�szl�sa
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null) // Ellen�rizz�k, hogy van-e Rigidbody2D a j�t�kosoaaaaaan
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