using UnityEngine;

public class Enemy : Unit
{
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * 10, Color.red);

        transform.Translate(direction * Time.deltaTime);
        transform.position = gameArea.KeepInside(transform.position, 0.5f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(1);
        }
    }
}
