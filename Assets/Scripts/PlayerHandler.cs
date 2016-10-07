using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {
	[SerializeField]
	private int _gold = 0;
	[SerializeField]
	private int _score = 0;

	private Vector3 startPos;
	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private UnityEngine.UI.Text GOLD;
	[SerializeField]
	private UnityEngine.UI.Text METER;
	[SerializeField]
	private ParticleSystem killParticle;

	[HideInInspector]
	public bool freezed = true;

	public int gold {
		get {
			return _gold;
		}
	}

	/// <summary>
	/// Adds gold to our gold.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void AddGold (int amount) {
		_gold += amount;
		GOLD.text = _gold.ToString() + "g";
	}

	void Start () {
		startPos = transform.position;
	}

	void Update () {
		if (Pause.current.isPaused)
			return;
		_score = (int)Mathf.Abs(Mathf.Round(startPos.y - transform.position.y));
		METER.text = _score + "m";
	}

	public void SpawnPlayer() {
		transform.position = spawnPoint.position;
		freezed = false;
		_gold = 0;
		_score = 0;
	}

	public void KillPlayer() {
		Database.database.UploadScoreToServer ("EDITOR", _score, _gold);
		killParticle.Play ();
		freezed = true;
	//	gameObject.SetActive (false);
	}
}
