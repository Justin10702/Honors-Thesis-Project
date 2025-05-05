using UnityEngine;
using Unity.MLAgents;

[RequireComponent(typeof(Collider))]
public class OpponentPunchReward : MonoBehaviour
{
    [Tooltip("Drag your OpponentAgent here")]
    public OpponentAgent agent;
    [Tooltip("Reward for hitting the player")]
    public float punchHitReward = 10.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Playerhead") || other.CompareTag("PlayerBody"))
        {
            agent.AddReward(punchHitReward);
            // Optional: end episode on knockout
            // agent.EndEpisode();
        }
    }
}
