using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;

    private int count = 0;
    [SerializeField] AudioClip slicedAudio;
    [SerializeField] AudioClip bombAudio;

    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;

        if (other.CompareTag("Bomb"))
        {
            AudioSource.PlayClipAtPoint(bombAudio, transform.position, 0.25f);
            Debug.Log("BOMB HIT!");
        }

        if (other.CompareTag("Ball"))
        {
            AudioSource.PlayClipAtPoint(slicedAudio, transform.position, 0.5f);
            count++;
            Debug.Log("Current Count: " + count);
        }
    }
}
