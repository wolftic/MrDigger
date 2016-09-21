using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour {
	[SerializeField]
	private int size = 0; //Grote van chunk
	[SerializeField]
	[Range(0, 1)]
	private float zoom = 1f; //Perlin noise zoom level
	[SerializeField]
	private float maxRange; //View distance
	[SerializeField]
	private float maxRangeToDelete = 50f;

	[SerializeField]
	private float updateInterval; //Hoe snel kijken we of een chunk gespawned moet worden?
	private float time;

	[SerializeField]
	private GameObject[] blocks = new GameObject[0]; //Spawnable blocks
	[SerializeField]
	public GameObject background;

	private List<GameObject> chunks;
	private float offset = 0f; //Distance offset
	private float seed = 1000f; //Random seed

	void Start () {
		chunks = new List<GameObject>();

		offset = Mathf.Sqrt (Mathf.Pow (size / 2f, 2f) * 2f);
		seed = Random.Range (0, 100000);

		GenerateChunk (Vector2.zero, size);
	}

	void Update () {
		if (time < Time.time) {
			time = Time.time + updateInterval;
			OnCheck ();
		}
	}

	void OnCheck() {
		float closest = 0;
		GameObject[] g = chunks.ToArray ();

		for (int i = 0; i < g.Length; i++) {
			if (g [i] == null)
				continue;
			
			float dist = Vector3.Distance (transform.position, g[i].transform.position);
 
			g [i].SetActive (dist<maxRange); //Blocken die te ver van de generator zijn worden uitgeschakeld

			if (dist > maxRangeToDelete)
				Destroy (g [i]);

			if (!g [i].activeSelf)
				continue;

			if (closest > dist || closest == 0) { 
				closest = dist;
			}
		}

		if (closest > offset) {
			float x = transform.position.x / size;
			float y = transform.position.y / size;

			x = Mathf.Round (x) * size;
			y = Mathf.Round (y) * size;

			GenerateChunk (new Vector2(x, y), size);
		}
	}

	void GenerateChunk(Vector2 xy, float size) {
		GameObject map = new GameObject (xy + " / " + size);
		map.transform.position = new Vector3 (xy.x, xy.y);

		float min = -(size / 2);
		float max = -min;

		for (float x = min; x < max; x++) {
			for (float y = min; y < max; y++) {
				float n = Mathf.Clamp01(Mathf.PerlinNoise ((xy.x + x + .5f + seed) * zoom, (xy.y + y + .5f + seed) * zoom));
				float p = 1f / blocks.Length;

				for (int i = 0; i < blocks.Length; i++) {
					float mx = p * (i+1);
					float mi = p * i;

					if (n >= mi && n < mx) {
						GameObject prefab = blocks [i];

						GameObject block = Instantiate (prefab) as GameObject;
						block.transform.position = new Vector3 (xy.x + x + .5f, xy.y + y + .5f);
						block.transform.SetParent (map.transform);

						break;
					}
				}

				GameObject bg = Instantiate (background) as GameObject;
				bg.transform.position = new Vector3 (xy.x + x + .5f, xy.y + y + .5f);
				bg.transform.SetParent (map.transform);
			}
		}
		chunks.Add (map);
	}
}
