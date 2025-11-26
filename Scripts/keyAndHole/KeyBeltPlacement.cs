// Places the key on the player's 'belly' when they grab it

using Normal.GorillaTemplate;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.Audio;

public class KeyBeltPlacement : MonoBehaviour
{
    Transform beltSlotTransform;
    bool touchingBelt = false;
    GrabbableObject grabbable;
    public PlayerBeltUI playerBeltUI;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<GrabbableObject>();
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
        playerBeltUI = hand.grabber.GetComponentInChildren<PlayerBeltUI>();
    }

    void HandleRelease(GrabberHand hand)
    {
        if (!hand.isOwnedLocallyInHierarchy) return;

       
        beltSlotTransform = playerBeltUI.keySlotTransform;

        if (touchingBelt && beltSlotTransform != null)
        {
            transform.position = beltSlotTransform.position;
            transform.rotation = beltSlotTransform.rotation;
            transform.parent = beltSlotTransform; 
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
        }
        else
        {
            transform.parent = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Belt"))
        {
            touchingBelt = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Belt"))
        {
            touchingBelt = false;
        }
    }
}
