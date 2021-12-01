using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float damage;

    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            if(Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                /*target.GetComponent<Enemy>().TakingDamage(damage);
                Destroy(gameObject);*/
                DamageTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetProperty(Transform projectileTarget, float projectileSpeed, float projectileDamage)
    {
        target = projectileTarget;
        speed = projectileSpeed;
        damage = projectileDamage;
    }

    private void DamageTarget()
    {
        if(target.GetComponent<Enemy>() != null)
        {
            target.GetComponent<Enemy>().TakingDamage(damage);
        }
        else if(target.GetComponent<Commander>() != null)
        {
            target.GetComponent<Commander>().TakingDamage(damage);
        }

        Destroy(gameObject);
    }
}
