using UnityEngine;
using TMPro;

public class StashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stashText;

    void Update()
    {
        if (SessionState.Instance != null)
        {
            stashText.text = $"Stash: {SessionState.Instance.stashSalvage}";
        }
    }
}