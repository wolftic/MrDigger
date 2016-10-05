﻿using UnityEngine;
using System.Collections;

public class Database : MonoBehaviour {
	[SerializeField]
	private string url;

	public static Database database;

	void Start () {
		database = this;
	}

	public void UploadScoreToServer(string name, int score, int gold) {
		StartCoroutine (UploadScoreToServerIE (name, score, gold));
	}

	IEnumerator UploadScoreToServerIE(string name, int score, int gold) {
		int checksum = CheckSum(name, score);
	
		WWWForm form = new WWWForm ();
		form.AddField ("NAME", name);
		form.AddField ("SCORE", score);
		form.AddField ("GOLD", gold);
		form.AddField ("CHECK", checksum);

		WWW www = new WWW (url, form);

		Debug.Log (form);

		yield return www;
		if (!string.IsNullOrEmpty (www.text)) {
			Debug.Log ("Message gotten: " + www.text);
		} else {
			Debug.Log ("Succes!");
		}

		yield break;
	}

	int CheckSum(string name, int score) {
		int checkSum = name.Length * name.Length + score;

		return checkSum;
	}
}
