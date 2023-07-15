using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5f; // Velocidad de movimiento de la pelota

    void Start()
    {
        LaunchBall();
    }

    void LaunchBall()
    {
        // Lanzar la pelota en una dirección aleatoria
        float randomDirectionX = Random.Range(-1f, 1f);
        float randomDirectionY = Random.Range(-1f, 1f);
        Vector2 launchDirection = new Vector2(randomDirectionX, randomDirectionY);
        rb.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }
}
