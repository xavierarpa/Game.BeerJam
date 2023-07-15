using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pelota"))
        {
            gameObject.SetActive(false);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForceAtPosition(-rb.velocity, collision.contacts[0].normal);
        }
    }

    /*
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pelota"))
        {
            gameObject.SetActive(false);
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(rb.velocity, ForceMode2D.Impulse);
        }
    }
     */

}
