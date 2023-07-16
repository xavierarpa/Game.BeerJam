using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreManager : MonoBehaviour
{
    public GameManager gamemanager ;
    public Transform tr_chapas_1 ;
    public Transform tr_chapas_2;
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        gamemanager.bs_p1_point.Subscribe(condition, RefreshUI_P1);

        gamemanager.bs_p2_point.Subscribe(condition, RefreshUI_P2);
    }
    private void RefreshUI_P1(int points)
    {
        for (int i = 0; i < tr_chapas_1.childCount; i++)
        {
            tr_chapas_1.GetChild(i).gameObject.SetActive(i+1 <= points);
        }
    }
    private void RefreshUI_P2(int points)
    {
        for (int i = 0; i < tr_chapas_2.childCount; i++)
        {
            tr_chapas_2.GetChild(i).gameObject.SetActive(i + 1 <= points);
        }
    }
}