using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using EzySlice;

public class BladeSlice : MonoBehaviour
{
    private int count = 0;
    [SerializeField] AudioClip practiceAudio;
    [SerializeField] AudioClip slicedAudio;
    [SerializeField] AudioClip bombAudio1;
    [SerializeField] AudioClip bombAudio2;
    

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;

    public Material crossSectionMaterialSafe;
    public Material crossSectionMaterialBomb;
    [SerializeField] float force = 100f;

    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            // practice objects
            if (target.CompareTag("Practice Ball"))
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterialSafe);
                SetupSlicedComponent(upperHull);

                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterialSafe);
                SetupSlicedComponent(lowerHull);

                AudioSource.PlayClipAtPoint(practiceAudio, transform.position, 0.5f);

                Destroy(target);    // destroys the main target when the object is sliced
                // destroys the sliced pieces after 5s
                Destroy(upperHull, 5f);
                Destroy(lowerHull, 5f);
            }

            // safe objects
            if (target.CompareTag("Ball"))
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterialSafe);
                SetupSlicedComponent(upperHull);

                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterialSafe);
                SetupSlicedComponent(lowerHull);

                this.count++;
                Debug.Log("THE BLADE HAS SLICED THIS OBJECT!");
                Debug.Log("Current Count: " + this.count);
                FindObjectOfType<GameManager>().IncreaseScore();
                AudioSource.PlayClipAtPoint(slicedAudio, transform.position, 0.5f);

                Destroy(target);    // destroys the main target when the object is sliced
                // destroys the sliced pieces after 5s
                Destroy(upperHull, 5f);
                Destroy(lowerHull, 5f);
            }
            // bomb object
            if (target.CompareTag("Bomb"))
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterialBomb);
                SetupSlicedComponent(upperHull);

                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterialBomb);
                SetupSlicedComponent(lowerHull);

                Debug.Log("BOMB SLICED!");
                this.count--;
                Debug.Log("Current Count: " + this.count);
                FindObjectOfType<GameManager>().DecreaseScore();
                AudioSource.PlayClipAtPoint(bombAudio1, transform.position, 0.5f);

                Destroy(target);    // destroys the main target when the object is sliced
                Destroy(upperHull, 5f);
                Destroy(lowerHull, 5f);
            }
            // instakill bomb object
            if (target.CompareTag("Instakill Bomb"))
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterialBomb);
                SetupSlicedComponent(upperHull);

                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterialBomb);
                SetupSlicedComponent(lowerHull);

                Debug.Log("INSTAKILL BOMB SLICED!");
                FindObjectOfType<GameManager>().ResetScore();
                this.count = 0;
                Debug.Log("Current Count (reset): " + this.count);
                
                AudioSource.PlayClipAtPoint(bombAudio2, transform.position, 0.5f);

                Destroy(target);    // destroys the main target when the object is sliced
                Destroy(upperHull, 5f);
                Destroy(lowerHull, 5f);
            }
        }

        if (FindObjectOfType<GameManager>().GetScore() < 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    // when object is sliced, give off a force to split the object apart
    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(force, slicedObject.transform.position, 1);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}