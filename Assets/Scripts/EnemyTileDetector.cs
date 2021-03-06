using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileDetector : MonoBehaviour
{
    /*[SerializeField] private Enemy parent;*/

    Collider2D detectedCollider;
    /*private Vector3 detectedDirection = Vector3.zero;*/
    private Tile detectedTile = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy parent = transform.parent.GetComponent<Enemy>();
        if (detectedCollider != collision)
        {
            detectedCollider = collision;
            Tile tempDetectedTile = detectedCollider.gameObject.GetComponent<Tile>();
            if (tempDetectedTile.GetIsLowground())
            {
                detectedTile = tempDetectedTile;
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
        if(detectedTile == null)
        {
            return Vector3.zero;
        }

        return detectedTile.GetDirection();
    }
}
