using UnityEngine;

public class DefaultGun : Gun
{
    protected override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 2.0f;

        fireRate = 2f;
    }
}
