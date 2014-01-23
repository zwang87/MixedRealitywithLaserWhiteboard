using UnityEngine;
using System;
using System.Collections;

public class KinectPointController : MonoBehaviour {
	//Assignments for a bitmask to control which bones to look at and which to ignore
	public enum BoneMask
	{
		None = 0x0,
		Hip_Center = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hip_Left = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		Hip_Right = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xFFFFF,
		Torso = 0x10000F, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000F0,
		Right_Arm = 0x100F00,
		Left_Leg = 0x10F000,
		Right_Leg = 0x1F0000,
		R_Arm_Chest = Right_Arm | Spine,
		No_Feet = All & ~(Foot_Left | Foot_Right),
		UpperBody = Shoulder_Center | Head | Shoulder_Left | Elbow_Left | Wrist_Left | Hand_Left|
		Shoulder_Right | Elbow_Right | Wrist_Right | Hand_Right
		
	}
	
	public SkeletonWrapper sw;
	
	public GameObject Shoulder_Center;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	
	private GameObject[] _bones; //internal handle for the bones of the model
	
	public int player;
	public BoneMask Mask = BoneMask.UpperBody;
	
	public float scale = 1.0f;
	
	
	
	LineRenderer bcLineRenderer;
	GameObject bodyConnection;
	
	GameObject viewPoint;
	
	public Material bcMaterial;
	// Use this for initialization
	void Start () {
		//store bones in a list for easier access
		_bones = new GameObject[10] {Shoulder_Center, Head,
			Shoulder_Left, Elbow_Left, Wrist_Left, Hand_Left,
			Shoulder_Right, Elbow_Right, Wrist_Right, Hand_Right};
		
		//initialize the line renderer for the connections between joints of two arms
		bodyConnection = new GameObject();
		bodyConnection.transform.name = "bodyConnection";
		bodyConnection.transform.parent = GameObject.Find("KinectPointMan").transform;
		bodyConnection.AddComponent<LineRenderer>();
		//bodyConnection.renderer.material = bcMaterial;
		bodyConnection.renderer.material = GameObject.Find("23_Hand_Right").renderer.material;
		bcLineRenderer = bodyConnection.GetComponent<LineRenderer>();
        bcLineRenderer.SetWidth(0.02f, 0.02f);
        bcLineRenderer.SetVertexCount(8);
		
		viewPoint = GameObject.Find("OVRCameraController");
	}
	
	// Update is called once per frame
	void Update () {
		if(player == -1)
			return;
		//update all of the bones positions
		if (sw.pollSkeleton())
		{
			for( int ii = 2; ii < 12; ii++) {
				//_bonePos[ii] = sw.getBonePos(ii);
				if( ((uint)Mask & (uint)(1 << ii) ) > 0 ){
					//_bones[ii].transform.localPosition = sw.bonePos[player,ii];
					//get positions of joints of upper body
					_bones[ii-2].transform.localPosition = new Vector3(
						sw.bonePos[player,ii].x * scale,
						sw.bonePos[player,ii].y * scale,
						sw.bonePos[player,ii].z * scale);
				}
			}
			//set the position of player's head to viewPoint
			viewPoint.transform.position = Head.transform.position;
		}
		//body connections between body joints of two arms
		for(int i = 0; i < 4; i++){
			//connect joints of left arm
			bcLineRenderer.SetPosition(3-i, _bones[i+2].transform.position);
			//connect joints of right arm
			bcLineRenderer.SetPosition(i+4, _bones[i+6].transform.position);
		}
	}
}
