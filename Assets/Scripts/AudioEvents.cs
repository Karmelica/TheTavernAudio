using FMODUnity;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    public static AudioEvents instance;
    
    [Header("Snapshots")]
    public EventReference insideRoomSnapshot;
    public EventReference globalMuteSnapshot;
    public EventReference musicMuteSnapshot;
    public EventReference ambientMuteSnapshot;
    public EventReference outsideMuteSnapshot;
    public EventReference lowHealthSnapshot;
    
    [Header("Health")]
    public EventReference lowHealth;
    
    [Header("Player Sounds")]
    public EventReference footSteps;
    public EventReference jump;

    [Header("Interaction")]
    public EventReference doorOpen;
    
    [Header("Ambient Sounds")]
    public EventReference ambientForest;
    public EventReference ambientFire;
    
    [Header("Music")]
    public EventReference tavernMusic;

    public EventReference tavernAmbient;

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
}
