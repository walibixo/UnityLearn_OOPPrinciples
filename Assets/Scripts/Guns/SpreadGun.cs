using UnityEngine;

public class SpreadGun : Gun
{
    [SerializeField]
    private int _spreadCount = 5;

    protected override void Start()
    {
        base.Start();

        Init(10.0f, 1.0f, 1.5f);
    }

    public override void Shoot(Vector3 position, Vector3 direction, bool fromPlayer)
    {
        if (TryToShoot())
        {
            for (int i = 0; i < _spreadCount; i++)
            {
                var spreadDirection = Quaternion.Euler(0, Random.Range(-10, 10), 0) * direction;
                ShootOneProjectile(position, spreadDirection, fromPlayer);
            }
        }
    }
}
