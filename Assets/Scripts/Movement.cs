using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationSpeed = 0.1f;

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            float mouseX = Input.mousePosition.x;
            float rotationY = mouseX * rotationSpeed; 

            transform.rotation = Quaternion.Euler(0, -rotationY, 0); 
        }
    }
}