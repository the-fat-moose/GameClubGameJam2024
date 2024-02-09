using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Transform playerOrientation;

    [Header("Mouse Sensitivity")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private float camXrotation;
    private float camYrotation;

    private void Start()
    {
        // lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        camYrotation += mouseX;
        camXrotation -= mouseY;
        // clamp the rotation to be -90 and 90 up and down
        camXrotation = Mathf.Clamp(camXrotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(camXrotation, camYrotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, camYrotation, 0);
    }
}
