using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find spawn point in the scene by tag
        GameObject spawnPointObj = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPointObj == null)
        {
            Debug.LogError("[PlayerSpawner] No GameObject with tag 'SpawnPoint' found in scene!");
            return;
        }

        Transform spawnPoint = spawnPointObj.transform;

        // Check if player already exists (persisted from another scene)
        FPSController existingPlayer = FindFirstObjectByType<FPSController>();
        if (existingPlayer != null)
        {
            // Player exists, reposition to spawn point
            // Disable CharacterController before setting position (it has its own position tracking)
            CharacterController cc = existingPlayer.GetComponent<CharacterController>();
            if (cc != null)
                cc.enabled = false;
            
            existingPlayer.transform.position = spawnPoint.position;
            existingPlayer.transform.rotation = spawnPoint.rotation;
            
            if (cc != null)
                cc.enabled = true;
            
            Debug.Log($"[PlayerSpawner] Player repositioned to spawn point: {spawnPoint.position}");
            return;
        }

        if (playerPrefab == null)
        {
            Debug.LogError("[PlayerSpawner] Player prefab not assigned!");
            return;
        }

        // Spawn player
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        DontDestroyOnLoad(player);
        Debug.Log("[PlayerSpawner] Player spawned and set to persist across scenes");
    }
}
