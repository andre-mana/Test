// Handles the flashlight toggle mechanic when the player grips the head 
// It syncs with the FlashlightSync script to control the light based on grip interactions.
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public FlashlightSync flashlightSync;
    [SerializeField] InputActionReference leftGripAction;
    [SerializeField] InputActionReference rightGripAction;

    // It's better not to use a bool to check if a hand is touching the head.
    // A bool cannot track multiple hands. Using a simple bool could cause a problem if player have one hand already, add another, and remove the other. 
    int handsTouchingHead = 0; 
    bool IsTouchingHead => handsTouchingHead > 0;

    private void OnEnable()
    {
        leftGripAction.action.performed += OnLeftGripPerformed;
        rightGripAction.action.performed += OnRightGripPerformed;
        leftGripAction.action.Enable();
        rightGripAction.action.Enable();
    }

    private void OnDisable()
    {
        leftGripAction.action.performed -= OnLeftGripPerformed;
        rightGripAction.action.performed -= OnRightGripPerformed;
        leftGripAction.action.Disable();
        rightGripAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand"))
            handsTouchingHead++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand"))
            handsTouchingHead--;
    }

    private void OnLeftGripPerformed(InputAction.CallbackContext ctx)
    {
        if (IsTouchingHead)
            flashlightSync.ToggleFlashlight(!flashlightSync.IsFlashlightOn);
    }

    private void OnRightGripPerformed(InputAction.CallbackContext ctx)
    {
        if (IsTouchingHead)
            flashlightSync.ToggleFlashlight(!flashlightSync.IsFlashlightOn);
    }
}

