using UnityEngine;
using System.Collections;

public class SliderBarController : MonoBehaviour {
	public double minV = -0.5, maxV = 0.5, curV = 0;

	public enum SliderType{
		XSlider = 0,
		YSlider = 1,
		ZSlider = 2
	};

	public SliderType sliderType;// = SliderType.XSlider;
	GameObject psMove;
	Vector3 localPos;
	// Use this for initialization
	void Start () {
		//this.transform.tag = "SliderBar";
		psMove = GameObject.Find("PSMoveController");
		localPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		localPos = this.transform.parent.transform.InverseTransformPoint(psMove.transform.position);
		//Debug.Log(localPos.x + " " + localPos.y + " " + localPos.z);
		clampBarPosition();
	}

	private void clampBarPosition(){
		Vector3 barPos = this.transform.localPosition;
		switch(sliderType){
		case SliderType.XSlider:
			barPos.x = Mathf.Clamp (localPos.x, (float)-maxV, (float)maxV);
			barPos.y = 0;
			barPos.z = 0;
			break;
		case SliderType.YSlider:
			barPos.y = 0;
			barPos.x = Mathf.Clamp (localPos.x, (float)-maxV, (float)maxV);
			barPos.z = 0;
			break;
		case SliderType.ZSlider:
			barPos.x = 0;
			barPos.y = 0;
			barPos.z = Mathf.Clamp (localPos.z, (float)-maxV, (float)maxV);
			break;
		}

		this.transform.localPosition = barPos;
	}
}
