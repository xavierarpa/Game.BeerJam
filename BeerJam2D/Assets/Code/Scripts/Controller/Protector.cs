using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector : MonoBehaviour
{
    public CollisionController collision = default;
    public SpriteRenderer spr;
    public SpriteRenderer spr_shadow;
    public AudioClip sound;
    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
        condition.Subscribe(ref collision.OnCollision, OnCollision);
    }
    void OnCollision(Collision2D _collision)
    {
        if (_collision.gameObject.tag.Equals("Pelota"))
        {
            collision.gameObject.SetActive(false);
            AudioManager._.PlaySound(sound);
        }
    }
    private void Update()
    {
        
        if(gameObject.transform.localScale == Vector3.zero)
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.activeInHierarchy == false)
        {
            gameObject.transform.localScale  = Vector3.MoveTowards(gameObject.transform.localScale, Vector3.zero, Time.deltaTime);
            spr.color  = Color.Lerp(spr.color , new Color(spr.color[0],spr.color[1],spr.color[2], 0) ,gameObject.transform.localScale.magnitude);
            spr_shadow.color  = Color.Lerp(spr_shadow.color , new Color(spr_shadow.color[0],spr_shadow.color[1],spr_shadow.color[2], 0) ,gameObject.transform.localScale.magnitude);
        }
        
    }
}
