using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public FadeManager f;
    public GifComponent gif;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        f.target=0;
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
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
        condition.Subscribe(ref FadeManager._.OnReachTarget, OnReachTarget);
    }
    public void OnReachTarget(float target)
    {
        if(target == 1)
        {
            GoToMenu();
        }
        else if( target == 0)
        {
            
        }
    }
    private void OnGifEnd() => ReadyToGoToMenu();
    private void OnPressAnyKey() => ReadyToGoToMenu();
    public void ReadyToGoToMenu() => FadeManager._.target=1;
    public void GoToMenu() => SceneManager.LoadScene("Menu");
}
