using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObjectPlacer : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab for the 3D object
    public int numberOfObjects = 10; // Number of objects to generate
    public float circleRadius = 5.0f; // Radius of the circle
    public Transform centerObject; // Object to follow for the circle center

    void Start()
    {
        PlaceObjectsInCircle();
    }

    void PlaceObjectsInCircle()
    {
        if (centerObject == null)
        {
            Debug.LogError("Please assign the centerObject in the inspector.");
            return;
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calculate position using polar coordinates and add the centerObject's position
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * circleRadius + centerObject.position.x;
            float z = Mathf.Sin(angle) * circleRadius + centerObject.position.z;

            // Generate a new 3D object in each iteration
            GameObject newObject = Instantiate(objectPrefab, new Vector3(x, 0f, z), Quaternion.identity);

            // Set the new object as a child of this script's GameObject (optional)
            newObject.transform.parent = transform;

            newObject.transform.Rotate(Vector3.up, 180f);
        }
    }
}
