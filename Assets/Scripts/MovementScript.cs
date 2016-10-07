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
	[SerializeField]
	private ParticleSystem bombParticle;
	[SerializeField]
	private Animator mineAnimation;

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
		if (Pause.current.isPaused)
			return;
			
		if (playerHandler.freezed)
			return; 

        transform.Translate(-transform.up * speed * Time.deltaTime);

		bool[] inScreen = InScreen (transform.position.x, transform.position.y);

		if (!inScreen[0]){
            transform.Rotate(0, 0, 1f);
        }
		if (!inScreen[1]) {
			transform.Rotate (0, 0, -1f);
		}
		if (!inScreen [2]) {
			speed = 0;
		}

		if (inScreen [0] && inScreen [1]) {
			if (Input.touchCount > 0) {
				float widt = Screen.width / 2;
				float tpos = Input.GetTouch (0).position.x - widt;
				float perc = tpos / widt;
				horizontal = perc;

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, horizontal * maxRotation), rotSpeed * Time.deltaTime); // * rotSpeed * Time.deltaTime);
			} else if (Input.GetAxisRaw("Horizontal") != 0) {
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, Input.GetAxis("Horizontal") * maxRotation), rotSpeed * Time.deltaTime);
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
			Animator anim = (Animator)Instantiate (mineAnimation, other.transform.position, Quaternion.identity) as Animator;
			anim.SetTrigger ("Play");
			Destroy (anim.gameObject, 1f / 12f * 5f);
			StartCoroutine (RemoveBlock (other.gameObject));
		}
    }

	IEnumerator RemoveBlock(GameObject block) {
		yield return new WaitForSeconds (1f / 12f * 5f);

		block.gameObject.SetActive(false);

		Color startColor = Color.white;

		switch (block.tag) {
		case "Blocks_Slow":						//Ziet er niet efficient uit #needfeedback
			startColor = colors [0];
			speed = Mathf.Clamp (speed - slowSpeed, 0, maxSpeed);
			break;
		case "Blocks_Gold":
			startColor = colors [1];
			playerHandler.AddGold (1);
			break;
		case "Blocks_Dirt":
			startColor = colors [2];
			break;
		case "Blocks_Stone":
			startColor = colors [3];
			break;
		case "Blocks_Gravel":
			startColor = colors [4];
			break;
		case "Blocks_Bomb":
			startColor = colors [5];
			bombParticle.transform.position = block.transform.position;
			bombParticle.Play ();
			playerHandler.KillPlayer ();
			break;
		default:
			Debug.Log ("Not found");
			break;
		}

		digParticle.startColor = startColor;
		digParticle.transform.position = block.transform.position;
		digParticle.Play ();

		yield break;
	}

	/// <summary>
	/// Is the x position in the screen
	/// </summary>
	/// <returns>2 booleans to check if the player is out of screen on the left or the right</returns>
	/// <param name="x">The x coordinate.</param>
	bool[] InScreen(float x, float y) {
		bool[] r = new bool[3];
		r [0] = x > -screenWidth/2; // als x buiten de -screenWidth/2 is dan is dit false
		r [1] = x < screenWidth/2; // als x butien de screenWidth/2 is dan is dit false
		r [2] = y > Camera.main.ScreenToWorldPoint(Vector3.zero).y;
		return r;
	}
}

