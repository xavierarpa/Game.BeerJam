using System;
using UnityEngine;
public class CriticalShotController : MonoBehaviour
{
    public Action OnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pelota"))
        {
            OnTrigger?.Invoke();
        }
    }
}