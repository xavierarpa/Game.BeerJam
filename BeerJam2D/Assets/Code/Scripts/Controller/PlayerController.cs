using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float yMax = 3.4f;
    [SerializeField] private float xSpeed = 11f;
    [SerializeField] private float ySpeed = 8f;
    [SerializeField] private float triggerDuration = 0.3f;

    public float xMax = 4.8f;
    public float xMin = 0f;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode hitKey;

    private GameObject triggerObject;
    private bool can_hit = true;

    void Start()
    {
        triggerObject = transform.GetChild(0).gameObject;
        triggerObject.SetActive(false);
    }

    
    void Update()
    {
        // Update position
        transform.position += CalculatePosition();
        HitMovement();

    }

    private void HitMovement()
    {
        if (Input.GetKeyDown(hitKey) && can_hit)
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
        can_hit = false;
        triggerObject.SetActive(true);
        yield return new WaitForSeconds(triggerDuration);
        triggerObject.SetActive(false);
        can_hit = true;
    }

}
