using UnityEngine;

public class GameArea : MonoBehaviour
{
    public Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<Renderer>().bounds;
    }

    public bool IsInside(Vector3 position, float padding)
    {
        return bounds.Contains(new Vector3(position.x - padding, 0, position.z + padding));
    }

    public Vector3 KeepInside(Vector3 position, float padding)
    {
        float x = Mathf.Clamp(position.x, bounds.min.x + padding, bounds.max.x - padding);
        float z = Mathf.Clamp(position.z, bounds.min.z + padding, bounds.max.z - padding);

        return new Vector3(x, 0, z);
    }

    public Vector3 GetRandomPosition(float padding)
    {
        float x = Random.Range(bounds.min.x + padding, bounds.max.x - padding);
        float z = Random.Range(bounds.min.z + padding, bounds.max.z - padding);

        return new Vector3(x, 0, z);
    }
}
