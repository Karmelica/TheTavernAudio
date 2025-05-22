using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class FMODCommands : MonoBehaviour
{
    #region EVENT EMITTER

    // EVENT EMITTER
    [SerializeField] public StudioEventEmitter tavernEmitter; // Deklaracja publicznego pola, które przechowuje referencję do event emittera na scenie.

    #endregion

    #region EVENT

    // EVENT
    EventInstance footstepsSound; // Deklaracja zmiennej, która będzie przechowywać instancję eventu Footsteps.

    public EventReference footstepsEvent; // Deklaracja publicznego pola, które przechowuje referencję do pliku z eventem Footsteps.

    private void Footsteps()
    {
        // jednorazowe odtworzenie
        RuntimeManager.PlayOneShot(footstepsEvent); // Odtwarza event jednokrotnie bez zarządzania jego instancją.

        // podstawowe zarządzanie eventem
        footstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy nową instancję eventu Footsteps.
        footstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone"); // Ustawia parametr o nazwie "Footsteps_surface" na wartość "Stone".
        footstepsSound.start(); // Uruchamia odtwarzanie eventu.
        footstepsSound.stop(STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        footstepsSound.stop(STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        footstepsSound.release(); // Zwolnia pamięć zajmowaną przez instancję eventu.

        // zarządzanie eventem z przypięciami emittera do gameObjectu 
        footstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
        footstepsSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform)); // Przypięcia emitter eventu do obiektu GameObject.
        footstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
        footstepsSound.start();
        footstepsSound.stop(STOP_MODE.IMMEDIATE);
        footstepsSound.stop(STOP_MODE.ALLOWFADEOUT);
        footstepsSound.release();
    }

    #endregion

    #region SNAPSHOT

    // SNAPSHOT
    EventInstance HealthSnap; // Deklaracja zmiennej, która będzie przechowywać instancję snapshotu Health.

    public EventReference healthSnapshot; // Deklaracja publicznego pola, które przechowuje referencję do pliku z snapshotem Health.

    private void StartSnapshot()
    {
        if (tavernEmitter != null && tavernEmitter.IsPlaying()) // Sprawdza, czy event emitter istnieje i jest aktywny.
        {
            HealthSnap = RuntimeManager.CreateInstance(healthSnapshot); // Tworzy nową instancję snapshotu Health.
            HealthSnap.start(); // Uruchamia snapshot.
        }
        else if (tavernEmitter != null && tavernEmitter.IsPlaying())
        {
            HealthSnap.stop(STOP_MODE.IMMEDIATE); // Stopuje snapshot bez fadeoutu.
            HealthSnap.stop(STOP_MODE.ALLOWFADEOUT); // Stopuje snapshot z fadeoutem.
            HealthSnap.release(); // Zwolnia pamięć zajmowaną przez instancję snapshotu.
        }
    }

    #endregion

    #region VCA

    // VCA
    FMOD.Studio.VCA GlobalVCA; // Deklaracja zmiennej, która będzie przechowywać referencję do VCA o nazwie "Mute".

    private void VCA()
    {
        GlobalVCA = RuntimeManager.GetVCA("vca:/Mute"); // Pobiera referencję do VCA o nazwie "Mute".
        GlobalVCA.setVolume(DecibelToLinear(0)); // Ustawia głośność VCA na maksimum (0 dB).
        GlobalVCA.setVolume(DecibelToLinear(-100)); // Obniża głośność VCA do minimalnego poziomu (-100 dB).
    }

    private float DecibelToLinear(float dB) // Funkcja przeliczająca wartość decybelową na skalę liniową.
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    #endregion

    #region EVENT / EMITTER Z MUZYKĄ

    // EVENT / EMITTER Z MUZYKĄ
    public EventInstance Music; // Deklaracja zmiennej, która będzie przechowywać instancję eventu Music.

    public StudioEventEmitter tavernEmitterMusic; // Deklaracja publicznego pola, które przechowuje referencję do event emittera na scenie.

    private void MusicSwitch()
    {
        // EVENT
        footstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy nową instancję eventu Footsteps.
        Music.setParameterByNameWithLabel("Switch_parts", "Part 2"); // Ustawia parametr o nazwie "Switch_parts" na wartość "Part 2".
        Music.start(); // Uruchamia odtwarzanie eventu.
        Music.stop(STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        Music.stop(STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        Music.release(); // Zwolnia pamięć zajmowaną przez instancję eventu.

        // EMITTER
        tavernEmitterMusic.SetParameter("Switch_parts", 0); // Ustawia parametr o nazwie "Switch_parts" na wartość 0 dla event emittera.
        tavernEmitterMusic.Play(); // Uruchamia odtwarzanie na emitterze.
        tavernEmitterMusic.Stop(); // Stopuje odtwarzanie na emitterze.
    }

    #endregion
}