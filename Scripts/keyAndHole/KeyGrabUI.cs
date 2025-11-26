// When the key is grabbed, display the world canvas showing where to place the key, 
// so it doesn't need to be carried at all times

using Normal.GorillaTemplate;
using Normal.Realtime;
using UnityEngine;

public class KeyGrabUI : MonoBehaviour
{
    [SerializeField] AudioClip grabSfx, releaseSfx;
    [SerializeField] Transform keyMeshTransform;
    AudioSource audioSource;
    GrabbableObject grabbable; 
    void Awake()
    {
        grabbable = GetComponent<GrabbableObject>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<RealtimeTransform>().syncScale = false;
    }

    void OnEnable()
    {
        grabbable.onGrab += HandleGrab;
        grabbable.onRelease += HandleRelease;
    }

    void OnDisable()
    {
        grabbable.onGrab -= HandleGrab;
        grabbable.onRelease -= HandleRelease;
    }

    void HandleGrab(GrabberHand hand)
    {

        GetComponent<RealtimeTransform>().syncScale = false;
        if (!hand.isOwnedLocallyInHierarchy) return;

        keyMeshTransform.localPosition = new Vector3(0,0, -0.025f);
        keyMeshTransform.localRotation = Quaternion.Euler(90, -96, 0);
        hand.grabber.GetComponentInChildren<PlayerBeltUI>().canvas.enabled = true;  
        audioSource.PlayOneShot(grabSfx);
    }

    void HandleRelease(GrabberHand hand)
    {
        if (!hand.isOwnedLocallyInHierarchy) return;

        keyMeshTransform.localPosition = Vector3.zero;
        keyMeshTransform.localRotation = Quaternion.identity;
        hand.grabber.GetComponentInChildren<PlayerBeltUI>().canvas.enabled = false;
        audioSource.PlayOneShot(releaseSfx);
    }
}
