using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
