using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform targetEnemy;
    private float speed;
    private float damage;

    private void FixedUpdate()
    {
        if(targetEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, speed * Time.fixedDeltaTime);
            Vector3 direction = targetEnemy.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>().TakingDamage(damage);
        Destroy(gameObject);
    }

    public void SetProperty(Transform target, float projectileSpeed, float projectileDamage)
    {
        targetEnemy = target;
        speed = projectileSpeed;
        damage = projectileDamage;
    }
}
