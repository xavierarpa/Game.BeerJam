using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int _index;
    public Animator[] animators;

    private int index = 0;
    private bool canSlide = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && canSlide)
        {
            Slide(-1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && canSlide)
        {
            Slide(1);
        }
    }

    public void Slide(int dir)
    {
        if((index == 0 && dir == -1) || (index == 2 && dir == 1))
        {

        }
        StartCoroutine(delay_to_slide());
    }

    IEnumerator delay_to_slide()
    {
        canSlide = false;
        yield return new WaitForSeconds(0.32f);
        canSlide = true;
    }

}
