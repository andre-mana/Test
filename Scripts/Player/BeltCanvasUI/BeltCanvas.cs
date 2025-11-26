// This is attached to the Gorilla avatar, so when the key calls hand.grabber.GetComponent<BeltCanvas>(), it will access the world canvas for display
// and the belt slot transform to snap the key into place.
using UnityEngine;
public class PlayerBeltUI : MonoBehaviour 
{
    public Canvas canvas;
    public Transform keySlotTransform;
}
