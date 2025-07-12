using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public float interval = 1.0f;
    public float rangeX = 2.5f;
    public float posY = 5.5f;

    private float time = 0.0f;
    public GameObject gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().isGameActive)
        {
            return;
        }

        time += Time.deltaTime;
        // 1秒に1回、ランダムな位置に障害物を生成
        if (time >= interval)
        {
            int randomIndex = Random.Range(0, obstacles.Length);
            GameObject obstacle = obstacles[randomIndex];
            float randomX = Random.Range(-rangeX, rangeX);
            Instantiate(obstacle, new Vector3(randomX, posY, 0), Quaternion.identity);
            time = 0.0f;
        }
    }
}
