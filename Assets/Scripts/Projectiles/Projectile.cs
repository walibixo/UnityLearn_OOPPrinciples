using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public bool isPiercing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isPiercing)
                return;

            Destroy(gameObject);
        }
    }
}
