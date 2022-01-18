using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommanderDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = transform.parent.GetComponent<Enemy>();

        if (collision.gameObject.layer == 6)
        {
            enemy.detectedCommander = collision.GetComponent<Commander>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = transform.parent.GetComponent<Enemy>();

        if (collision.gameObject.layer == 6)
        {
            enemy.detectedCommander = null;
        }
    }
}
