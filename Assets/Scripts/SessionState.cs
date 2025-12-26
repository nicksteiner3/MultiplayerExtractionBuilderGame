using UnityEngine;

public class SessionState : MonoBehaviour
{
    public static SessionState Instance;

    public int salvage;

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

    public void AddSalvage(int amount)
    {
        salvage += amount;
        Debug.Log($"Salvage: {salvage}");
    }
}