using System;
using UnityEngine;
public class CollisionController : MonoBehaviour
{
    public Action<Collision2D> OnCollision;
    private void OnCollisionEnter2D(Collision2D collision) => OnCollision?.Invoke(collision);
}