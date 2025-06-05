using FMODUnity;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    public static AudioEvents instance;
    
    [Header("Player Sounds")]
    public EventReference footSteps;
    public EventReference jump;

    [Header("Interaction")] public EventReference doorOpen;
    
    [Header("Ambient Sounds")]
    public EventReference ambientForest;
    public EventReference ambientFire;
    
    [Header("Music")] public EventReference tavernMusic;

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
