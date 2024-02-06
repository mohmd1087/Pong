using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreZone : MonoBehaviour
{
    public GameObject ballSpawner;
    public GameObject gameManager;
  


    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;

    private void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        _playerScore = 0;
        InitServeDirection();
        Debug.Log(message:$"{_playerScore}");
    }

    private void OnTriggerEnter(Collider other)
    {
            if (!other.gameObject.CompareTag("Ball"))
                return;
            PlayerScored();
            Destroy(other.gameObject);
        
    }

    private void PlayerScored()
    {
        String playerTag = tag;
        Debug.Log($"{playerTag} scores, {playerTag} has {++_playerScore} points");
        ++_playerScore;


        if (_playerScore != GoalScore)
        {
            _serveRight = CompareTag("Player1");
            ballSpawner.GetComponent<BallSpawnerController>().NextServe();
        }
        else
        {
            Debug.Log($"{playerTag} has WON!! Restarting the game...");
            gameManager.GetComponent<GameManager>().PlayerWon(tag);
        }
        PaddleController.ResetHitCount();
    }
    


    public static bool WillServeRight()
    {
        return _serveRight;
    }
    
    private static void InitServeDirection()
    {
        _serveRight = Random.Range(0, 2) == 1;
    }
}