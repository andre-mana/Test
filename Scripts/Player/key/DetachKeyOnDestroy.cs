// Reset key properties and detach from the current player that was destroyed

using UnityEngine;

public class DetachKeyOnDestroy : MonoBehaviour
{
    [HideInInspector] public KeyBeltPlacement key;


    private void OnDestroy()
    {
        if(key != null) 
        {
            key.GetComponent<Rigidbody>().isKinematic = false;
            key.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            key.transform.parent = null;
        }
    }
}
