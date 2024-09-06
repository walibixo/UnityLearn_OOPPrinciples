using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected float speed;
    protected float range;

    public virtual void Spawn()
    {
        Debug.Log("Unit is spawning");
    }

    public virtual void Move()
    {
        Debug.Log("Unit is moving");
    }

    public virtual void Attack()
    {
        Debug.Log("Unit is attacking");
    }

    public virtual void Die()
    {
        Debug.Log("Unit is dying");
    }

    public virtual void TakeDamage()
    {
        Debug.Log("Unit is taking damage");
    }

    public virtual void Heal()
    {
        Debug.Log("Unit is healing");
    }
}
