using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;

    protected float projectileSpeed = 10.0f;
    protected float projectileLife = 2.0f;

    public virtual void Start()
    {
    }

    public virtual void Shoot(Vector3 origin, Vector3 direction)
    {
        // Instantiate a projectile
        var projectile = Instantiate(projectilePrefab, origin, projectilePrefab.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

        // Rotate the projectile to face the direction, with slight random deviation
        projectile.transform.forward = direction;
        projectile.transform.Rotate(Vector3.up, Random.Range(-50.0f, 50.0f));

        Destroy(projectile, projectileLife);
    }
}
