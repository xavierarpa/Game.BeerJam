using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Settings")]
    [Space]
    public float yMax = default;
    public float xSpeed = default;
    public float ySpeed = default;
    public float triggerDuration = default;
    public float xMax = default;
    public float xMin = default;
    public float xLimit_critical = default;
    public int direction = default;
    public float dashExtraSpeed = default;
    public AudioClip[] normal_hit_sounds;
    public AudioClip[] critical_hit_sounds;


    [Header("Controller")]
    [Space]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode hitKey;

    [Header("References")]
    [Space]
    public GameObject obj_parent_triggers;
    public TriggerController trigger;
    public TriggerController trigger_critical;
    public HandSprite handSprite;
    public SpriteRenderer renderer_hand;
    private bool CanHit => !obj_parent_triggers.activeInHierarchy;
    private float dashXSpeed;
    private float dashYSpeed;

    void Start()
    {
        obj_parent_triggers.SetActive(false);
        dashXSpeed = xSpeed + dashExtraSpeed;
        dashYSpeed = ySpeed + dashExtraSpeed;
    }
    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
        condition.Subscribe(ref trigger.OnTrigger, OnTrigger);
        condition.Subscribe(ref trigger_critical.OnTrigger, OnTrigger_Critical);
    }
    void OnTrigger_Critical(Collider2D collider)
    {
        if (collider.tag.Equals("Pelota"))
        {
            AudioManager._.PlaySound(critical_hit_sounds[Random.Range(0, critical_hit_sounds.Length)]);
            Ball.PlayImpact();
            CameraController._.Shake();
            obj_parent_triggers.SetActive(false);
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            //stop ball
            rb.velocity = Vector2.zero;
            //apply impulse based on the collision's y position
            float yImpulse = -(obj_parent_triggers.transform.position.y - collider.ClosestPoint(obj_parent_triggers.transform.position).y);
            Vector2 mPos = new Vector2(direction, yImpulse);

            rb.AddForceAtPosition(mPos * 9 * 2f, collider.transform.position, ForceMode2D.Impulse);
        }
    }
    void OnTrigger(Collider2D collider)
    {
        // Play GIF animati

        if (collider.tag.Equals("Pelota"))
        {
            AudioManager._.PlaySound(normal_hit_sounds[Random.Range(0, normal_hit_sounds.Length)]);
            // play pop up animationif shock
            // TODO
            obj_parent_triggers.SetActive(false);
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            //stop ball
            rb.velocity = Vector2.zero;
            //apply impulse based on the collision's y position
            float yImpulse = -(obj_parent_triggers.transform.position.y - collider.ClosestPoint(obj_parent_triggers.transform.position).y);
            Vector2 mPos = new Vector2(direction, yImpulse);

            rb.AddForceAtPosition(mPos * 9, collider.transform.position, ForceMode2D.Impulse);
        }

    }
    void Update()
    {
        // Update position
        transform.position += CalculatePosition();
        HitMovement();

        // P1
        trigger_critical.gameObject.SetActive(transform.position.x >= -xLimit_critical && transform.position.x <= xLimit_critical);


        //Update view pos
        // Calcula la dirección desde el objeto hacia la posición objetivo
        // Vector3 directionToTarget = Vector3.zero - transform.position;
        // Rota el objeto hacia la posición objetivo
        if(transform.position.x>0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(-transform.position.y, -transform.position.x) * Mathf.Rad2Deg);
        }
    }
    private void OnDrawGizmosSelected()
    {
        //FRONT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMax, -yMax, 0));

        //BACK 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(xMin, yMax, 0), new Vector3(xMin, -yMax, 0));

        // CRITICAL LIMIT
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(xLimit_critical * -direction, yMax, 0), new Vector3(xLimit_critical * -direction, -yMax, 0));
    }
    private void HitMovement()
    {
        if (Input.GetKeyDown(hitKey) && CanHit)
        {
            handSprite.Animate_Action();
            StartCoroutine(trigger_duration());
        }
    }

    private Vector3 CalculatePosition()
    {
        Vector2 nPos = Vector2.zero;

        // retreat hand automatically
        // if it's player1
        if(direction == 1)
        {
            if (transform.position.x > xMin)
            {
                nPos.x -= direction;
            }
        } else //if it's player 2
        {
            if (transform.position.x < xMax)
            {
                nPos.x -= direction;
            }
        }





        if (GameManager._.bs_is_321.LastValue)
        {
            // Nada
            if (direction == 1)
            {
                if (transform.position.x > xMin)
                {
                    nPos.x -= direction;
                }
            }
            else //if it's player 2
            {
                if (transform.position.x < xMax)
                {
                    nPos.x -= direction;
                }
            }
        }
        else
        {
            //Horizontal movement
            if (Input.GetKey(leftKey))
            {
                // Check it is within range
                if (transform.position.x > xMin)
                {
                    nPos.x -= xSpeed;
                }
            }
            else if (Input.GetKey(rightKey))
            {
                // Check it is within range
                if (transform.position.x < xMax)
                {
                    nPos.x += xSpeed;

                }
            }
        }


        // Vertical movement
        if (Input.GetKey(upKey))
        {
            // Check it is within range
            if (transform.position.y < yMax)
            {
                nPos.y += ySpeed;
            }
        }
        else if (Input.GetKey(downKey))
        {
            // Check it is within range
            if (transform.position.y > -yMax)
            {
                nPos.y -= ySpeed;
            }
        }


        return (Vector3)nPos * Time.deltaTime;
    }
    IEnumerator trigger_duration()
    {
        obj_parent_triggers.SetActive(true);
        yield return new WaitForSeconds(triggerDuration);
        obj_parent_triggers.SetActive(false);
    }

}