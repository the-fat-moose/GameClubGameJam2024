using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CreatureCage"))
        {
            if (other.GetComponent<CageTarget>() != null)
            {
                other.GetComponent<CageTarget>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
