using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceiver : MonoBehaviour {
	Thread receiveThread;
	UdpClient udpClient;
	public const int port = 9090;
	public string lastReceivedUDPpacket = "";

	private double posX = 0;
	private double posY = 0;
	private string content = "";
	private System.Object thisLock = new System.Object();

	// Use this for initialization
	void Start () {
		receiveThread = new Thread(new ThreadStart(this.ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(posX + "  " + posY);
		if(posX >= 0 && posY >= 0)
			this.transform.localPosition = new Vector3((float)(5*(posX-160.0)/160), 0, (float)(5*(120.0-posY)/120));
	}

	private void ReceiveData(){
		udpClient = new UdpClient(port);

		while(true){
			try{
				IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = udpClient.Receive(ref ip);
				lock(thisLock){
					content = Encoding.UTF8.GetString (data);
					string[] array = content.Split(' ');
					if("pos:" == array[0]){
						posX = Convert.ToDouble(array[1]);
						posY = Convert.ToDouble(array[2]);
					}
				}
			}catch(Exception e){
				Debug.Log(e.ToString());
			}
		}
		udpClient.Close();
	}

	void OnDisable(){
		if(receiveThread != null)
			receiveThread.Abort();
		Debug.Log("Connection Lost");
	}
}