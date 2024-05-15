using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Handler : MonoBehaviour
{
    public bool input_active = true;
    public float get_mouse_x() { return (input_active) ? Input.GetAxis("Mouse X") : 0; }
    public float get_mouse_y() { return (input_active) ? Input.GetAxis("Mouse Y") : 0; }

    public bool get_mouse_0() { return (input_active) ? Input.GetMouseButton(0) : false; }
}
