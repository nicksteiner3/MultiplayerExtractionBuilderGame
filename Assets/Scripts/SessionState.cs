using UnityEngine;

public class SessionState : MonoBehaviour
{
    public static SessionState Instance;

    public int resourcesCollected;
    public int enemiesKilled;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}