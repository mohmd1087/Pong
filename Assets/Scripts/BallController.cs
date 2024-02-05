
using UnityEngine;

public class PongBall : MonoBehaviour
{
    public float startingSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _velocityBeforeCollision;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = ScoreZone.WillServeRight() ? _rigidbody.velocity = Vector3.right * startingSpeed : _rigidbody.velocity = Vector3.left * startingSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _velocityBeforeCollision = _rigidbody.velocity;
    }

    public Vector3 VelocityBeforeCollision()
    {
        return _velocityBeforeCollision;
    }
}