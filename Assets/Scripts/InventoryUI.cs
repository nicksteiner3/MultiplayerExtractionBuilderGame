using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI salvageText;
    private static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SessionState.Instance == null) return;

        salvageText.text = $"Salvage: {SessionState.Instance.runSalvage}";
    }
}