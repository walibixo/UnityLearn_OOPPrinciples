public class BurstGun : Gun
{
    protected override void Start()
    {
        base.Start();

        Init(10.0f, 1.0f, 8f);
    }
}
