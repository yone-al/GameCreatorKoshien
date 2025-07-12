using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 3.0f;
    public float limitX = 2.5f;
    public float limitY = 4.5f;

    public GameObject gameManager;
    public GameObject hpText;

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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        this.gameObject.transform.Translate(movement);

        // Mathf.Clampを使って値を範囲内に収める
        float clampedX = Mathf.Clamp(this.gameObject.transform.position.x, -limitX, limitX);
        this.gameObject.transform.position = new Vector3(clampedX, this.gameObject.transform.position.y, 0);
        float clampedY = Mathf.Clamp(this.gameObject.transform.position.y, -limitY, limitY);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, clampedY, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hp--;
            Debug.Log("hp: " + hp);
            hpText.GetComponent<TextMeshProUGUI>().text = "HP: " + hp;

            if (hp <= 0)
            {
                gameManager.GetComponent<GameManager>().GameOver();
            }
        }
    }
}
