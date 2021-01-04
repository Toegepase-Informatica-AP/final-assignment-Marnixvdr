
using TMPro;
using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private TextMeshPro scoreBoard;

    private Kart kart;

    public void OnEnable()
    {
        kart = transform.GetComponentInChildren<Kart>();
        scoreBoard = transform.GetComponentInChildren<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        scoreBoard.text = kart.GetCumulativeReward().ToString("f2");
    }
}