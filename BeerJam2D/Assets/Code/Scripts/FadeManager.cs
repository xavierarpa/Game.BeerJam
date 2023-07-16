using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager _;
    public Image img;
    public float target;
    public float speed;
    public Action<float> OnReachTarget;
    private void Awake()
    {
        _ = this;
        ChangeColorInstant();
    }
    void Update()
    {
        if(img.color.a == target)
        {

        }
        else
        {
            ChangeColor();

            // llega al target
            if (img.color.a == target)
            {
                // INVOKE!!!!
                OnReachTarget?.Invoke(target);
            }
        }
    }
    void ChangeColor()
    {
        var col = img.color;
        col.a = Mathf.MoveTowards(col.a, target, Time.deltaTime * speed);
        img.color = col;
    }
    void ChangeColorInstant()
    {
        var col = img.color;
        col.a = target;
        img.color = col;
    }
}
