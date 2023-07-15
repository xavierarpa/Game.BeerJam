using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public Action OnTriggerVoid;
    public Action<Collider2D> OnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Se�orehhh");
        //OnTriggerVoid.Invoke();
        OnTrigger?.Invoke(collision);
    }
}
