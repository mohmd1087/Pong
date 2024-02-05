
using UnityEngine;


public class PaddleController : MonoBehaviour
{
    public float unitsPerSecond;


    private Rigidbody _rigidbody;
    private string _playerAxis;

    private static int _timesHit;
    private const float MaxAngle = 60f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAxis = CompareTag("Player1") ? "P1Vertical" : "P2Vertical";

        ResetTimesHit();
    }

    private void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis(_playerAxis);
        _rigidbody.velocity = verticalAxis * unitsPerSecond * Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Bounds bounds = GetComponent<BoxCollider>().bounds;
        Rigidbody ballRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 ballVelocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        
        
        float magnitude = ballVelocity.magnitude * (1f + _timesHit/100f);
        float collisionTransformY = Mathf.Clamp(collision.transform.position.y, bounds.min.y, bounds.max.y);
        
        float rotationScaleY = (2f * (collisionTransformY - bounds.min.y) / bounds.size.y) - 1f;
        
        if (ballVelocity.x > 0f)
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * -MaxAngle) * Vector3.left * magnitude;
        else
            ballRigidBody.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * MaxAngle) * Vector3.right * magnitude;
        
        _timesHit++;

  
    }
    
    public static void ResetTimesHit()
    {
        _timesHit = 0;
    }
}