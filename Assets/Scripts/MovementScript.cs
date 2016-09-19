using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    public float speed;

    

    void Update()
    {
        transform.Translate((-Vector2.up * 0.5f + Vector2.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(0, 0, 2);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, -5);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Blocks")) {
            other.gameObject.SetActive(false);
        }
    }

}
