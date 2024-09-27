using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [field: SerializeField]
    protected GameObject ProjectilePrefab { get; private set; }

    [SerializeField]
    private AudioClip _shootSound;

    private float _projectileSpeed = 10.0f;
    private float _projectileLife = 2.0f;
    private float _fireRate = 0.5f;

    private float _shootCooldown;
    private bool _canShoot;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        UpdateCooldown();
    }

    protected void Init(float projectileSpeed, float projectileLife, float fireRate)
    {
        _projectileSpeed = projectileSpeed;
        _projectileLife = projectileLife;
        _fireRate = fireRate;
    }

    public virtual void Shoot(Vector3 position, Vector3 direction, bool fromPlayer)
    {
        if (TryToShoot())
        {
            ShootOneProjectile(position, direction, fromPlayer);
        }
    }

    public bool CanShoot()
    {
        return _canShoot;
    }

    protected bool TryToShoot()
    {
        if (_canShoot)
        {
            _canShoot = false;
            return true;
        }

        return false;
    }

    private void UpdateCooldown()
    {
        if (_canShoot)
            return;

        _shootCooldown -= Time.deltaTime;
        if (_shootCooldown <= 0.0f)
        {
            _shootCooldown = 1.0f / _fireRate;
            _canShoot = true;
        }
    }

    protected void ShootOneProjectile(Vector3 origin, Vector3 direction, bool fromPlayer)
    {
        // Instantiate a projectile
        var projectile = Instantiate(ProjectilePrefab, origin, ProjectilePrefab.transform.rotation);

        ShootOneProjectile(projectile, direction, fromPlayer);
    }

    protected void ShootOneProjectile(GameObject projectile, Vector3 direction, bool fromPlayer)
    {
        projectile.GetComponent<Projectile>().Init(fromPlayer);

        projectile.GetComponent<Rigidbody>().velocity = direction * _projectileSpeed;

        // Rotate the projectile to face the direction, with slight random deviation
        projectile.transform.forward = direction;
        projectile.transform.Rotate(Vector3.up, Random.Range(-50.0f, 50.0f));

        SoundManager.PlaySound(_shootSound, true, 0.1f);

        Destroy(projectile, _projectileLife);
    }
}
