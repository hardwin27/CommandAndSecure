using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileDetectorController : MonoBehaviour
{
    private Vector3 detectedDirection = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TileController detectedTile = collision.gameObject.GetComponent<TileController>();
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
