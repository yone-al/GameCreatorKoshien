using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 velocity = new Vector2(0, -5);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
