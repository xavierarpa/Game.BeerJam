using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    void Start()
    {
        FadeManager._.target = 0;
        Debug.Log("Gan� el " + GameManager._.lastWinner);
    }
}
