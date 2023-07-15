using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int MAX_ROUND = 3;
    public static GameManager _;

    [Header("P1 Settings")]
    public BehaviourSubject<int> bs_p1_point = new BehaviourSubject<int>(0);
    public PlayerController p1 = default;
    public Porteria porteria_p1 = default;


    [Header("P2 Settings")]
    public BehaviourSubject<int> bs_p2_point = new BehaviourSubject<int>(0);
    public PlayerController p2 = default;
    public Porteria porteria_p2 = default;


    private void Awake()
    {
        _ = this;
        ResetGame();
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        bs_p1_point.Subscribe(condition, CheckWinCondition_P1);
        bs_p2_point.Subscribe(condition, CheckWinCondition_P2);
    }
    public void ResetGame()
    {
        bs_p1_point.Invoke(0);
        bs_p2_point.Invoke(0);
    }
    public void CheckWinCondition_P1(int value) => CheckWinCondition(value, 1);
    public void CheckWinCondition_P2(int value) => CheckWinCondition(value, 2);
    public void CheckWinCondition(int value, int player)
    {
        if(SurpassRounds(value))
        {
            //Ha ganao player
            // TOdo
            // Func end

            // END GAME
        }
        else
        {
            //
            // END ROUND
            //Func  reset positions
        }
    }
    public bool SurpassRounds(int value) => value >= MAX_ROUND;
    public void GoToMenu() => SceneManager.LoadScene("Menu");
    public void Replay() => SceneManager.LoadScene("Game");
}
