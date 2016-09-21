using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {
	[SerializeField]
	private float speed = 1f;
	
	void Update () {
		transform.Translate( -Vector3.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Blocks"))
        {
            other.gameObject.SetActive(false);
        }
    }   
}
