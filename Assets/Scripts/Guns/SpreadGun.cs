using UnityEngine;

public class SpreadGun : Gun
{
    [SerializeField]
    private int _spreadCount = 5;

    protected override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 1.0f;

        fireRate = 1.5f;
    }

    public override void Shoot(Vector3 position, Vector3 direction, bool fromPlayer)
    {
        if (canShoot)
        {
            canShoot = false;

            for (int i = 0; i < _spreadCount; i++)
            {
                var spreadDirection = Quaternion.Euler(0, Random.Range(-10, 10), 0) * direction;
                ShootOneProjectile(position, spreadDirection, fromPlayer);
            }
        }
    }
}
