using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifComponent : MonoBehaviour
{
    public Action OnGifEnd;
    [field:SerializeField] public Image Image { get; private set;}
    [SerializeField] private int index;
    [SerializeField] private bool stopWhenEnd;
    [SerializeField, Min(1)] private int speed;
    [field:SerializeField] public List<Sprite> Sprites {get; private set;}
    
    private void Update()
    {
        if(Sprites.Count > 0)
        {
            if(index >= Sprites.Count)
            {
                OnGifEnd?.Invoke();
                if(stopWhenEnd)
                {
                    // 
                }
                else
                {
                    index = 0; // ~reset    
                }
            } 
            else if(Time.frameCount % speed == 0)
            {
                Image.sprite = Sprites[index];
                index++;
            }
        }
    }
}
