using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gombabullet : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // Fel kell haszn�lni az "up" ir�nyt, mivel a l�ved�k rot�ci�j�t haszn�ljuk az ir�ny�t�shoz
    }
}
