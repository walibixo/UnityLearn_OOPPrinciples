using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private AudioClip _hitHurtSound;

    protected override void Start()
    {
        base.Start();

        Init(20, 4.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            var projectile = other.GetComponent<Projectile>();
            if (projectile.FromPlayer)
            {
                TakeDamage(projectile.Damage);
                SoundManager.PlaySound(_hitHurtSound, true);
            }
        }
    }
}
