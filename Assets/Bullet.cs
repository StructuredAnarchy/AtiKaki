using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // A lövedék sebessége
    public float maxLifetime = 5f; // A lövedék maximális élettartama
    public float rotationSpeed = 100f; // Forgási sebesség fok per másodperc

void Update()
{
    // Forgatás a Z tengely körül, a deltaTime segítségével az idővel arányosan
    transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
}
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Az egér pozíciójának lekérdezése
        mousePosition.z = 0; // Biztosítjuk, hogy a Z koordináta 0 legyen, mert 2D-s játékban vagyunk

        moveDirection = (mousePosition - transform.position).normalized; // Az irány kiszámítása
        rb.velocity = moveDirection * speed; // A lövedék sebességének beállítása
        
        Destroy(gameObject, maxLifetime); // A lövedék megsemmisítése a megadott idő után
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Itt lehet kezelni az ütközéseket

        // Például, ha az ellenség tagje "Enemy"
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            // Ide jöhet a logika, hogy mit csináljon, ha ellenségbe ütközik
        }

        Destroy(gameObject); // Megsemmisíti a lövedéket, ha bármi másba ütközik
    }
}
