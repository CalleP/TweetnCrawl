using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class ServerConnector : 
MonoBehaviour {
	bool socketReady = false;
	
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	String Host = "195.178.179.176";
	Int32 Port = 8080; 

	//Test the connection
	void TestSocketConnection(){
		setupSocket ();
		writeSocket ("test");
		Debug.Log(readSocket());
		closeSocket ();
	}

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	public void setupSocket() {
		try {
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error:" + e);
		}
	}
	
	public void writeSocket(string theLine) {
		if (!socketReady)
			return;
		String tmpString = theLine + "\r\n";
		theWriter.Write(tmpString);
		theWriter.Flush();
	}
	
	public String readSocket() {
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return theReader.ReadLine();
		return "";
	}
	
	public void closeSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
	
	public void maintainConnection(){
		if(!theStream.CanRead) {
			setupSocket();
		}
	}
} // end class s_TCP