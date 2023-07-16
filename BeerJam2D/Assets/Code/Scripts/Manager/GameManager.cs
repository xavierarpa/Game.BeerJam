using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int MAX_ROUND = 3;
    public static GameManager _;

    [Header("Settings")]
    [Space]
    public AudioClip clip_game_loop;
    public Animator countdownAnimation;


    public BehaviourSubject<bool> bs_is_321 = new BehaviourSubject<bool>(false);

    [Header("P1 Settings")]
    public BehaviourSubject<int> bs_p1_point = new BehaviourSubject<int>(0);
    public PlayerController p1 = default;
    public Porteria porteria_p1 = default;


    [Header("P2 Settings")]
    public BehaviourSubject<int> bs_p2_point = new BehaviourSubject<int>(0);
    public PlayerController p2 = default;
    public Porteria porteria_p2 = default;

    [Header("Pelota Settings")]
    [Space]
    public Ball ball;


    private void Awake()
    {
        _ = this;
        AudioManager._.PlayMusic(clip_game_loop);
        ResetGame();
    }
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition)
    {
        bs_p1_point.Subscribe(condition, CheckWinCondition_P1);
        bs_p2_point.Subscribe(condition, CheckWinCondition_P2);
        condition.Subscribe(ref porteria_p1.OnPelotaCollide, OnPelotaCollidePorteria_P1);
        condition.Subscribe(ref porteria_p2.OnPelotaCollide, OnPelotaCollidePorteria_P2);
    }
   
    public void ResetGame()
    {
        bs_p1_point.Invoke(0);
        bs_p2_point.Invoke(0);
        ball.Reset();
        StartCountdown();
    }
    private void StartCountdown()
    {
        countdownAnimation.SetTrigger("321");
        StartCoroutine(start_countdown());
    }
    public void OnPelotaCollidePorteria_P1() => bs_p2_point.Invoke(bs_p2_point.LastValue + 1);
    public void OnPelotaCollidePorteria_P2() => bs_p1_point.Invoke(bs_p1_point.LastValue + 1);
    public void CheckWinCondition_P1(int value) => CheckWinCondition(value, 1);
    public void CheckWinCondition_P2(int value) => CheckWinCondition(value, 2);
    public void CheckWinCondition(int value, int player)
    {
        if (SurpassRounds(value))
        {
            //Ha ganao player
            // TOdo
            // Func end

            // END GAME
            // Debug.Log($"END GAME: {player}");
            Time.timeScale = 0;
        }
        // En el round 0 se ejecutará el countdown
        else if(value != 0)
        {
            //
            // END ROUND
            //Func  reset positions
            ball.Reset();
            ball.LaunchBall();
            // Debug.Log("END ROUND");
        }
    }
    public bool SurpassRounds(int value) => value >= MAX_ROUND;
    public void GoToMenu() => SceneManager.LoadScene("Menu");
    public void Replay() => SceneManager.LoadScene("Game");
    IEnumerator start_countdown()
    {
        bs_is_321.Invoke(true);
        ball._collider2D.enabled = false;
        yield return new WaitForSeconds(3f);
        ball._collider2D.enabled = true;
        ball.LaunchBall();
        bs_is_321.Invoke(false);
    }
}
