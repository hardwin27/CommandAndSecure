using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamageTile : Tile
{
    public float dotDuration = 5f;
    public float dotDamage = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        isLowground = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect if the collision is on layer enemy
        if(collision.gameObject.layer == 8)
        {
            collision.GetComponent<Enemy>().SetDoT(dotDuration, dotDamage);
        }
        
    }
}