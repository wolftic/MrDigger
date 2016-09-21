using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
	[SerializeField]				//[SerializeField] zorgt ervoor dat je het in de editor kan zien.
	private float speed = 1f;		//Gebruik private als je de variabele niet in een ander script gaat gebruiken.
	[SerializeField]
	private float rotSpeed = 25f;

    void Update()
    {
        /*transform.Translate((-Vector2.up * 0.5f + Vector2.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(0, 0, 2);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, -5);
        }*/
        transform.Translate(-transform.up * speed * Time.deltaTime);
        transform.Rotate(0, 0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime);

        if (transform.position.x < 2.7)
        {
            transform.Rotate(0, 0, 1f);
        }
        if (transform.position.x > -2.7)
        {
            transform.Rotate(0, 0, -1f);
        }

    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Blocks")) {
            other.gameObject.SetActive(false);
        }
    }

}
