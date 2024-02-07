
using UnityEngine;

// References:
// https://gamedev.stackexchange.com/questions/206926/the-random-range-function-isnt-activating-when-i-call-it-how-do-i-get-the-func
public class GameManager : MonoBehaviour
{
    public GameObject p1ScoreZone;
    public GameObject p2ScoreZone;
    public GameObject ballSpawner;

    private void Start()
    {
    }

    public void PlayerWon(string winningTag)
    {
        Debug.Log(message:$"Game Over, {winningTag} has won!"); 
        RestartGame();
        ballSpawner.GetComponent<BallSpawnerController>().NextServe();

    }
    
 
    private void RestartGame()
    {
        p1ScoreZone.GetComponent<ScoreZone>().Reset();
        p2ScoreZone.GetComponent<ScoreZone>().Reset();
    }
    
}