public class DefaultGun : Gun
{
    public override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        projectileLife = 2.0f;
    }
}
