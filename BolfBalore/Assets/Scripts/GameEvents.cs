using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [Header("Globals")]
    public Input_Handler INPUT_HANDLER;

    [Header("Rules")]
    public int max_hits = 10;
    public int max_time = 180; // seconds

    [Header("Gameplay")]
    public List<int> my_hits = new List<int>(); // Append score after level
    public int current_hits = 0;
    public bool turn_over = false;
    public int timer = 0;

    [Header("Ui")]
    public TextMeshProUGUI hits_text;
    public TextMeshProUGUI timer_text;

    private IEnumerator Countdown() {
        while(true) {
            yield return new WaitForSeconds(2);
            timer -= 1;
        }
    }

    void Start()
    {
        timer = max_time;
        StartCoroutine(Countdown());
    }

    void Update()
    {
        if (current_hits >= max_hits || timer <= 0)
        {
            turn_over = true;
        }

        if(turn_over == true && current_hits > 0)
        {
            my_hits.Add(current_hits);
            current_hits = 0;
        }

        INPUT_HANDLER.input_active = !turn_over;
        hits_text.text  = current_hits.ToString();
        timer_text.text = $"{Mathf.RoundToInt(timer/60)}:{timer - (60 * Mathf.RoundToInt(timer/60))}";
    }
}
