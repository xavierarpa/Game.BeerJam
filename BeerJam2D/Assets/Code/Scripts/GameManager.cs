using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int MAX_ROUND = 3;
    public static GameManager _;
    public BehaviourSubject<int> bs_player_1_point = new BehaviourSubject<int>(0);
    public BehaviourSubject<int> bs_player_2_point = new BehaviourSubject<int>(0);
    private void Awake()
    {
        _ = this;
        ResetGame();
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        bs_player_1_point.Subscribe(condition, CheckWinCondition_P1);
        bs_player_2_point.Subscribe(condition, CheckWinCondition_P2);
    }
    public void ResetGame()
    {
        bs_player_1_point.Invoke(0);
        bs_player_2_point.Invoke(0);
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
