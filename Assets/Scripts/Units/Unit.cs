using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int health;
    protected float speed;
    protected float range;

    private Vector3 _originalScale;

    protected Rigidbody _rigidbody;
    protected GameObject shooter;

    protected GameArea gameArea;

    [field: SerializeField]
    protected Gun Gun { get; private set; }

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private AudioClip deathSound;

    protected virtual void Start()
    {
        _originalScale = transform.localScale;
        _rigidbody = GetComponent<Rigidbody>();
        shooter = transform.Find("Shooter").gameObject;
        gameArea = FindObjectOfType<GameArea>();

        SetGun(Gun);
    }

    protected virtual void Move()
    {
        Debug.Log("Unit is moving");
    }

    protected virtual void Attack()
    {
        Debug.Log("Unit is attacking");
    }

    protected virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        SoundManager.PlaySound(deathSound, true);

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected IEnumerator SqashAndStretch()
    {
        var squashScale = _originalScale * 0.8f;

        transform.localScale = _originalScale;

        // Use Lerp to smoothly transition between the original scale and the squash scale
        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(_originalScale, squashScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }

        var stretchScale = _originalScale * 1.2f;
        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(squashScale, stretchScale, t / 0.1f);
            yield return new WaitForEndOfFrame();
        }

        for (float t = 0; t < 0.05f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(stretchScale, _originalScale, t / 0.05f);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetGun(Gun gun = null)
    {
        Gun = Instantiate(gun, transform);
    }
}
