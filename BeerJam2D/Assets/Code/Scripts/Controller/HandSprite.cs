using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSprite : MonoBehaviour
{
    public Animator animator = default;
    [ContextMenu("Action")] public void Animate_Action() => animator.SetTrigger("Action");
    [ContextMenu("Idle")] public void Animate_Idle() => animator.SetTrigger("Idle");
}
