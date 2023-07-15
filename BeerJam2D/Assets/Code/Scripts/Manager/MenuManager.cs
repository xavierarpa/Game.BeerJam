using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public AudioClip clip_menu;
    private void Awake() 
    {
        AudioManager._.PlayMusic(clip_menu);
    }   
}
