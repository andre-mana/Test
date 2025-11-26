// EnemyAIController manages the enemy's AI behavior, including movement, animation, and network synchronization.
// It uses Behavior Designer for the AI logic, NavMeshAgent for pathfinding, and Realtime for networked ownership and synchronization.

using BehaviorDesigner.Runtime;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : RealtimeComponent<EnemyModel>
{
    [SerializeField] public Animator animator;
    RealtimeTransform realtimeTransform;
    public RealtimeTransform rt;
    public NavMeshAgent agent;
    public BehaviorTree behaviorTree;
    [SerializeField] AudioSource audioSourceSaw;
    public float rotationSmoothSpeed = 5f;
    public float followSpeed;
    public float smoothRotationSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        realtimeTransform = GetComponent<RealtimeTransform>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.Warp(transform.position);

        if (realtime != null)
        {
            realtime.didConnectToRoom += DidConnectToRoom;
        }
        realtimeTransform = GetComponent<RealtimeTransform>();
        realtimeTransform.ownerIDSelfDidChange += OnOwnerIDSelfDidChange; 
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        if (!realtimeTransform.isOwnedLocallySelf && !realtimeTransform.isOwnedRemotelySelf)
            realtimeTransform.RequestOwnership();
    }

    private void OnOwnerIDSelfDidChange(RealtimeComponent<RealtimeTransformModel> comp, int ownerID)
    {
        if (!realtimeTransform.isOwnedLocallySelf && ownerID == -1)
        {
            realtimeTransform.RequestOwnership();
        }
    }

    protected override void OnRealtimeModelReplaced(EnemyModel previousModel, EnemyModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.currentAnimationDidChange -= OnAnimationChanged;
            previousModel.behaviorTreeEnabledDidChange -= OnBehaviorTreeChanged;
        }

        if (currentModel != null)
        {
            currentModel.currentAnimationDidChange += OnAnimationChanged;
            currentModel.behaviorTreeEnabledDidChange += OnBehaviorTreeChanged;

            if (realtimeTransform.isOwnedLocallySelf)
            {
                // Ensure the agent syncs with the local transform position and rotation
                agent.Warp(transform.position); // Sync the NavMesh agent position
                agent.transform.rotation = transform.rotation;
            }
        }
    }

    private void OnBehaviorTreeChanged(EnemyModel model, bool value)
    {
        behaviorTree.enabled = value;
    }

    // Called when the animation changes in the model.
    private void OnAnimationChanged(EnemyModel model, string newAnim)
    {
        ApplyAnimation(newAnim);
    }

    // Applies the animation state to the animator based on the given string
    public void ApplyAnimation(string state)
    {
        if (animator == null) return;

        animator.SetBool("Idle", state == "Idle");
        animator.SetBool("Walk", state == "Walk");
        animator.SetBool("Attack", state == "Attack");
    }

    // Updates the model's current animation state if the object is owned locally.
    // sends the new animation state to the model.
    public void SetAnimation(string state)
    {
        if (realtimeTransform.isOwnedLocallySelf) // Only update if this object is owned locally.
            model.currentAnimation = state;
    }

    public void SetInitialPosition(Vector3 initialPosition)
    {
        if (realtimeTransform.isOwnedLocallySelf && !behaviorTree.enabled)
        {
            agent.Warp(initialPosition);
            model.behaviorTreeEnabled = true;
        }
    }
}
