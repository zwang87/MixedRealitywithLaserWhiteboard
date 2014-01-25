using UnityEngine;
using System.Collections;

public class SliderBarController : MonoBehaviour {
	public double minV = -0.5, maxV = 0.5, curV = 0;
	public float xV = 0, yV = 0;
	public enum SliderType{
		XSlider = 0,
		YSlider = 1,
		ZSlider = 2
	};

	public SliderType sliderType;// = SliderType.XSlider;
	GameObject psMove, table;
	Vector3 tablePos;
	Vector3 localPos;

	public Vector3 barPos;// = this.transform.localPosition;
	Vector3 temp;// = tablePos;
	// Use this for initialization
	void Start () {
		psMove = GameObject.Find("PSMoveController");
		table = GameObject.Find("Table");	
		tablePos = table.transform.position;
		localPos = Vector3.zero;
		barPos = this.transform.localPosition;
	}


	bool test = false;
	// Update is called once per frame
	void Update () {
		bool isTriggered = GameObject.Find("PSMoveController").GetComponent<PSMoveController>().sliderBarTriggered;

		//if(!isTriggered)
		//	test = false;
		if(test && isTriggered){
			localPos = this.transform.parent.transform.InverseTransformPoint(psMove.transform.position);

		}
		//Debug.Log(localPos.x + " " + localPos.y + " " + localPos.z);
		clampBarPosition();
		

	}

	void OnTriggerEnter (Collision other){

		if(other.gameObject.transform.tag == "PSMove"){
			//clampBarPosition();
			test = true;
		}

	}

	void OnTriggerExit (Collision other){
		
		if(other.gameObject.transform.tag == "PSMove"){
			//clampBarPosition();
			test = false;
		}
		
	}


	private void clampBarPosition(){
		//switch(sliderType){
		//case SliderType.XSlider:
			barPos.x = Mathf.Clamp (localPos.x, (float)-maxV, (float)maxV);
			barPos.y = 0;
			barPos.z = 0;
			/*
			break;
		case SliderType.YSlider:
			barPos.y = 0;
			yV = barPos.x = Mathf.Clamp (localPos.x, (float)-maxV, (float)maxV);
			barPos.z = 0;
			break;
		case SliderType.ZSlider:
			barPos.x = 0;
			barPos.y = 0;
			barPos.z = Mathf.Clamp (localPos.z, (float)-maxV, (float)maxV);
			break;
		}
		*/
		//bool isTriggered = GameObject.Find("PSMoveController").GetComponent<PSMoveController>().sliderBarTriggered;
		//Debug.Log(isTriggered);
		//if(isTriggered)
			this.transform.localPosition = barPos;
		//table.transform.position = tablePos + new Vector3(xV, yV, 0);
	}
}