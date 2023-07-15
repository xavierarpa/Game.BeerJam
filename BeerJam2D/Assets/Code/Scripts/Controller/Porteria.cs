using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porteria : MonoBehaviour
{
    public Action OnPelotaCollide;
    public CollisionController collision = default;
    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
        condition.Subscribe(ref collision.OnCollision, OnCollision);
    }
    void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Pelota"))
        {
            OnPelotaCollide?.Invoke();
        }
    }
}
