using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public AudioClip clip_menu;
    public Animator animator;
    [Space]
    public Button btn_play;
    public Button btn_tutorial;
    public Button btn_credit;
    public Button btn_lechegamer;
    public Button btn_sanmiguel;
    public Button btn_exit;

    private void Awake() 
    {
        AudioManager._.PlayMusic(clip_menu);
        FadeManager._.target=0;
    }   
    private void Start()
    {
        animator.SetTrigger("Display");
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        condition.Subscribe(ref btn_play, OnPress_Play);
        condition.Subscribe(ref btn_tutorial, OnPress_Tutorial);
        condition.Subscribe(ref btn_credit, OnPress_Credit);
        condition.Subscribe(ref btn_lechegamer, OnPress_LeChefGamer);
        condition.Subscribe(ref btn_sanmiguel, OnPress_SanMiguel);
        condition.Subscribe(ref btn_exit, OnPress_Exit);
    }
    private void OnPress_Play()
    {
        SceneManager.LoadScene("Game");
    }
    private void OnPress_Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    private void OnPress_Credit()
    {
        SceneManager.LoadScene("Creditos");
    }
    private void OnPress_LeChefGamer()
    {
        Application.OpenURL("https://www.twitch.tv/lechefgamer20");
    }
    private void OnPress_SanMiguel()
    {
        Application.OpenURL("https://www.sanmiguel.com/es/beerjam/");
    }
    private void OnPress_Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
