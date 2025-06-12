using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class DoorInteract : MonoBehaviour, IInteractable
{
    private Animator animator;
    public bool isOpen;
    private EventInstance doorOpenEvent;
    
    public void Interact()
    {
        if (isOpen)
        {
            isOpen = false;
            if(AmbSwitcher.instance.isInsideRoom)
            {
                AudioManager.instance.insideRoomSnapshotInstance.start();
            }
            animator.SetTrigger("Close");
            doorOpenEvent.setParameterByNameWithLabel("DoorState", "Close");
            doorOpenEvent.start();
        }
        else
        {
            isOpen = true;
            if (AmbSwitcher.instance.isInsideRoom)
            {
                AudioManager.instance.insideRoomSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            animator.SetTrigger("Open");
            doorOpenEvent.setParameterByNameWithLabel("DoorState", "Open");
            doorOpenEvent.start();
        }
    }

    private void Start()
    {
        doorOpenEvent = AudioManager.instance.New3DEventInstance(AudioEvents.instance.doorOpen, transform.position);
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the door.");
        }
    }
}
