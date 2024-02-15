using UnityEngine;

//References:
// https://gamedev.stackexchange.com/questions/203052/changing-falling-behavior-of-my-object-after-collision-to-go-gradually-down-and
// https://forum.unity.com/threads/fast-moving-object-collision-with-controller-object.1378386/

public class PongBorder : MonoBehaviour
{
    public GameObject ballSpawner;
    public AudioClip ballSound;
    
    private AudioSource _clip;
    
    private void Start()
    {
        _clip = GetComponent<AudioSource>();
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;
        Vector3 velocity = collision.gameObject.GetComponent<PongBall>().VelocityBeforeCollision();
        collision.rigidbody.velocity = new Vector3(velocity.x, velocity.y * -1, velocity.z);
        PlayWallHitSound();
        
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
    
    
    private void PlayWallHitSound()
    {
        _clip.PlayOneShot(ballSound);
    }
}