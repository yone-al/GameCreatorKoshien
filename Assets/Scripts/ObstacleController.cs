using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float threshold = -5.5f;
    private GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        Vector2 velocity = new Vector2(0, -5);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().isGameActive)
        {
            Vector2 velocity = new Vector2(0, 0);
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = velocity;
        }

        float posY = this.gameObject.transform.position.y;
        if (posY < threshold)
        {
            Destroy(this.gameObject);
        }
    }
}
