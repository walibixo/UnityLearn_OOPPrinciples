using System.Collections;
using UnityEngine;

public class Player : Unit
{
    public GameObject projectilePrefab;

    private Camera mainCamera;
    private new Rigidbody rigidbody;

    private Vector3 originalScale;
    private Vector3 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        speed = 4.0f;
        range = 10.0f;

        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();

        originalScale = transform.localScale;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        moveInput.z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = moveInput;
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

        StartCoroutine(SqashAndStretch());
    }

    IEnumerator SqashAndStretch()
    {
        var squashScale = originalScale * 0.8f;

        // Use Lerp to smoothly transition between the original scale and the squash scale
        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, squashScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }

        var stretchScale = originalScale * 1.2f;
        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(squashScale, stretchScale, t / 0.1f);
            yield return new WaitForEndOfFrame();
        }

        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(stretchScale, originalScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }
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
