using UnityEngine;

public class Player : Unit
{
    private GameManager _gameManager;

    private Vector3 _moveInput;

    protected override void Awake()
    {
        base.Awake();

        _gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Init(1, 4.0f);

        transform.position = new Vector3(0, -1, 0);
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal") * Speed;
        _moveInput.z = Input.GetAxisRaw("Vertical") * Speed;

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

        KeepInsideGameArea();
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

    protected override void Die()
    {
        base.Die();

        _gameManager.GameOver();
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
