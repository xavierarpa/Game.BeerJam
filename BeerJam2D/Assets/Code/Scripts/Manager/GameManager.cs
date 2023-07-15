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
    public AudioClip clip_game;
    public AudioClip clip_game_loop;


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
        if(AudioManager._.src_music.clip == clip_game || AudioManager._.src_music.clip == clip_game_loop)
        {
            // nada
        }
        else
        {
            AudioManager._.PlayMusic(clip_game);
            this.Invoke(nameof(EnableLoop), clip_game.length - 1);
        }

        ResetGame();
    }
    private void EnableLoop()
    {
        AudioManager._.PlayMusic(clip_game_loop);
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
    }
    public void OnPelotaCollidePorteria_P1() => bs_p2_point.Invoke(bs_p2_point.LastValue + 1);
    public void OnPelotaCollidePorteria_P2() => bs_p1_point.Invoke(bs_p1_point.LastValue + 1);
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
            Debug.Log($"END GAME: {player}");
            Time.timeScale = 0;
        }
        else
        {
            //
            // END ROUND
            //Func  reset positions
            ball.rb.velocity=Vector2.zero;
            ball.rb.position=Vector2.zero;
            ball.transform.position = Vector3.zero;
            ball.LaunchBall();
            Debug.Log("END ROUND");
        }
    }
    public bool SurpassRounds(int value) => value >= MAX_ROUND;
    public void GoToMenu() => SceneManager.LoadScene("Menu");
    public void Replay() => SceneManager.LoadScene("Game");
}
