using UnityEngine;

public class Shrink : MonoBehaviour
{ 
    public GameObject p1Paddle;
    public GameObject p2Paddle;
    private float rotationSpeed = 500f;

    private void Start()
    {
        newPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        
        if (other.gameObject.GetComponent<PongBall>().VelocityBeforeCollision().x < 0f)
        {
            p1Paddle.gameObject.GetComponent<PaddleController>().Shrink();
        }
        else
        {
            p2Paddle.gameObject.GetComponent<PaddleController>().Shrink();
        }
        
        newPosition();
    }
    
    private void Update()
    {
        Transform transform = GetComponent<Transform>();
        transform.Rotate(Vector3.right * (rotationSpeed * Time.deltaTime));
    }

    private void newPosition()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-5f, 5f), 0f);
    }
}
