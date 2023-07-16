using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public AudioClip clip_menu;
    public Animator animator;
    private void Awake() 
    {
        AudioManager._.PlayMusic(clip_menu);
        FadeManager._.target=0;
    }   
    private void Start()
    {
        animator.SetTrigger("Display");
    }
}
