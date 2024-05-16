using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingEffect : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingSpeed = 1.0f; // Speed of the bobbing effect
    public float bobbingAmount = 0.5f; // Amount of bobbing movement

    [Header("Rotation Settings")]
    public bool enableRotation = false; // Enable or disable rotation
    public Vector3 rotationSpeed = new Vector3(15.0f, 30.0f, 45.0f); // Rotation speed on each axis

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save the initial position
    }

    void Update()
    {
        // Bobbing effect
        float newY = startPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotation effect
        if (enableRotation)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
