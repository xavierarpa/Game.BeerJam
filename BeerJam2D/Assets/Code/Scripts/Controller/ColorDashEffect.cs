using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDashEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shader shader;

    public Color color;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shader = Shader.Find("GUI/Text Shader");
    }

    void ColorSprite()
    {
        spriteRenderer.material.shader = shader;
        spriteRenderer.color = color;
    }

    void Update()
    {
        ColorSprite();
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }
}
