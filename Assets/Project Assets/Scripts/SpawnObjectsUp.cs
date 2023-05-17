using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsUp : MonoBehaviour
{
    private Collider spawnArea;     // the spawn area is going to be within the box collider

    public GameObject[] objectPrefabs;  // array of game objects that will be spawned

    // the spawn times of an object
    public float minSpawnDelay = 0.2f;
    public float maxSpawnDelay = 5f;

    // the angle in which the object will be launched towards the player
    public float minAngle = -10f;
    public float maxAngle = 10f;

    // the amount of force that will be launched towards the player
    public float minForce = 50f;
    public float maxForce = 300f;

    public float lifetime = 5f;     // the amount of time the object stays alive before getting destroyed 

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);    // waits for 2 seconds before spawning objects

        while (enabled)
        {
            // picks a random object from the objectPrefabs array (we add the objects into the array in the inspector in Unity)
            GameObject objPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

            // the position of where the objects are spawned
            Vector3 pos = new Vector3();
            pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // rotation of the spawner to spawn the objects
            Quaternion rotation = Quaternion.Euler(0f, Random.Range(minAngle, maxAngle), 0f);

            // create the object 
            GameObject obj = Instantiate(objPrefab, pos, rotation);

            // add a force to have the objects move in a direction
            float force = Random.Range(minForce, maxForce);
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.up * force, ForceMode.VelocityChange);

            // destroy the object after x amount of seconds
            Destroy(obj, lifetime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
