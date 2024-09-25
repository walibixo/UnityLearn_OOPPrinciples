using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    [SerializeField]
    private float _waterLevel = 0.0f;  // Y-level of the water surface
    [SerializeField]
    private float _floatStrength = 10.0f;  // Strength of the buoyancy

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the difference between object position and water level
        float difference = _waterLevel - transform.position.y;

        // If the object is below the water level, apply an upward force
        if (difference > 0)
        {
            _rigidBody.AddForce(difference * _floatStrength * Vector3.up, ForceMode.Force);
        }
        else if (difference < 0)
        {
            var position = transform.position;
            position.y = _waterLevel;
            transform.position = position;
        }
    }
}

