using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosManager : MonoBehaviour
{
    private void Start()
    {
        FadeManager._.target = 0;
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        condition.Subscribe(ref FadeManager._.OnReachTarget, OnReachTarget);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            OnPress_Any();
        }
    }
    private void OnPress_Any()
    {
        FadeManager._.target=1;
    }    
    private void OnReachTarget(float target)
    {
        if(target==0)
        {
            // a
        }
        else if (target == 1)
        {
            GoToMenu();
        }
    }
    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
