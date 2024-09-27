using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField]
    public int Damage { get; private set; } = 1;

    public bool FromPlayer { get; private set; } = false;

    [SerializeField]
    private bool _isPiercing;

    public void Init(bool fromPlayer)
    {
        FromPlayer = fromPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((FromPlayer && other.CompareTag("Enemy"))
            || (!FromPlayer && other.CompareTag("Player")))
        {
            if (_isPiercing)
                return;

            Destroy(gameObject);
        }
    }
}
