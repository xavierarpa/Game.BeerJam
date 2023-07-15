using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public TrailRenderer trail;
    public float speed = 5f; // Velocidad de movimiento de la pelota
    public float fireTrailSpeed = 5f;

    [Header("Settings")]
    [Space]
    public float min_mass;

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
        float randomDirectionX = MinMax(.5f, 1f);
        float randomDirectionY = MinMax(.1f, 1f);
        Vector2 launchDirection = new Vector2(randomDirectionX, randomDirectionY);
        rb.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }

    private float MinMax(float min, float max) 
    {
        return (Random.Range(0, 2) == 0 ? 1 : -1) * Random.Range(min,max);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision
    }
}
