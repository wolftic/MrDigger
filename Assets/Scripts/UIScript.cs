using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIScript : MonoBehaviour
{
    public Button butt;
    public GameObject monster;
    private Vector3 goTo;
    private bool goToPlace = false;

    void Start()
    {
        Button btn = butt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void Update() {
        if (goToPlace) {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, goTo, 3f * Time.deltaTime);
            gameObject.GetComponent<Text>().enabled = false;
        }
       
    }

    void TaskOnClick() {
       goTo  = new Vector3(0, -4.26f, -13.222f);
        goToPlace = true;
    }
}
