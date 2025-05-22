using FMODUnity;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    public static AudioEvents instance;
    
    [Header("Player Sounds")]
    public EventReference footSteps;

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
