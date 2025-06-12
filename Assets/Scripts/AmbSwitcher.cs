using System;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AmbSwitcher : MonoBehaviour
{
    public static AmbSwitcher instance;
    
    public bool isInsideRoom;
    
    [SerializeField] private EventReference outsideSnapshot;
    [SerializeField] private EventReference muteFireSnapshot;
    
    private EventInstance outsideSnapshotInstance;
    private EventInstance muteFireSnapshotInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NoFire"))
        {
            muteFireSnapshotInstance.start();
        }
        if (other.CompareTag("InsideRoom"))
        {
            isInsideRoom = true;
        }
        if (other.CompareTag("AmbSwitch"))
        {
            outsideSnapshotInstance.start();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NoFire"))
        {
            muteFireSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        if (other.CompareTag("InsideRoom"))
        {
            isInsideRoom = false;
            AudioManager.instance.insideRoomSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        if (other.CompareTag("AmbSwitch"))
        {
            outsideSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void Start()
    {
        instance = this;
        
        // Initialize the snapshots
        outsideSnapshotInstance = RuntimeManager.CreateInstance(outsideSnapshot);
        muteFireSnapshotInstance = RuntimeManager.CreateInstance(muteFireSnapshot);
    }
}
