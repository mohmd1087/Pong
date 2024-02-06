
using UnityEngine;


public class PaddleController : MonoBehaviour
{
    public float unitsPerSecond;


    private Rigidbody _rigidbody;
    private string _playerAxis;

    private static int _hitCount;
    private const float MaxAngle = 50f;

    // Start is called before the first frame update
    void Start()
    {   
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerAxis = CompareTag("Player1") ? "Vertical" : "P2Vertical";

        ResetHitCount();
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
        Rigidbody ballrb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 ballVelocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        
        
        float magnitude = ballVelocity.magnitude * (1f + _hitCount/80f);
        float collisionTransformY = Mathf.Clamp(collision.transform.position.y, bounds.min.y, bounds.max.y);
        
        float rotationScaleY = (2f * (collisionTransformY - bounds.min.y) / bounds.size.y) - 1f;
        
        if (ballVelocity.x > 0f)
            ballrb.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * -MaxAngle) * Vector3.left * magnitude;
        else
            ballrb.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * MaxAngle) * Vector3.right * magnitude;
        
        _hitCount++;

  
    }
    
    public static void ResetHitCount()
    {
        _hitCount = 0;
    }
}