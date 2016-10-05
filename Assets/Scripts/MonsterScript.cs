using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {
	[SerializeField]			//[SerializeField] zorgt ervoor dat je het in de editor kan zien.
	private float speed = 1f;	//Gebruik private als je de variabele niet in een ander script gaat gebruiken.
	
	void Update () {
		transform.Translate( -Vector3.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.StartsWith ("Blocks")) {
			other.gameObject.SetActive (false);
		} else if (other.CompareTag ("Player")) {
			other.GetComponent<PlayerHandler> ().KillPlayer ();
		}
    }   
}
