using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FirstPersonController>().cageMaterialPickups += 1;
            Destroy(gameObject);
        }
    }
}
