using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Normal.GorillaTemplate;
using UnityEngine;

public class keyHole : MonoBehaviour
{
    [SerializeField] Rigidbody hatchRB;
    [SerializeField] Transform keyMeshTransform; 
    Animator animator;
    Collider collider;
    Rigidbody rb;
    GrabbableObject grabbableObject;
    KeyGrabUI keyGrabUI;
    public KeyBeltPlacement keyBeltPlacement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        grabbableObject = GetComponent<GrabbableObject>();
        keyGrabUI = GetComponent<KeyGrabUI>();
        keyBeltPlacement = GetComponent<KeyBeltPlacement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("keyHole"))
        {

            Destroy(keyBeltPlacement.playerBeltUI.gameObject);
            collider.enabled = false;
            transform.parent = null;
            keyBeltPlacement.enabled = false;
            grabbableObject.enabled = false;
            keyGrabUI.enabled = false; 
            keyMeshTransform.localPosition = Vector3.zero;
            keyMeshTransform.localRotation = Quaternion.identity;
            animator.enabled = true;
            rb.isKinematic = true; 
        }
    }

    public void lastFrameAnim()
    { 
        hatchRB.GetComponent<Collider>().enabled = false;    
        hatchRB.isKinematic = false;
        Destroy(gameObject);
    }
}