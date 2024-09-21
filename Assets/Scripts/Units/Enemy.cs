using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    private Transform _player;

    protected override void Start()
    {
        base.Start();
        speed = 2.0f;
        range = 10.0f;

        _player = GameObject.FindGameObjectWithTag("Player").transform;

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
        transform.LookAt(_player);
        Vector3 direction = (_player.position - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * 10, Color.red);

        transform.Translate(speed * Time.deltaTime * direction);
        transform.position = gameArea.KeepInside(transform.position, 0.5f);
    }

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));

            var originalSpeed = speed;
            speed = 0;

            yield return new WaitForSeconds(1.0f);

            Attack();

            yield return new WaitForSeconds(1.0f);

            speed = originalSpeed;
        }
    }

    protected override void Attack()
    {
        if (!Gun.CanShoot())
            return;

        var (success, direction) = GetAimDirection();
        if (!success)
            return;

        Gun.Shoot(shooter.transform.position, direction, false);

        StartCoroutine(SqashAndStretch());
    }

    private (bool success, Vector3 direction) GetAimDirection()
    {
        var direction = _player.transform.position - shooter.transform.position;
        // Ignore direction along y-axis
        direction.y = 0;
        // Add a slight deviation to the direction
        direction.x += Random.Range(-0.1f, 0.1f);
        direction = direction.normalized;

        return (true, direction);
    }
}
