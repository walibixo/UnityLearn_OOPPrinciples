using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject _player;

    private bool _isStopped;

    protected override void Awake()
    {
        base.Awake();

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Start()
    {
        base.Start();

        Init(1, 2.0f);

        StartCoroutine(AttackPlayer());
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
            if (projectile.FromPlayer)
            {
                TakeDamage(projectile.Damage);
            }
        }
    }

    protected override void Move()
    {
        if (_player == null)
            return;

        transform.LookAt(_player.transform);

        if (!_isStopped)
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0;
            direction = direction.normalized;

            Debug.DrawRay(transform.position, direction * 10, Color.red);

            transform.Translate(Speed * Time.deltaTime * direction);
        }

        KeepInsideGameArea();
    }

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));

            _isStopped = true;

            yield return new WaitForSeconds(1.0f);

            Attack();

            yield return new WaitForSeconds(1.0f);

            _isStopped = false;
        }
    }

    protected override void Attack()
    {
        if (!Gun.CanShoot())
            return;

        var (success, direction) = GetAimDirection();
        if (!success)
            return;

        Gun.Shoot(ShooterPosition, direction, false);

        StartCoroutine(SqashAndStretch());
    }

    private (bool success, Vector3 direction) GetAimDirection()
    {
        if (_player == null)
            return (false, Vector3.zero);

        var direction = _player.transform.position - ShooterPosition;
        // Ignore direction along y-axis
        direction.y = 0;
        // Add a slight deviation to the direction
        direction.x += Random.Range(-0.1f, 0.1f);
        direction = direction.normalized;

        return (true, direction);
    }
}
