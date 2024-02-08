using System;
using UnityEngine;
using Random = UnityEngine.Random;

//References:
// https://gamedev.stackexchange.com/questions/203052/changing-falling-behavior-of-my-object-after-collision-to-go-gradually-down-and
// https://forum.unity.com/threads/fast-moving-object-collision-with-controller-object.1378386/

public class ScoreZone : MonoBehaviour
{
    public GameObject ballSpawner;
    public GameObject gameManager;
  

    
    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;
    private static int _score;
    private  static int _score1;

    private void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        String playerTag = tag;
        _playerScore = 0;
  
        Debug.Log(message:$"{tag} = {_playerScore}");
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
        bool scored = CompareTag("Player1");
        //bool scored2 = CompareTag("Player2");
        ++_playerScore;
        if (scored)
        {
            _score = _playerScore;
            // Debug.Log(message:$"{_score} : {_score1}");
            Debug.Log($"LEFT PADDLE SCORES");
            Debug.Log(message:$"The score is " +
                              $"{_score} : {_score1}");
        }
        else
        {
            _score1 = _playerScore;
            Debug.Log(message:$"{_score}{_score1}");
            Debug.Log($"RIGHT PADDLE SCORES");
            Debug.Log(message:$"The score is " +
                              $"{_score} : {_score1}");
        }
        //Debug.Log($"{playerTag} scores, {playerTag} has {_playerScore} points");
        //Debug.Log(message:$"The score is {playerTag}: {_playerScore}");
       // Debug.Log(message:$"{scored}, {scored2}");


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
    

}