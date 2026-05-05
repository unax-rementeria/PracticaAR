using UnityEngine;

public class ArrowChild : MonoBehaviour
{
    private Arrows arrowsParent;
    public AudioClip clip;
    public AudioSource audioSource;

    void Start()
    {
        arrowsParent = GetComponentInParent<Arrows>();
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(clip);
        arrowsParent.NextArrow();
    }
}
