using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public GameObject gameOverPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }
}
