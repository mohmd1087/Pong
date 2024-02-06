
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameObject p1ScoreZone;
    public GameObject p2ScoreZone;

    private void Start()
    {
    }

    public void PlayerWon(string winningTag)
    {
        Debug.Log(message:$"{winningTag} has won!"); 

    }
    
 
    private void RestartGame()
    {
        p1ScoreZone.GetComponent<ScoreZone>().Reset();
        p2ScoreZone.GetComponent<ScoreZone>().Reset();
    }
    
}