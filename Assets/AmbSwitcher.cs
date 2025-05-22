using UnityEngine;

public class AmbSwitcher : MonoBehaviour
{
    public GameObject ambTavern;
    public GameObject ambForest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ambTavern.SetActive(false);
            ambForest.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ambTavern.SetActive(true);
            ambForest.SetActive(false);
        }
    }
}
