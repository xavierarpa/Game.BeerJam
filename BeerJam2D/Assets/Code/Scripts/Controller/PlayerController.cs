using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Settings")]
    [Space]
    public float yMax = 3.4f;
    public float xSpeed = 11f;
    public float ySpeed = 8f;
    public float triggerDuration = 0.3f;
    public float xMax = 4.8f;
    public float xMin = 0f;
    public float xLimit_critical = 0f;
    public int direction = 1;
    public float dashExtraSpeed = 3f;

    [Header("Controller")]
    [Space]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode hitKey;
    public KeyCode dashKey;

    [Header("References")]
    [Space]
    public GameObject obj_parent_triggers;
    public TriggerController trigger;
    public TriggerController trigger_critical;
    public HandSprite handSprite;
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
            obj_parent_triggers.SetActive(false);
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            //stop ball
            rb.velocity = Vector2.zero;
            //apply impulse based on the collision's y position
            float yImpulse = -(obj_parent_triggers.transform.position.y - collider.ClosestPoint(obj_parent_triggers.transform.position).y);
            Vector2 mPos = new Vector2(direction, yImpulse);

            rb.AddForceAtPosition(mPos * 9 * 1.5F, collider.transform.position, ForceMode2D.Impulse);
        }
    }
    void OnTrigger(Collider2D collider)
    {
        // Play GIF animati

        if (collider.tag.Equals("Pelota"))
        {
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
        PerformDash();

        // P1
        trigger_critical.gameObject.SetActive(transform.position.x >= -xLimit_critical && transform.position.x >= -xLimit_critical);
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
        Gizmos.DrawLine(new Vector3(xLimit_critical, yMax, 0), new Vector3(xLimit_critical, -yMax, 0));
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

    private void PerformDash()
    {
        if (Input.GetKeyDown(dashKey))
        {

        }
    }

    IEnumerator trigger_duration()
    {
        obj_parent_triggers.SetActive(true);
        yield return new WaitForSeconds(triggerDuration);
        obj_parent_triggers.SetActive(false);
    }

}