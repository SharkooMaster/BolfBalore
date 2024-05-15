using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Globals")]
    public Input_Handler INPUT_HANDLER;
    public GameEvents GAME_EVENTS;

    [Header("Parameters")]
    [SerializeField] private float magnitude;
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private Rigidbody rb;

    [Header("Ui")]
    public TextMeshProUGUI magnitude_text;
    public RectTransform direction_arrow;

    void Update()
    {
        Vector3 direction = transform.forward;

        Quaternion arrowRotation = Quaternion.LookRotation(direction);
        direction_arrow.rotation = Quaternion.Euler(90, arrowRotation.eulerAngles.y, 0);
        magnitude_text.text = $"{Mathf.RoundToInt(magnitude * 100)}";

        if (Input.GetMouseButton(0))
        {
            magnitude += INPUT_HANDLER.get_mouse_y() * (sensitivity * Time.deltaTime);
            magnitude = Mathf.Clamp(magnitude, 0, 1);
            direction_arrow.localScale = new Vector3(1, (magnitude * 1.3f) + 0.2f);
        }
        else
        {
            if (magnitude > 0)
            {
                rb.AddForce(direction * magnitude * 10, ForceMode.Impulse);
                magnitude = 0;
                direction_arrow.localScale = new Vector3(1, (magnitude * 1.3f) + 0.2f);
                GAME_EVENTS.current_hits++;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Goal")
        {
            GAME_EVENTS.turn_over = true;
        }
    }
}
