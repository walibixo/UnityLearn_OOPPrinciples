public class BurstGun : Gun
{
    protected override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 1.0f;

        fireRate = 8f;
    }
}
