using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public Animator animator;
    public GameObject text_pl1;
    public GameObject text_pl2;
    void Start()
    {
        FadeManager._.target = 0;
        if(GameManager._.lastWinner == 1)
        {
            text_pl1.SetActive(true);
            text_pl2.SetActive(false);
            animator.SetTrigger("Player1");
        } else
        {
            text_pl2.SetActive(true);
            text_pl1.SetActive(false);
            animator.SetTrigger("Player2");
        }
    }
}
