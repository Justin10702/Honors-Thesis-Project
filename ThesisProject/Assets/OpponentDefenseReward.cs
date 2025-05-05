using UnityEngine;
using Unity.MLAgents;

[RequireComponent(typeof(Collider))]
public class OpponentDefenseReward : MonoBehaviour
{
    [Tooltip("Drag your OpponentAgent here")]
    public OpponentAgent agent;
    [Tooltip("Reward for blocking with arm")]
    public float blockReward = 0.5f;
    [Tooltip("Penalty when body or head is hit")]
    public float hitPenalty = -1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerFist")) return;

        // If the player’s punch hits an OpponentArm → blocked!
        if (CompareTag("OpponentFist"))
        {
            agent.AddReward(blockReward);
        }
        // If they hit OpponentBody or OpponentHead → agent got hit
        else if (CompareTag("OpponentBody") || CompareTag("OpponentHead"))
        {
            agent.AddReward(hitPenalty);
            // Optional: end episode on heavy hit
            // agent.EndEpisode();
        }
    }
}
