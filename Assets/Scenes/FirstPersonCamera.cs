using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform bodyTransform;
    [SerializeField, Range (0,100)] float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        bodyTransform.Rotate(new Vector3(0, mouseX * mouseSensitivity,0));

        
    }
}
