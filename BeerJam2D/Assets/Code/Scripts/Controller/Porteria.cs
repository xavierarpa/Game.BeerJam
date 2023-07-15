using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porteria : MonoBehaviour
{
    public CollisionController collision = default;
    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
        if (condition) collision.OnCollision += OnCollision;
        else collision.OnCollision -= OnCollision;
    }

    void OnCollision(Collision2D collision)
    {
        Debug.Log(this);
        
        if (collision.gameObject.tag.Equals("Pelota"))
        {

        }

    }
}
