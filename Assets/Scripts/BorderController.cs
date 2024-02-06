using UnityEngine;

public class PongBorder : MonoBehaviour
{
    public GameObject ballSpawner;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;
        Vector3 velocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        collision.rigidbody.velocity = new Vector3(velocity.x, velocity.y * -1, velocity.z);
        
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (!collisionInfo.gameObject.CompareTag("Ball"))
            return;
        if (collisionInfo.gameObject.GetComponent<Rigidbody>().velocity.y == 0f)
        {
            Destroy(collisionInfo.gameObject);
            ballSpawner.GetComponent<BallSpawnerController>().NextServe();
            PaddleController.ResetHitCount();
        }
    }
}