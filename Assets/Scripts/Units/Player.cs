using UnityEngine;

public class Player : Unit
{
    private Vector3 _moveInput;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.position = new Vector3(0, -1, 0);
        speed = 4.0f;
        range = 10.0f;
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        _moveInput.z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            var projectile = other.GetComponent<Projectile>();
            if (!projectile.FromPlayer)
            {
                TakeDamage(projectile.Damage);
            }
        }
    }

    protected override void Move()
    {
        transform.Translate(_moveInput * Time.deltaTime);
        transform.position = gameArea.KeepInside(transform.position, 0.5f);
    }

    protected override void Attack()
    {
        if (!Gun.CanShoot())
            return;

        var (success, direction) = GetAimDirection();
        if (!success)
            return;

        Gun.Shoot(shooter.transform.position, direction, true);

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
