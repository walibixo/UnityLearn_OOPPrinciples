using UnityEngine;

public class DefaultGun : Gun
{
    protected override void Start()
    {
        base.Start();

        Init(10.0f, 2.0f, 2f);
    }
}
