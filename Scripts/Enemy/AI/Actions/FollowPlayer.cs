using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks; 

public class FollowPlayer : Action
{
    EnemyAIController controller;
    public SharedTransform currentTarget;

    public override void OnStart()
    {
        controller = GetComponent<EnemyAIController>();
        if (controller.rt.isOwnedLocallySelf)
        {
            controller.agent.enabled = true;
            controller.agent.speed = controller.followSpeed;
            controller.SetAnimation("run");
        }
       
    }

    public override TaskStatus OnUpdate()
    {
        if (controller.rt.isOwnedLocallySelf)
        {

            RotateTowardsPlayer();
            controller.agent.SetDestination(currentTarget.Value.position);
            transform.position = controller.agent.nextPosition;
        }
        else
        {
            controller.agent.Warp(transform.position); 
        }

        return TaskStatus.Running;
    }

    void RotateTowardsPlayer()
    {
        Vector3 toTarget = controller.agent.steeringTarget - controller.agent.transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(toTarget.normalized);
            controller.agent.transform.rotation = Quaternion.Slerp(controller.agent.transform.rotation, targetRot, controller.smoothRotationSpeed * Time.deltaTime);
        }
    }

    public override void OnEnd()
    {
        controller.agent.enabled = false;
    }
}
