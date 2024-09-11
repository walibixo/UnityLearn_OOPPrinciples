using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;

    protected float projectileSpeed = 10.0f;
    protected float projectileLife = 2.0f;
    protected float fireRate = 0.5f;

    protected float shootCooldown;
    protected bool canShoot;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        if (!canShoot)
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0.0f)
            {
                shootCooldown = 1.0f / fireRate;
                canShoot = true;
            }
        }
    }

    public virtual void Shoot(Vector3 position, Vector3 direction)
    {
        if (canShoot)
        {
            canShoot = false;

            ShootOneProjectile(position, direction);
        }
    }

    public bool CanShoot()
    {
        return canShoot;
    }

    protected void ShootOneProjectile(Vector3 origin, Vector3 direction)
    {
        // Instantiate a projectile
        var projectile = Instantiate(projectilePrefab, origin, projectilePrefab.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

        // Rotate the projectile to face the direction, with slight random deviation
        projectile.transform.forward = direction;
        projectile.transform.Rotate(Vector3.up, Random.Range(-50.0f, 50.0f));

        Destroy(projectile, projectileLife);
    }

    protected void ShootOneProjectile(GameObject projectile, Vector3 direction)
    {
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

        // Rotate the projectile to face the direction, with slight random deviation
        projectile.transform.forward = direction;
        projectile.transform.Rotate(Vector3.up, Random.Range(-50.0f, 50.0f));

        Destroy(projectile, projectileLife);
    }
}
