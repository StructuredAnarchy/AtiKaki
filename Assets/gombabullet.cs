using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gombabullet : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // Fel kell használni az "up" irányt, mivel a lövedék rotációját használjuk az irányításhoz
    }
}
