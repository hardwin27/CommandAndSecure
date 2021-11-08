using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileDetector : MonoBehaviour
{
    [SerializeField] private Enemy parent;

    Collider2D detectedCollider;
    private Vector3 detectedDirection = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(detectedCollider != collision)
        {
            detectedCollider = collision;
            Tile detectedTile = detectedCollider.gameObject.GetComponent<Tile>();
            if (!detectedTile.GetIsCommanderTile())
            {
                detectedDirection = detectedTile.GetDirection();
                parent.SetNewTileTarget(detectedCollider.transform.position);
            }
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
