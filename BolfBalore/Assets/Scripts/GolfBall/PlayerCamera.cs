using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player_pivot;
    public GameObject player_cam_obj;
    public Camera player_cam;

    [SerializeField] private float sensitivity_x = 100f;
    [SerializeField] private float sensitivity_y = 100f;
    [SerializeField] private float distance_from_pivot = 10f;
    [SerializeField] private float min_distance = 2f;
    [SerializeField] private float max_distance = 10f;
    [SerializeField] private float collision_offset = 0.2f;
    [SerializeField] private float smooth_speed = 10f;

    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3 current_velocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * sensitivity_x * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * sensitivity_y * Time.deltaTime;

        yaw += mouse_x;
        pitch -= mouse_y;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        Vector3 desired_cam_pos = GetSphericalCoordinate(max_distance, yaw, pitch);
        desired_cam_pos += player_pivot.transform.position;

        Vector3 adjusted_cam_pos = AdjustCameraCollision(player_pivot.transform.position, desired_cam_pos);

        player_cam_obj.transform.position = Vector3.SmoothDamp(player_cam_obj.transform.position, adjusted_cam_pos, 
                                                               ref current_velocity, smooth_speed * Time.deltaTime);
        player_cam_obj.transform.LookAt(player_pivot.transform.position);
    }

    private Vector3 AdjustCameraCollision(Vector3 pivot, Vector3 desired_position)
    {
        RaycastHit hit;
        Vector3 direction = desired_position - pivot;
        float distance = direction.magnitude;
        direction.Normalize();

        if (Physics.SphereCast(pivot, collision_offset, direction, out hit, distance))
        {
            return hit.point + hit.normal * collision_offset;
        }

        return desired_position;
    }

    private Vector3 GetSphericalCoordinate(float r, float yaw, float pitch)
    {
        float yaw_rad = Mathf.Deg2Rad * yaw;
        float pitch_rad = Mathf.Deg2Rad * pitch;

        Vector3 to_return = new Vector3();
        to_return.x = r * Mathf.Cos(pitch_rad) * Mathf.Sin(yaw_rad);
        to_return.y = r * Mathf.Sin(pitch_rad);
        to_return.z = r * Mathf.Cos(pitch_rad) * Mathf.Cos(yaw_rad);

        return to_return;
    }
}
