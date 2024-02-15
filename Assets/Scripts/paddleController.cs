using UnityEngine;


public class PaddleController : MonoBehaviour
{
    public float unitsPerSecond;
    public AudioClip ballSound;
    private AudioSource _source;
    private Rigidbody _rigidbody;
    private string _playerAxis;
    private bool _isShrunken;

    private static int _hitCount;
    private const float MaxAngle = 50f;

    // Start is called before the first frame update
    void Start()
    {   
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerAxis = CompareTag("Player1") ? "Vertical" : "P2Vertical";
        _source = GetComponent<AudioSource>();

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
            //float anglePercent = (bounds - collision.transform.position.x) / 2.5f;
            //Vector3 newVec = Quaternion.Euler(0f, -anglePercent * 45f, 0f) * Vector3.forward;
            //Debug.DrawLine(transform.position, transform.position + newVec * 30f, Color.red);
        
        if (ballVelocity.x > 0f)
            ballrb.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * -MaxAngle) * Vector3.left * magnitude;
        else
            ballrb.velocity = Quaternion.Euler(0f, 0f, rotationScaleY * MaxAngle) * Vector3.right * magnitude;
        
        _hitCount++;
        PlayBallSound();
        if (_isShrunken)
            Grow();

  
    }
    private void PlayBallSound()
    {
        _source.outputAudioMixerGroup.audioMixer.SetFloat("Paddles", (_hitCount/10f));
        _source.PlayOneShot(ballSound);
    }
    public void Shrink()
    {
        // Debug.Log("shrinking");
        //_source.PlayOneShot(shrinkSound);
        _isShrunken = true;
        transform.localScale = new Vector3(1f, 2.5f, 1f);
    }
    
    private void Grow()
    {
        // Debug.Log("growing");
        _isShrunken = false;
        transform.localScale = new Vector3(1f, 5f, 1f);
    }
    
    public static void ResetHitCount()
    {
        _hitCount = 0;
    }
}