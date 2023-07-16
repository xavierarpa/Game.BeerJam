using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GifComponent gif;
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnPressAnyKey();
        }
        else
        {
            //Nada
        }
    }
    private void Subscribe(bool condition)
    {
        condition.Subscribe(ref gif.OnGifEnd, OnGifEnd);
    }
    private void OnGifEnd() 
    {
        GoToMenu();
    }
    private void OnPressAnyKey() 
    {
        // GoToMenu();
    }
    public void GoToMenu() => SceneManager.LoadScene("Game");
    // public void GoToMenu() => SceneManager.LoadScene("Menu");
}
