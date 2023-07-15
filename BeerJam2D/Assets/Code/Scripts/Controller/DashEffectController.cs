using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectController : MonoBehaviour
{
    public static DashEffectController dashEffectController;
    public GameObject shadow;
    public List<GameObject> pool = new List<GameObject>();
    public float speed;
    public Color color;

    private float timer;

    private void Awake()
    {
        dashEffectController = this;
    }

    public GameObject GetShadows()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;
                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<ColorDashEffect>().color = color;
                return pool[i];
            }
        }

        GameObject obj = Instantiate(shadow, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<ColorDashEffect>().color = color;
        pool.Add(obj);

        return obj;
    }

    public void DashShadowsEffect()
    {
        timer += speed * Time.deltaTime;
        if(timer > 1)
        {
            GetShadows();
            timer = 0;
        }
    }
}
