// Syncs the flashlight's on/off state across all players

using Normal.Realtime;
using UnityEngine;

public class FlashlightSync : RealtimeComponent<FlashlightModel>
{
    [SerializeField] Light flashlight;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip flashLightSfx;
    [SerializeField] Renderer minerFlashLightRend;
    Material matMinerFlashLight;

    public bool IsFlashlightOn => model.flashlightOn;

    void Start()
    {
        matMinerFlashLight = minerFlashLightRend.material; 
    }

    void Update()
    {
        if (flashlight != null)
        {
            if (model.flashlightOn)
            {
                matMinerFlashLight.EnableKeyword("_EMISSION");
                flashlight.enabled = true;
            }
            else
            {
                matMinerFlashLight.DisableKeyword("_EMISSION");
                flashlight.enabled = false;
            } 
        }
    }

    public void ToggleFlashlight(bool on)
    {
        audioSource.PlayOneShot(flashLightSfx);
        model.flashlightOn = on;
    }
}
