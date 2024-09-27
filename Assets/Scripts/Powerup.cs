using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [field: SerializeField]
    public Gun Gun { get; private set; }

    [SerializeField]
    private AudioClip _pickUpSound;

    void Start()
    {
        StartCoroutine(DestroyOnTimeout());
    }

    void Update()
    {
        if (transform.position.y < 0.2f)
        {
            transform.position += 1f * Time.deltaTime * Vector3.up;
        }

        transform.Rotate(100f * Time.deltaTime * Vector3.forward);
    }

    IEnumerator DestroyOnTimeout()
    {
        yield return new WaitForSeconds(6.0f);

        Destroy(gameObject);
    }

    public void PickUp()
    {
        SoundManager.PlaySound(_pickUpSound, true);
    }
}
