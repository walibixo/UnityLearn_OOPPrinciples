using UnityEngine;

public class SpreadGun : Gun
{
    public int SpreadCount = 5;

    protected override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 1.0f;

        fireRate = 1.5f;
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        if (canShoot)
        {
            canShoot = false;

            for (int i = 0; i < SpreadCount; i++)
            {
                var spreadDirection = Quaternion.Euler(0, Random.Range(-10, 10), 0) * direction;
                ShootOneProjectile(position, spreadDirection);
            }
        }
    }
}
