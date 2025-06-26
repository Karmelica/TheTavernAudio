using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public EventInstance insideRoomSnapshotInstance;
    public EventInstance GlobalMuteInstance;
    public EventInstance MusicMuteInstance;
    public EventInstance AmbientMuteInstance;
    public EventInstance OutsideMuteInstance;
    public EventInstance LowHealthSnapshotInstance;
    
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
        //eventInstance.start();
        return eventInstance;
    }
    
    public EventInstance New3DEventInstance(EventReference eventReference, Vector3 position)
    {
        var eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.set3DAttributes(position.To3DAttributes());
        //eventInstance.start();
        return eventInstance;
    }

    protected virtual void MuteInput(EventInstance eventInstance, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            eventInstance.start();
        }

        if (Input.GetKeyUp(keyCode))
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void MuteOnInput()
    {
        MuteInput(GlobalMuteInstance, KeyCode.U);
        MuteInput(MusicMuteInstance, KeyCode.I);
        MuteInput(AmbientMuteInstance, KeyCode.O);
        MuteInput(OutsideMuteInstance, KeyCode.P);
        MuteInput(LowHealthSnapshotInstance, KeyCode.K);
    }

    private void Update()
    {
        MuteOnInput();
    }

    private void Start()
    {
        var healthEvent = NewEventInstance(AudioEvents.instance.lowHealth);
        healthEvent.start();
        
        insideRoomSnapshotInstance = NewEventInstance(AudioEvents.instance.insideRoomSnapshot);
        GlobalMuteInstance = NewEventInstance(AudioEvents.instance.globalMuteSnapshot);
        MusicMuteInstance = NewEventInstance(AudioEvents.instance.musicMuteSnapshot);
        AmbientMuteInstance = NewEventInstance(AudioEvents.instance.ambientMuteSnapshot);
        OutsideMuteInstance = NewEventInstance(AudioEvents.instance.outsideMuteSnapshot);
        LowHealthSnapshotInstance = NewEventInstance(AudioEvents.instance.lowHealthSnapshot);
    }
}
