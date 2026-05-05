using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Colect : MonoBehaviour
{
    public Score score;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("gema")) return;
        if (other.gameObject == null) return;

        // Immediately disable AND untag to block any duplicate calls
        other.enabled = false;
        other.gameObject.tag = "Untagged";

        score.totalScore++;
        Debug.Log("Score: " + score.totalScore);
        Destroy(other.gameObject);
    }
}
