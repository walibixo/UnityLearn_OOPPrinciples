using UnityEngine;

public class Player : Unit
{
    public Gun Gun;

    private Vector3 moveInput;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.position = new Vector3(0, -1, 0);
        speed = 4.0f;
        range = 10.0f;

        Gun = Instantiate(Gun, transform);
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        moveInput.z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(moveInput * Time.deltaTime);
        transform.position = gameArea.KeepInside(transform.position, 0.5f);
    }

    protected override void Attack()
    {
        if (!Gun.CanShoot())
            return;

        var (success, direction) = GetAimDirection();
        if (!success)
            return;

        Gun.Shoot(shooter.transform.position, direction);

        StartCoroutine(SqashAndStretch());
    }

    private (bool success, Vector3 direction) GetAimDirection()
    {
        // Shoot a projectile in direction of the mouse
        var (success, mousePosition) = MouseAiming.GetMousePosition();
        if (!success)
            return (false, Vector3.zero);

        var direction = mousePosition - shooter.transform.position;
        // Ignore direction along y-axis
        direction.y = 0;
        // Add a slight deviation to the direction
        direction.x += Random.Range(-0.1f, 0.1f);
        direction = direction.normalized;

        return (true, direction);
    }
}
