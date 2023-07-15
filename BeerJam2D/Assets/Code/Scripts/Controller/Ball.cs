using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public TrailRenderer trail;
    public float speed = 5f; // Velocidad de movimiento de la pelota
    
    public float fireTrailSpeed = 5f;

    void Start()
    {
        LaunchBall();
    }
    private void Update()
    {
        trail.emitting = rb.velocity.magnitude >= fireTrailSpeed;
    }

    public void LaunchBall()
    {
        // Lanzar la pelota en una dirección aleatoria
        float randomDirectionX = Random.Range(-1f, 1f);
        float randomDirectionY = Random.Range(-1f, 1f);
        Vector2 launchDirection = new Vector2(randomDirectionX, randomDirectionY);
        rb.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }
}
