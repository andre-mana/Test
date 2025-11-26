using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


// Instead of using a field of view with Vector3.Angle(transform.forward, target.position - transform.position) and raycasting, I opted to simplify the detection by using OverlapSphere. 
//This forces the enemy to continuously chase the player.
public class CanSeePlayer : Conditional
{
    public float detectionRange;
    Collider[] hits = new Collider[10];
    public Transform topHead;
    public LayerMask detectionLayer;
    public SharedTransform currentTarget;

    public override TaskStatus OnUpdate()
    {
        Transform closestTarget = null;
        float closestDist = Mathf.Infinity;

        int count = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, hits, detectionLayer);

        for (int i = 0; i < count; i++)
        {
            Transform target = hits[i].transform.root;
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestTarget = target;
            }
        }

        if (closestTarget != null && currentTarget != null)
        {
            currentTarget.Value = closestTarget;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
