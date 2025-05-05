using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class OpponentAgent : Agent
{
    [Header("Scene References")]
    [Tooltip("Drag in the Player's head or root Transform")]
    public Transform playerHead;

    Rigidbody rb;
    Animator animator;

    [Header("Movement Settings")]
    [Tooltip("Force applied for movement")]
    public float moveForce = 5f;

    [Header("Reward Shaping")]
    [Tooltip("Multiplier for distance penalty (closer = less penalty)")]
    public float distancePenaltyFactor = -0.001f;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public override void OnEpisodeBegin()
    {
        // Randomize spawn around origin
        transform.localPosition = new Vector3(
            Random.Range(-2f, 2f),
            1f,
            Random.Range(-2f, 2f)
        );
        rb.linearVelocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 1) Relative position to player (normalized)
        Vector3 delta = playerHead.position - transform.position;
        sensor.AddObservation(delta / 10f);

        // 2) Current velocity
        sensor.AddObservation(rb.linearVelocity / 10f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var cont = actions.ContinuousActions;

        // Movement
        Vector3 move = new Vector3(cont[0], 0f, cont[1]);
        rb.AddForce(move * moveForce);

        // Animate based on speed
        float speed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        // Punch trigger (branch 2)
        if (cont[2] > 0.5f)
        {
            animator.SetTrigger("Punch");
        }

        // Reward: small penalty proportional to distance
        float dist = Vector3.Distance(transform.position, playerHead.position);
        AddReward(distancePenaltyFactor * dist);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Simple baseline: always walk straight at the player
        var cont = actionsOut.ContinuousActions;
        Vector3 dir = (playerHead.position - transform.position).normalized;
        cont[0] = dir.x;
        cont[1] = dir.z;
        cont[2] = 0f;  // no punch
    }
}
