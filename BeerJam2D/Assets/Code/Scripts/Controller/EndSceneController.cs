using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    public Animator animator;
    public GameObject text_pl1;
    public GameObject text_pl2;
    public Image plate;
    public Sprite[] plate_sprites;
    
    
    void Start()
    {
        FadeManager._.target = 0;
        //if(GameManager._.lastWinner == 1)
        if(true)
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
        SetPlateSprite();
    }

    void SetPlateSprite()
    {
        int id = PlayerPrefs.GetInt("LastPlato", 0);
        id++;
        if(id >= plate_sprites.Length) id = 0;
        PlayerPrefs.SetInt("LastPlato", id);
        plate.sprite = plate_sprites[id];
    }

}
