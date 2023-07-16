using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public AudioClip clip;
    public Animator animator;
    public GameObject text_pl1;
    public GameObject text_pl2;
    public Image plate;
    public Sprite[] plate_sprites;

    private bool canHit = false;
    
    void Awake()
    {
        AudioManager._.PlayMusic(clip);
    }
    void Start()
    {
        FadeManager._F.target = 0;

        if(GameManager.lastWinner == 1)
        {
            text_pl1.SetActive(true);
            text_pl2.SetActive(false);
            animator.SetTrigger("Player1");
        } else if (GameManager.lastWinner == 2)
        {
            text_pl2.SetActive(true);
            text_pl1.SetActive(false);
            animator.SetTrigger("Player2");
        }
        else
        {
            throw new System.Exception("Ta raro :S");
        }

        StartCoroutine(delay_to_hit());

        SetPlateSprite();
    }

    private void Update()
    {
        if (canHit)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                Replay();
            }
            else
            {
                //Nada
            }
        }
    }

    void SetPlateSprite()
    {
        if (PlayerPrefs.HasKey("LastPlato"))
        {
            
        }
        else
        {
            PlayerPrefs.SetInt("LastPlato", Random.Range(0, plate_sprites.Length));
        }

        int id = PlayerPrefs.GetInt("LastPlato", 0);
        id++;
        if(id >= plate_sprites.Length) id = 0;
        PlayerPrefs.SetInt("LastPlato", id);
        plate.sprite = plate_sprites[id];
    }
    public void Replay() => SceneManager.LoadScene("Game");

    IEnumerator delay_to_hit()
    {
        yield return new WaitForSeconds(1.1f);
        canHit = true;
    }

}
