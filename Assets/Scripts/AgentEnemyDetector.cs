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
        if(collision.gameObject.layer == 8)
        {
            agent.detectedEnemies.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Transform temp = collision.transform;
            if (agent.detectedEnemies.FindIndex(t => t == temp) != -1)
            {
                agent.detectedEnemies.Remove(temp);
            }
        }
    }
}
