using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AmbSwitcher : MonoBehaviour
{
    [SerializeField] private EventReference outsideSnapshot;
    
    private EventInstance outsideSnapshotInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outsideSnapshotInstance.start();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outsideSnapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            outsideSnapshotInstance.release();
        }
    }

    private void Start()
    {
        outsideSnapshotInstance = RuntimeManager.CreateInstance(outsideSnapshot);
    }
}
