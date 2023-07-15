using System;
using UnityEngine;
public class TriggerController : MonoBehaviour
{
    public Action<Collider2D> OnTrigger;
    private void OnTriggerEnter2D(Collider2D collision) => OnTrigger?.Invoke(collision);
}