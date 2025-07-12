using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public GameObject gameOverPanel;
    public float timeLimit = 60.0f;
    private float timeLeft;
    public GameObject timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            timeLeft = 0.0f;
            GameOver();
        }

        Debug.Log(timeLeft);
        timerText.GetComponent<TextMeshProUGUI>().text = timeLeft.ToString("F0");
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }
}
