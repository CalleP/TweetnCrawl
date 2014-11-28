using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

public class ServerConnector : MonoBehaviour {
	bool socketReady = false;

    public TcpClient client;
    public NetworkStream nwStream;


	public String Host = "195.178.179.176";
	public Int32 Port = 8080; 

    public static List<string> Hashtags = new List<string>();
    public static List<string> TwitterNAmes = new List<string>();
    public static string TweetData;

	//Test the connection
	void TestSocketConnection(){

	}

	// Use this for initialization
	public ServerConnector() {

        //---data to send to the server---


        //---create a TCPClient object at the IP and port no.---
        client = new TcpClient(Host, Port);
        nwStream = client.GetStream();
        //var hash = ParseHashtag(Send("Test"));
        //Debug.Log(hash[0]);
        //---read back the text---



        //var newString = outputString.Replace("\n", "");


        //TweetData = newString;



        ////Security.PrefetchSocketPolicy(Host, Port);
        //setupSocket();



        //writeSocket("Test");

        //var test = readSocket();
        //Debug.Log(test.ToString());



        //closeSocket();
        //test = readSocket();
        //Debug.Log(test);
	}

    public string Send(string textToSend)
    {


        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

        //---send the text---
        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

        var outputString = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
        return outputString;
    }

    public string[] ParseHashtag(string hashtagSet)
    {
        hashtagSet = hashtagSet.Replace("{", "");
        hashtagSet = hashtagSet.Replace("}", "");
        var outArr = hashtagSet.Split(',');
        return outArr;
    }

    public void Close() 
    {
        client.Close();
    }

	// Update is called once per frame
	void Update() {
		
	}
	
	

}