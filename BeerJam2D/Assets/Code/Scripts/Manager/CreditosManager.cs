using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosManager : MonoBehaviour
{
    public FadeManager f;
    private void Start()
    {
        f.target = 0;
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        condition.Subscribe(ref f.OnReachTarget, OnReachTarget);
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
        f.target=1;
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
