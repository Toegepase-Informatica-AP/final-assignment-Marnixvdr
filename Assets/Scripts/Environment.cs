
using TMPro;
using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private TextMeshPro scoreBoard;
    public TextMeshPro countDownTimer; 



    public Kart kart1;
    public Kart kart2;
    public Kart kart3;


    void Start()
    {
        kart1.enabled = false;
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        kart1.enabled = false;
        kart2.enabled = false;
        kart3.enabled = false;

        while (count >= 0)
        {
            countDownTimer.text = count.ToString();
            Debug.Log(count.ToString());
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...s
        StartGame();
    }

    void StartGame()
    {
        kart1.enabled = true;
        kart2.enabled = true;
        kart3.enabled = true;
        countDownTimer.text = "GO";
    }


    public void OnEnable()
    {
        kart1 = transform.GetComponentInChildren<Kart>();
        scoreBoard = transform.GetComponentInChildren<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        scoreBoard.text = kart1.GetCumulativeReward().ToString("f2");
    }
}