using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIScript : MonoBehaviour
{
    public Button butt;
    public GameObject monster;
    public Vector3 goTo;
    private bool goToPlace = false;
    public static UIScript current;

    void Start()
    {
        current = this;
        Button btn = butt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void Update() {
        if (goToPlace) {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, goTo, 1.5f * Time.deltaTime);
        }
       
    }

    void TaskOnClick() {
       goTo  = new Vector3(0, -4.26f, -13.222f);
        goToPlace = true;
    }
}
