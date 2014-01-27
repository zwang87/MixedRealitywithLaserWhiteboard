using UnityEngine;
using System.Collections;

public class YSliderController : MonoBehaviour {
	public double minV = -0.5, maxV = 0.5, curV = 0;
	float xV = 0, yV = 0;
	
	GameObject psMove, table;
	Vector3 tablePos;
	Vector3 localPos;
	
	Vector3 barPos;// = this.transform.localPosition;
	Vector3 temp;// = tablePos;
	// Use this for initialization
	void Start () {
		//this.transform.tag = "SliderBar";
		psMove = GameObject.Find("PSMoveController");
		table = GameObject.Find("Table");	
		tablePos = table.transform.position;
		localPos = Vector3.zero;
		
		barPos = this.transform.localPosition;
		temp = tablePos;
	}
	
	// Update is called once per frame
	void Update () {
		localPos = this.transform.parent.transform.InverseTransformPoint(psMove.transform.position);
		//Debug.Log(localPos.x + " " + localPos.y + " " + localPos.z);
		clampBarPosition();
	}
	
	private void clampBarPosition(){
		float value;
		value = barPos.x = Mathf.Clamp (localPos.x, (float)-maxV, (float)maxV);
		barPos.y = 0;
		barPos.z = 0;

		
		this.transform.localPosition = barPos;
		table.transform.position = tablePos + new Vector3(0, value, 0);
	}
}
