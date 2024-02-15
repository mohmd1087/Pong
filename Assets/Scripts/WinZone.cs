using System;
using UnityEngine;
using TMPro;

//References:
// https://gamedev.stackexchange.com/questions/203052/changing-falling-behavior-of-my-object-after-collision-to-go-gradually-down-and
// https://forum.unity.com/threads/fast-moving-object-collision-with-controller-object.1378386/
//https://forum.unity.com/threads/reveal-effects-through-animating-vertex-positions.927344/
//https://assetstore.unity.com/packages/tools/animation/leantween-3595#content
//https://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html

public class ScoreZone : MonoBehaviour
{
 
    public GameObject ballSpawner;
    public GameObject gameManager;
    public TextMeshProUGUI scoreTM;
    public AudioClip scoreClip;

    private AudioSource _scoresound;
  

    
    private int _playerScore;
    private const int GoalScore = 11;
    private static bool _serveRight;
    private static int _score;
    private  static int _score1;

    private void Start()
    {
        _scoresound = GetComponent<AudioSource>();
        Reset();
    }
    
    public void Reset()
    {
        String playerTag = tag;
        _playerScore = 0;
        scoreTM.text = $"{_playerScore}";
  
        //Debug.Log(message:$"{tag} = {_playerScore}");
    }

    private void OnTriggerEnter(Collider other)
    {
            if (!other.gameObject.CompareTag("Ball"))
                return;
            PlayerScored();
            _scoresound.PlayOneShot(scoreClip);
            Destroy(other.gameObject);
          


    }
      private void ScoreText()
        {
            scoreTM.text = $"{_playerScore}";
            LeanTween.rotate( scoreTM.rectTransform, 360f, 0.5f).setEase(LeanTweenType.easeOutElastic);
           // LeanTween.alpha(scoreTM.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInCirc);
        }
    
    

    private void PlayerScored()
    {
        String playerTag = tag;
        bool scored = CompareTag("Player1");
        //bool scored2 = CompareTag("Player2");
        ++_playerScore;
        ScoreText();
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