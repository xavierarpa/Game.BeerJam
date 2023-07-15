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

    [Header("Controller")]
    [Space]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode hitKey;

    [Header("References")]
    [Space]
    public GameObject triggerObject;
    public TriggerController trigger;
    private bool CanHit => !triggerObject.activeInHierarchy;

    void Start()
    {
        triggerObject = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        triggerObject.SetActive(false);
    }
    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
        if (condition) trigger.OnTrigger += OnTrigger;
        else trigger.OnTrigger -= OnTrigger;
    }

    void OnTrigger(Collider2D collider)
    {
        // Play GIF animati

        if (collider.tag.Equals("Pelota"))
        {
            // play pop up animationif shock
            // TODO
            Debug.Log("pelota");
            triggerObject.SetActive(false);
            collider.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.right, transform.position);
        }
        
    }
    void Update()
    {
        // Update position
        transform.position += CalculatePosition();
        HitMovement();

    }
    private void OnDrawGizmosSelected()
    {
        //FRONT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMax, -yMax, 0));

        //BACK 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(xMin, yMax, 0), new Vector3(xMin, -yMax, 0));

    }
    private void HitMovement()
    {
        if (Input.GetKeyDown(hitKey) && CanHit)
        {
            StartCoroutine(trigger_duration());
        }
    }

    private Vector3 CalculatePosition()
    {
        Vector2 nPos = Vector2.zero;

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

        return (Vector3) nPos * Time.deltaTime;
    }

    IEnumerator trigger_duration()
    {
        triggerObject.SetActive(true);
        yield return new WaitForSeconds(triggerDuration);
        triggerObject.SetActive(false);
    }

}
