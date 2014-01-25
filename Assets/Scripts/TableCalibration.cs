using UnityEngine;
using System.Collections;

public class TableCalibration : MonoBehaviour {
	GameObject xSlider;
	GameObject ySlider;
	GameObject zSlider;
	GameObject psMove;

	Vector3 barPos;
	Vector3 temp;
	private bool isTriggered = false;

	// Use this for initialization
	void Start () {
		temp = this.transform.position;
		xSlider = GameObject.Find("XSliderBar");
		ySlider = GameObject.Find("YSliderBar");
		zSlider = GameObject.Find("ZSliderBar");
		psMove = GameObject.Find("PSMoveController");


	}

	// Update is called once per frame
	void Update () {
		isTriggered = psMove.GetComponent<PSMoveController>().sliderBarTriggered;
		if(isTriggered){
			Debug.Log(xSlider.transform.localPosition.x + "  " + ySlider.transform.localPosition.x + "  " + isTriggered);
			this.transform.position = new Vector3(temp.x+xSlider.transform.localPosition.x, temp.y+ySlider.transform.localPosition.x, temp.z+zSlider.transform.localPosition.x);
		}
	}

}