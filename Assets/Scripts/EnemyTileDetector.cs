using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileDetector : MonoBehaviour
{
    private Vector3 detectedDirection = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tile detectedTile = collision.gameObject.GetComponent<Tile>();
        if(!detectedTile.GetIsCommanderTile())
        {
            detectedDirection = detectedTile.GetDirection();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*detectedDirection = Vector3.zero;*/
    }

    public Vector3 GetDetectedDirection()
    {
        return detectedDirection;
    }
}
