using UnityEngine;

public class Ball : MonoBehaviour
{
    private static Ball _; 
    public Rigidbody2D rb;
    public TrailRenderer trail;
    public ParticleSystem ps_handicap;
    public ParticleSystem ps_impact;
    public float speed = 5f; // Velocidad de movimiento de la pelota
    public float fireTrailSpeed = 5f;


    [Header("Settings")]
    [Space]
    public int touches_to_reduce;
    public int count_touches_to_reduce;
    public float min_mass;
    public float base_mass;
    public float reductor_mass;

    void Awake()
    {
        _=this;
    }
    void Start()
    {
        LaunchBall();
    }
    private void Update()
    {
        trail.emitting = rb.velocity.magnitude >= fireTrailSpeed;
    }

    public void Reset()
    {
        rb.velocity=Vector2.zero;
        rb.position=Vector2.zero;
        transform.position = Vector3.zero;
        count_touches_to_reduce = 0;
        rb.mass = base_mass;
        ps_handicap.Stop();
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

    public static void PlayImpact()
    {
        _.ps_impact.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // collision
            if (touches_to_reduce >= count_touches_to_reduce)
            {
                count_touches_to_reduce++;
            }
            else
            {
                ps_handicap.Play();

                if(rb.mass > min_mass)
                {
                    rb.mass -= reductor_mass;
                    if(rb.mass < min_mass) 
                    {
                        rb.mass = min_mass;
                    }
                }
                else
                {
                    //Nada
                    rb.mass = min_mass;
                }
            }
        }

    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    // }
}
