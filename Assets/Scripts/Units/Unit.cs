using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 _originalScale;

    private GameArea _gameArea;
    protected GameObject shooter;

    [field: SerializeField]
    protected Gun Gun { get; private set; }

    [field: SerializeField]
    protected int Health { get; private set; }

    [field: SerializeField]
    protected float Speed { get; private set; }

    [SerializeField]
    private GameObject _deathEffect;

    [SerializeField]
    private AudioClip _deathSound;

    protected virtual void Awake()
    {
        _gameArea = FindObjectOfType<GameArea>();
    }

    protected virtual void Start()
    {
        _originalScale = transform.localScale;
        shooter = transform.Find("Shooter").gameObject;

        SetGun(Gun);
    }

    protected void Init(int health, float speed)
    {
        Health = health;
        Speed = speed;
    }

    private void SetGun(Gun gun)
    {
        Gun = Instantiate(gun, transform);
    }

    protected virtual void Move()
    {
        Debug.Log("Unit is moving");
    }

    protected void KeepInsideGameArea(float padding = 0.5f)
    {
        transform.position = _gameArea.KeepInside(transform.position, padding);
    }

    protected virtual void Attack()
    {
        Debug.Log("Unit is attacking");
    }

    protected virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        SoundManager.PlaySound(_deathSound, true);

        if (_deathEffect != null)
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
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

}
