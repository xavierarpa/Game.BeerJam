using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public int _index;
    public Animator[] animators;
    public Animator aux;

    private int index = 0;
    private bool canSlide = true;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && canSlide)
        {
            Slide(1);
        } /*else if (Input.GetKeyDown(KeyCode.LeftArrow) && canSlide)
        {
            Slide(-1);
        }*/
    }

    public void Slide(int dir)
    {


        /*if((index == 0 && dir == -1) || (index == 2 && dir == 1))
        {
            //Nada
        }*/
        if (index == 2 && dir == 1)
        {
            SceneManager.LoadScene("Menu");
        }
        //dir == -1: slide fuera (der)
        else if(dir == 1)
        {
            animators[index].SetTrigger("SlideOut");
            index++;
        }
        //dir == 1: slide dentro (izq)
        else if (dir == -1)
        {
            index--;
            animators[index].SetTrigger("SlideIn");

        }
        StartCoroutine(delay_to_slide());
    }

    IEnumerator delay_to_slide()
    {
        canSlide = false;
        yield return new WaitForSeconds(0.4f);
        canSlide = true;
    }

}
