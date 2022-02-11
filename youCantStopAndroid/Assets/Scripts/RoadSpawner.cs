using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    public GameObject[] roadPrefabs;
    public GameObject barierPrefab;

    private GameObject barierInstance;

    public float spawnZPosition = 0f;
    public float roadLength = 40f;
    public int numberOfRoads = 3;

    public Transform carTransform;

    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        SpawnRoad(0);
        barierInstance = Instantiate(barierPrefab, new Vector3(24,.5f,-10) , transform.rotation);
    }

    void Update()
    {
        if(carTransform != null)
        {
            if (carTransform.position.z > spawnZPosition - (numberOfRoads * roadLength))
            {
                
                 SpawnRoad(Random.Range(1, roadPrefabs.Length));

                 if (activeRoads.Count > 2 * numberOfRoads)
                 {
                      moveObjectForward(barierInstance.transform);
                      DeleteRoad();
                 }
                
            }
        }           
    }

    public void SpawnRoad(int roadIndex)
    {
        GameObject roadInstance = Instantiate(roadPrefabs[roadIndex], transform.forward * spawnZPosition, transform.rotation);
        roadInstance.transform.parent = this.transform;
        activeRoads.Add(roadInstance);
        spawnZPosition += roadLength;
    }

    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

    private void moveObjectForward(Transform objectToMove)
    {
        objectToMove.position += transform.forward * roadLength;
    }
}
