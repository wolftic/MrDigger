using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
	[SerializeField]				//[SerializeField] zorgt ervoor dat je het in de editor kan zien.
	private float speed = 1f;		//Gebruik private als je de variabele niet in een ander script gaat gebruiken.
	private float maxSpeed;
	[SerializeField]
	private float slowSpeed = 1f;

	[SerializeField]
	private float screenWidth = 2.5f;
	[SerializeField]
	private ParticleSystem digParticle;

	private PlayerHandler playerHandler;

	private float horizontal = 0f;

	[SerializeField]
	private float maxRotation = 45f;
	[SerializeField]
	private float rotSpeed = 25f;

	void Start() {
		playerHandler = GetComponent<PlayerHandler> ();
		maxSpeed = speed;
	}

    void Update()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);

		bool[] inScreen = InScreen (transform.position.x);

		if (!inScreen[0]){
            transform.Rotate(0, 0, 1f);
        }
		if (!inScreen[1]) {
			transform.Rotate (0, 0, -1f);
		}
		if (inScreen [0] && inScreen [1]) {
			if (Input.touchCount > 0) {
				float widt = Screen.width / 2;
				float tpos = Input.GetTouch (0).position.x - widt;
				float perc = tpos / widt;
				horizontal = perc;

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, horizontal * maxRotation), rotSpeed * Time.deltaTime); // * rotSpeed * Time.deltaTime);
			} else {
				horizontal = Mathf.Lerp (horizontal, 0, Time.deltaTime);
			}
		}

		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp (pos.x, -screenWidth / 2, screenWidth / 2);
		transform.position = pos;

		speed = Mathf.Lerp (speed, maxSpeed, Time.deltaTime);
    }

	[SerializeField]
	private Color[] colors;

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.StartsWith("Blocks")) {
            other.gameObject.SetActive(false);

			if (other.tag == "Blocks_Slow") {							//Ziet er niet efficient uit #needfeedback
				digParticle.startColor = colors [0];
				speed = Mathf.Clamp(speed - slowSpeed, 0, maxSpeed);
			} else if(other.tag == "Blocks_Gold"){
				digParticle.startColor = colors [1];
				playerHandler.AddGold (1);
			} else if(other.tag == "Blocks_Dirt"){
				digParticle.startColor = colors [2];
			} else if(other.tag == "Blocks_Stone"){
				digParticle.startColor = colors [3];
			} else if(other.tag == "Blocks_Gravel"){
				digParticle.startColor = colors [4];
			} 

			digParticle.transform.position = other.transform.position;
			digParticle.Play ();
		}
    }

	/// <summary>
	/// Is the x position in the screen
	/// </summary>
	/// <returns>2 booleans to check if the player is out of screen on the left or the right</returns>
	/// <param name="x">The x coordinate.</param>
	bool[] InScreen(float x) {
		bool[] r = new bool[2];
		r [0] = x > -screenWidth / 2; // als x buiten de -screenWidth/2 is dan is dit false
		r [1] = x < screenWidth / 2; // als x butien de screenWidth/2 is dan is dit false
		return r;
	}
}
