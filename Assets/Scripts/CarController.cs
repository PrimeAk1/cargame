using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 20, rotationSpeed = 3, verticalInput, horizontalInput, rotationDirection;
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);

        horizontalInput = Input.GetAxis("Horizontal");
        rotationDirection = (verticalInput < 0) ? -1 : 1;

        if (verticalInput != 0)
        transform.Rotate(Vector3.up, horizontalInput * speed * rotationSpeed * Time.deltaTime * rotationDirection);
    }
}
