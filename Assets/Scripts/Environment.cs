
using TMPro;
using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private TextMeshPro scoreBoard;
    public TextMeshPro countDownTimer;



    private Kart kart;

    void Start()
    {
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            countDownTimer.text = count.ToString();
            Debug.Log(count.ToString());
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        StartGame();
    }

    void StartGame()
    {
        countDownTimer.text = "GO";
    }


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