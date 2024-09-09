using UnityEngine;

public class SpreadGun : Gun
{
    public int SpreadCount = 5;

    public override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 2.0f;
    }

    public override void Shoot(Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < SpreadCount; i++)
        {
            var spreadDirection = Quaternion.Euler(0, Random.Range(-10, 10), 0) * direction;
            base.Shoot(position, spreadDirection);
        }
    }
}
