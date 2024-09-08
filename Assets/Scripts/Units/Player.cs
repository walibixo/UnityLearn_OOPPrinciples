using UnityEngine;

public class Player : Unit
{
    public GameObject projectilePrefab;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        speed = 5.0f;
        range = 10.0f;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(hInput, 0, vInput);
        moveDirection.Normalize();

        transform.Translate(speed * Time.deltaTime * moveDirection);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        // Shoot a projectile in direction of the mouse
        var (success, mousePosition) = GetMousePosition();
        if (!success)
            return;

        var direction = (mousePosition - transform.position).normalized;

        direction *= range;

        // Add a slight deviation to the direction
        direction.x += Random.Range(-0.1f, 0.1f);

        // Ignore direction along y-axis
        direction.y = 0.25f;

        Debug.DrawRay(transform.position, direction, Color.red, 1.0f);

        // Instantiate a projectile
        var projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = direction;

        // Rotate the projectile to face the direction, with slight random deviation
        projectile.transform.LookAt(mousePosition);
        projectile.transform.Rotate(Vector3.up, Random.Range(-50.0f, 50.0f));

        Destroy(projectile, 2.0f);
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction, Color.green, 1.0f);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
