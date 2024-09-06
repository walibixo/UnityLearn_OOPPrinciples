using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(hInput, 0, vInput);
        moveDirection.Normalize();

        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}
