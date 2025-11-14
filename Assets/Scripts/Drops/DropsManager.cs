using UnityEngine;
public class DropsManager : MonoBehaviour
{
    public static DropsManager Instance { get; private set; }
    [SerializeField] private GameObject expDropPrefab;
    [SerializeField] private Transform dropsParent;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void SpawnExpDrop(Vector3 position)
    {
        position.y += 1f; // slight offset
        GameObject exp = Instantiate(expDropPrefab, position, Quaternion.identity,dropsParent);
    }
    public void SpawnUpgradeDrop()
    {
        // Future implementation for spawning upgrade drops
    }
}
