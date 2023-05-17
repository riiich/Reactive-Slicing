using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] AudioClip bombAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            Debug.Log("THE BLADE HAS TOUCHED THE BOMB!");
            AudioSource.PlayClipAtPoint(bombAudio, transform.position);
        }
    }
}
