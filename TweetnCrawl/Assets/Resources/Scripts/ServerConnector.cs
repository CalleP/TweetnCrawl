using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

public class ServerConnector : 
MonoBehaviour {
	bool socketReady = false;
	
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	String Host = "195.178.179.176";
	Int32 Port = 8080; 

    public static List<string> Hashtags = new List<string>();
    public static List<string> TwitterNAmes = new List<string>();
    public static string TweetData;

	//Test the connection
	void TestSocketConnection(){

	}

	// Use this for initialization
	void Start() {

        //---data to send to the server---
        string textToSend = "Test";

        //---create a TCPClient object at the IP and port no.---
        TcpClient client = new TcpClient(Host, Port);
        NetworkStream nwStream = client.GetStream();
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

        //---send the text---
        Console.WriteLine("Sending : " + textToSend);
        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

        //---read back the text---
        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

        var outputString = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
        Debug.Log("Received : " + Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));

        var newString = outputString.Replace("\n", "");


        TweetData = newString;
        Console.ReadLine();
        client.Close();

        
        ////Security.PrefetchSocketPolicy(Host, Port);
        //setupSocket();



        //writeSocket("GetTestData");

        //var test = readSocket();
        //Debug.Log(test.ToString());



        //closeSocket();
        //test = readSocket();
        //Debug.Log(test);
	}

    

	// Update is called once per frame
	void Update() {
		
	}
	

    //remove following functions?
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
		String tmpString = theLine;
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