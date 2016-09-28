using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {
	[SerializeField]
	private int _gold;

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
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
