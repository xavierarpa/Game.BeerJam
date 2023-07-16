using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneAnimationFunctions : MonoBehaviour
{

    public void FadeToEat()
    {
        FadeManager._.target = 1;
    }

    public void ReturnToMenu() => SceneManager.LoadScene("Menu");
}
