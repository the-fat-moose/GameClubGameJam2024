using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialPickup : MonoBehaviour
{
    [Header("Audio Parameters")]
    [SerializeField] private AudioClip pickupSound;
    private AudioSource pickupAudioSource;
    OptionsManager om;

    private void Start()
    {
        pickupAudioSource = GetComponent<AudioSource>();
        om = GameObject.FindFirstObjectByType<OptionsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FirstPersonController>().HandleMaterialCollection(1);
            if (pickupAudioSource != null && om != null) { pickupAudioSource.PlayOneShot(pickupSound, 1f * (om.sfxVolume * om.masterVolume)); }

            Destroy(gameObject, 0.5f);
        }
    }
}
