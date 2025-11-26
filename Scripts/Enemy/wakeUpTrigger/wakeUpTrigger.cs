// Invisible triggers that set the staring enemy's position when the player collides with them

using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class wakeUpTrigger : MonoBehaviour
{
    [SerializeField] EnemyAIController enemy; 
    [SerializeField] Transform wakeUpPosition;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            enemy.SetInitialPosition(wakeUpPosition.position);
            GetComponent<Collider>().enabled = false;
        }
    }
}
