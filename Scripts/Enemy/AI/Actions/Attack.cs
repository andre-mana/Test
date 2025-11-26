using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    EnemyAIController controller;
    public SharedTransform currentTarget;

    public override void OnStart()
    {
        controller = GetComponent<EnemyAIController>();
        controller.agent.enabled = true;
        controller.agent.speed = 0;
        if (controller.rt.isOwnedLocallySelf)
        {
            controller.SetAnimation("Attack");
        }
    }

    public override TaskStatus OnUpdate()
    {
        RotateTowardsPlayer();
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        controller.SetAnimation("run");
    }

    void RotateTowardsPlayer()
    {
        Vector3 toTarget = currentTarget.Value.position - transform.position;
        toTarget.y = 0f;
        Quaternion targetRot = Quaternion.LookRotation(toTarget.normalized);
        controller.agent.transform.rotation = Quaternion.Slerp(controller.agent.transform.rotation, targetRot, controller.smoothRotationSpeed * Time.deltaTime);
    }
}
