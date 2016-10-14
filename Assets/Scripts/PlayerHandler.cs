using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerHandler : MonoBehaviour {
	[SerializeField]
	private string _name = "EDITOR";
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

	[SerializeField]
	private UnityEvent onDie = new UnityEvent ();

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
        startPos = transform.position;
		freezed = false;
		_gold = 0;
		_score = 0;
	}

    public void ResetPlayer()
    {
        freezed = true;
        transform.position = new Vector3(0, 100, 0);

        Vector3 pos = Camera.main.transform.localPosition;
        pos.y = 7;
        UIScript.current.goTo = pos;

        Pause.current.ForceUnpause();
    }

	public void KillPlayer() {
		killParticle.Play ();
		freezed = true;
		transform.position = new Vector3(0, 100, 0);

		onDie.Invoke ();

		Vector3 pos = Camera.main.transform.localPosition;
		pos.y = 7;
        UIScript.current.goTo = pos;

		Database.database.UploadScoreToServer (_name, _score, _gold);
	}

	public void SetName(UnityEngine.UI.InputField field) {
		_name = field.text;
	}
}
