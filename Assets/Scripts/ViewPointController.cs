using UnityEngine;
using System.Collections;

public class ViewPointController : MonoBehaviour {
	public int player;
	public SkeletonWrapper sw;
	public float scale = 3.0f;
	//transform vector for head & hand point
	private OVRCameraController ovrCamera;
	
	//head position
	private int headIndex;
	private Vector3 initHeadPos = Vector3.zero;
	private Vector3 headPos = Vector3.zero;
	private Vector3 newHeadVec = Vector3.zero;
	private Vector3 curHeadPos = Vector3.zero;
	private bool initialized = false;
	
	//hand position
	private int handIndex;
	private Vector3 initHandPos = Vector3.zero;
	private Vector3 handPos = Vector3.zero;
	private Vector3 curHandPos = Vector3.zero;
	private Vector3 newHandVec = Vector3.zero;
	
	//hand control sphere
	private GameObject handPoint;
	
	
	
	
	// Use this for initialization
	void Start () {
		headIndex = (int)Kinect.NuiSkeletonPositionIndex.Head;
		handIndex = (int)Kinect.NuiSkeletonPositionIndex.HandRight;
		
		//ovrCamera = GetComponent<OVRCameraController>();
		
		
		//handPoint = GameObject.Find("MidPtSph");
		
		/*
		handPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		handPoint.name = "MidPtSph";
		handPoint.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
		//handPoint.tag = "RingMarker";
		//handPoint.AddComponent("CollisionHandler");
		handPoint.AddComponent<Rigidbody>();
		handPoint.collider.isTrigger = false;
		handPoint.rigidbody.isKinematic = true;
		handPoint.rigidbody.useGravity = false;
		handPoint.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		
		*/
	}
	
	
	
	// Update is called once per frame
	void Update () {
		//ovrCamera.transform.localPosition = 
		if(sw.pollSkeleton()){
			if(sw.boneState[player, headIndex] == Kinect.NuiSkeletonPositionTrackingState.Tracked
				&& sw.boneState[player, handIndex] == Kinect.NuiSkeletonPositionTrackingState.Tracked){
			
				if(!initialized){//get head position
					initHeadPos = new Vector3(
						sw.bonePos[player, headIndex].x,
						sw.bonePos[player, headIndex].y,
						sw.bonePos[player, headIndex].z);
					
					initHandPos = new Vector3(
						sw.bonePos[player, handIndex].x,
						sw.bonePos[player, handIndex].y,
						sw.bonePos[player, handIndex].z);
						
			
					initialized = true;
					
					
				}
				else{
					headPos = new Vector3(
						sw.bonePos[player, headIndex].x,
						sw.bonePos[player, headIndex].y,
						sw.bonePos[player, headIndex].z);
					newHeadVec = headPos - initHeadPos;
					curHeadPos = initHeadPos + newHeadVec;
				
					/*
					handPos = new Vector3(
						sw.bonePos[player, handIndex].x,
						sw.bonePos[player, handIndex].y,
						sw.bonePos[player, handIndex].z);
					
					newHandVec = (handPos - initHandPos) * scale;
					curHandPos = initHandPos + newHandVec;
					handPoint.transform.localPosition = curHandPos;
					*/
					
					//Debug.Log (headPos.x + " " + headPos.y + " " + initHeadPos.x + " " +initHeadPos.y + " " + newHeadVec.x + " " +newHeadVec.y);
					//Debug.Log (handPos.x + " " + handPos.y + " " + handPos.z);
					//ovrCamera.transform.localPosition = curHeadPos;
					this.gameObject.transform.localPosition = curHeadPos;
				}
			}
			
		}
		//Debug.Log(curHeadPos.x + " " + curHeadPos.y + " " + curHeadPos.z);
	}
}
