
using UnityEngine;

public class Big : MonoBehaviour
{  public AudioClip reverseClip;

    private AudioSource _source;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        Reposition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        
        Vector3 ballVelocity = other.GetComponent<Rigidbody>().velocity;
        other.GetComponent<Rigidbody>().velocity = new Vector3(ballVelocity.x, -ballVelocity.y, 0f);
        
        _source.PlayOneShot(reverseClip);
        
        Reposition();
    }

    private void Reposition()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0f);
    }
}
