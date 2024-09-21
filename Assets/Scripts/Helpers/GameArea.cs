using UnityEngine;

public class GameArea : MonoBehaviour
{
    public Bounds Bounds { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Bounds = GetComponent<Renderer>().bounds;
    }

    public bool IsInside(Vector3 position, float padding)
    {
        return Bounds.Contains(new Vector3(position.x - padding, 0, position.z + padding));
    }

    public Vector3 KeepInside(Vector3 position, float padding)
    {
        float x = Mathf.Clamp(position.x, Bounds.min.x + padding, Bounds.max.x - padding);
        float z = Mathf.Clamp(position.z, Bounds.min.z + padding, Bounds.max.z - padding);

        return new Vector3(x, position.y, z);
    }

    public Vector3 GetRandomPosition(float padding)
    {
        float x = Random.Range(Bounds.min.x + padding, Bounds.max.x - padding);
        float z = Random.Range(Bounds.min.z + padding, Bounds.max.z - padding);

        return new Vector3(x, -1, z);
    }

    public Vector3 GetRandomPosition(float padding, Vector3 avoidAreaCenter, float radiusAvoidAreaCenter)
    {
        Vector3 randomPosition = GetRandomPosition(padding);

        while (Vector3.Distance(randomPosition, avoidAreaCenter) < radiusAvoidAreaCenter)
        {
            randomPosition = GetRandomPosition(padding);
        }

        return randomPosition;
    }
}
