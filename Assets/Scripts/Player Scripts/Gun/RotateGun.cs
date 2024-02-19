using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    private void Update()
    {
        desiredRotation = transform.parent.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
