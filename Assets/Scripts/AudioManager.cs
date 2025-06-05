using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayOneShot(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }
    
    public EventInstance NewEventInstance(EventReference eventReference)
    {
        var eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.start();
        return eventInstance;
    }
    
    public EventInstance New3DEventInstance(EventReference eventReference, Vector3 position)
    {
        var eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.set3DAttributes(position.To3DAttributes());
        eventInstance.start();
        return eventInstance;
    }
}
