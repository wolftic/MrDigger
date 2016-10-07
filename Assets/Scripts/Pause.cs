using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	[SerializeField]
	private float countdownLength = 5f;
	private bool isPaused;

	public void PauseGame () {
		isPaused = !isPaused;
		if (isPaused) {
			Time.timeScale = 0;
		} else {
			StartCoroutine (unPauseGame (countdownLength));
		}
	}

	IEnumerator unPauseGame (float seconds) {
		yield return new WaitForSeconds (seconds);
		Time.timeScale = 1;
		yield break;
	}
}
