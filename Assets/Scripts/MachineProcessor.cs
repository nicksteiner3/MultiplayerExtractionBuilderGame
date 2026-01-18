using UnityEngine;

public class MachineProcessor : MonoBehaviour, IPowered
{
    [SerializeField] private MachineData machineData;
    
    private bool isProcessing = false;
    private float progress = 0f;
    private bool isPaused = false;

    public float PowerConsumption => machineData != null ? machineData.powerConsumption : 0f;

    public void StartProcessing()
    {
        if (machineData == null)
        {
            Debug.LogError("MachineProcessor: No MachineData assigned");
            return;
        }

        if (isProcessing)
        {
            Debug.Log($"{machineData.machineName}: Already processing");
            return;
        }

        // Check if we have inputs
        if (!SessionState.Instance.HasMaterial(machineData.inputMaterial, machineData.inputAmount))
        {
            Debug.Log($"{machineData.machineName}: Insufficient input material");
            return;
        }

        // Check power
        if (!HasSufficientPower())
        {
            Debug.Log($"{machineData.machineName}: Insufficient power");
            return;
        }

        // Request power
        if (!PowerManager.Instance.RequestPower(this, machineData.powerConsumption))
        {
            Debug.Log($"{machineData.machineName}: Failed to reserve power");
            return;
        }

        // Consume inputs
        SessionState.Instance.RemoveMaterial(machineData.inputMaterial, machineData.inputAmount);

        isProcessing = true;
        progress = 0f;
        isPaused = false;
        Debug.Log($"{machineData.machineName}: Started processing");
    }

    private void Update()
    {
        if (!isProcessing) return;

        // Check power status
        if (!HasSufficientPower())
        {
            if (!isPaused)
            {
                isPaused = true;
                OnPowerLost();
            }
            return;
        }
        else if (isPaused)
        {
            isPaused = false;
            OnPowerRestored();
        }

        progress += Time.deltaTime;

        if (progress >= machineData.processTime)
        {
            CompleteProcessing();
        }
    }

    private void CompleteProcessing()
    {
        // Add output materials
        SessionState.Instance.AddMaterial(machineData.outputMaterial, machineData.outputAmount);
        
        Debug.Log($"{machineData.machineName}: Processing complete");

        // Release power
        PowerManager.Instance.ReleasePower(this);

        isProcessing = false;
        progress = 0f;
    }

    public void CancelProcessing()
    {
        if (!isProcessing) return;

        PowerManager.Instance.ReleasePower(this);
        isProcessing = false;
        progress = 0f;
        Debug.Log($"{machineData.machineName}: Processing cancelled");
    }

    public bool HasSufficientPower()
    {
        return PowerManager.Instance.GetAvailablePower() >= machineData.powerConsumption;
    }

    public void OnPowerLost()
    {
        if (isProcessing)
        {
            PowerManager.Instance.ReleasePower(this);
            Debug.Log($"{machineData.machineName}: Power lost, pausing");
        }
    }

    public void OnPowerRestored()
    {
        if (isProcessing)
        {
            PowerManager.Instance.RequestPower(this, machineData.powerConsumption);
            Debug.Log($"{machineData.machineName}: Power restored, resuming");
        }
    }

    public bool IsProcessing() => isProcessing;
    public float GetProgress() => progress;
    public MachineData GetMachineData() => machineData;
}
