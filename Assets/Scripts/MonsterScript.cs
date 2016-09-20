using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	
	void Update () {
        transform.Translate(0, -0.025f, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blocks"))
        {
            other.gameObject.SetActive(false);
        }
    }   
}
