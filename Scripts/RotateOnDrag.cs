using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    private float startingMousePosition;
    private Vector3 startingEulerAngles;

    public float rotationSpeed;
    public Vector3 rotationVector; // not neccesary for one axis only
    public Vector2 mouseAxis; // not neccesary for one axis only

    [Min(1)]
    public Vector3 stepAngle; // float stepAngle


    private void OnMouseDown() {
        startingMousePosition = (Input.mousePosition * mouseAxis).magnitude; // just Input.mousePosition.x/y
        startingEulerAngles = transform.rotation.eulerAngles;
    }

    private void OnMouseDrag() {
        float distance = startingMousePosition - (Input.mousePosition * mouseAxis).magnitude; // just startingMousePosition - Input.mousePosition.x/y
        Vector3 newEulerAngles = startingEulerAngles + rotationVector * rotationSpeed * distance; // just startingEulerAngles * rotationSpeed * distance;
        newEulerAngles = new Vector3(Mathf.RoundToInt(newEulerAngles.x / stepAngle.x) * stepAngle.x, // switch stepAngle.x/y/z to just stepAngle
                                    Mathf.RoundToInt(newEulerAngles.y / stepAngle.y) * stepAngle.y,
                                    Mathf.RoundToInt(newEulerAngles.z / stepAngle.z) * stepAngle.z);
        transform.rotation = Quaternion.Euler(newEulerAngles);
    }
}
