using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEnemyDetector : MonoBehaviour
{
    private Agent agent;

    private void Start()
    {
        agent = transform.parent.GetComponent<Agent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        agent.detectedEnemies.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform temp = collision.transform;
        if (agent.detectedEnemies.FindIndex(t => t == temp) != -1)
        {
            agent.detectedEnemies.Remove(temp);
        }
    }
}
