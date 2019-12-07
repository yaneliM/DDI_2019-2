using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget;
    [Range(1,10)]
    public float rotationSpeed = 1f;
    private float mouseX, mouseY;
    public Vector2 limitY = new Vector2(-30,60);

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
      // Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        
        mouseY = Mathf.Clamp(mouseY, limitY.x, limitY.y);
        cameraTarget.rotation = Quaternion.Euler(mouseY,mouseX,0);
        transform.LookAt(cameraTarget);
    }
}
