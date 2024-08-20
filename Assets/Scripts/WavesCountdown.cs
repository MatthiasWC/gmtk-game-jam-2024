using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesCountdown : MonoBehaviour
{
    [System.NonSerialized] public static WavesCountdown instance;

    public float prepTime = 20f;
    private int countdownSeconds = 5;
    [SerializeField] private TextMeshProUGUI countdownTextBox;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        countdownTextBox = GetComponent<TextMeshProUGUI>();

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(prepTime - countdownSeconds);

        for (int i = countdownSeconds; i > 0; i--)
        {
            countdownTextBox.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownTextBox.text = "climb, ye fool!";
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
