using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected float speed;
    protected float range;

    private Vector3 originalScale;

    protected new Rigidbody rigidbody;
    protected GameObject shooter;

    public GameArea gameArea;

    protected virtual void Start()
    {
        originalScale = transform.localScale;
        rigidbody = GetComponent<Rigidbody>();
        shooter = transform.Find("Shooter").gameObject;
    }

    protected virtual void Spawn()
    {
        Debug.Log("Unit is spawning");
    }

    protected virtual void Move()
    {
        Debug.Log("Unit is moving");
    }

    protected virtual void Attack()
    {
        Debug.Log("Unit is attacking");
    }

    protected virtual void Die()
    {
        Debug.Log("Unit is dying");
    }

    protected virtual void TakeDamage()
    {
        Debug.Log("Unit is taking damage");
    }

    protected virtual void Heal()
    {
        Debug.Log("Unit is healing");
    }

    protected IEnumerator SqashAndStretch()
    {
        var squashScale = originalScale * 0.8f;

        // Use Lerp to smoothly transition between the original scale and the squash scale
        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, squashScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }

        var stretchScale = originalScale * 1.2f;
        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(squashScale, stretchScale, t / 0.1f);
            yield return new WaitForEndOfFrame();
        }

        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(stretchScale, originalScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }
    }
}
