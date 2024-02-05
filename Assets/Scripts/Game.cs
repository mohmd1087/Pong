
using TMPro;

using UnityEngine;

using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public GameObject p1ScoreZone;
    public GameObject p2ScoreZone;
    public GameObject ballSpawner;
    public TextMeshProUGUI winText;
    public Button startButton;

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