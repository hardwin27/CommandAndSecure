using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommanderDetector : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            enemy.detectedCommander = collision.GetComponent<Commander>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemy.detectedCommander = null;
        }
    }
}
