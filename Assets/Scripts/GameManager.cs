using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public GameObject gameOverPanel;
    public float timeLimit = 60.0f;
    public float timeCount = 0.0f;
    public GameObject timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        timeCount += Time.deltaTime;

        timerText.GetComponent<TextMeshProUGUI>().text = "8:" + ((int)timeCount).ToString().PadLeft(2, '0');
        Debug.Log(timeCount);

        if (timeCount >= timeLimit)
        {
            timeCount = timeLimit;
            GameOver();
            timerText.GetComponent<TextMeshProUGUI>().text = "9:00";
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }
}
