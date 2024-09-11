using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public float waterLevel = 0.0f;  // Y-level of the water surface
    public float floatStrength = 10.0f;  // Strength of the buoyancy
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the difference between object position and water level
        float difference = waterLevel - transform.position.y;

        // If the object is below the water level, apply an upward force
        if (difference > 0)
        {
            rb.AddForce(difference * floatStrength * Vector3.up, ForceMode.Force);
        }
    }
}

