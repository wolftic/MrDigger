using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	[SerializeField]
	private float countdownLength = 5f;
	[SerializeField]
	private UnityEngine.UI.Text countdown;

	//[HideInInspector]
	public bool isPaused = false;
	private bool paused = false;

	public static Pause current;

	void Start () {
		current = this;
	}

	public void PauseGame () {
		paused = !paused;
		if (!paused) {
			StartCoroutine (unPauseGame (countdownLength));
		} else {
			isPaused = paused;
		}
	}

	IEnumerator unPauseGame (float seconds) {
		countdown.gameObject.SetActive (true);
		while (seconds > 0) {
			countdown.text = Mathf.Round (seconds).ToString ();
			yield return new WaitForSeconds (1f);
			seconds -= 1;
		}
		isPaused = false;
		countdown.gameObject.SetActive (false);
		yield break;
	}

    public void ForceUnpause()
    {
        paused = false;
        isPaused = false;
    }
}
