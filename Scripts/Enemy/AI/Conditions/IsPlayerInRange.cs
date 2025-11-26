using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsPlayerInRange : Conditional
{
    EnemyAIController controller;
    public SharedTransform currentTarget;
    public float range;

    public override void OnAwake()
    {
        controller = GetComponent<EnemyAIController>();
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, currentTarget.Value.position) < range)
        {
            return TaskStatus.Success;

        }

        return TaskStatus.Failure;
    }
}
