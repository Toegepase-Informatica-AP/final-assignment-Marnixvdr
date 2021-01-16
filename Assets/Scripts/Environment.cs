
using TMPro;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private TextMeshPro scoreBoard;
    private TextMeshPro lapCounter;
    private Kart kart;

    public void OnEnable()
    {
        kart = transform.GetComponentInChildren<Kart>();
        scoreBoard = transform.GetComponentInChildren<TextMeshPro>();
        lapCounter = GameObject.FindWithTag("Finishlinetext").GetComponentInChildren<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        scoreBoard.text = kart.GetCumulativeReward().ToString("f2");
        lapCounter.text = kart.lapCounterText.ToString();
    }
}