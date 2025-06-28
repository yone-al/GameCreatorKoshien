using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        this.gameObject.transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hp--;
            Debug.Log("hp: " + hp);
        }
    }
}
