using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ServerConnector : MonoBehaviour {
	bool socketReady = false;

    public TcpClient client;
    public NetworkStream nwStream;


	public String Host = "195.178.179.176";
	public Int32 Port = 8080; 

    public static List<string> Hashtags = new List<string>();
    public static List<string> TwitterNAmes = new List<string>();
    public static string TweetData;

    public bool OfflineMode = false;


    void Start(){
        var time = Time.realtimeSinceStartup;
        Connect();
        var output = Send("GetTopList");

        Debug.Log(ParseTopList(output));
        Close();

    }

    public void Connect() {



        IAsyncResult result;
        Action action = () =>
        {
            client = new TcpClient(Host, Port);
            nwStream = client.GetStream();
        };

        result = action.BeginInvoke(null, null);

        if (result.AsyncWaitHandle.WaitOne(3000))
            Console.WriteLine("Method successful.");
        else
            OfflineMode = true;
    }

    public string Send(string textToSend)
    {

        if (OfflineMode)
        {
             return GenerateOfflineString();

        }

        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

        //---send the text---
        try
        {
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }
        catch (Exception)
        {
            OfflineMode = true;
            return GenerateOfflineString();
        }
       

        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

        var outputString = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
        return outputString;
    }

    public string GenerateOfflineString()
    {
        System.Random rand = new System.Random();
        string output = "{";
        for (int i = 0; i < 10; i++)
        {
            output = output + "#LostInTwitter" + rand.Next(0, 5000) + ", ";
        }
        output = output + "#LostInTwitter}";

        return output;
    }

    public string[] ParseHashtag(string hashtagSet)
    {
        hashtagSet = hashtagSet.Replace("{", "");
        hashtagSet = hashtagSet.Replace("}", "");
        var outArr = hashtagSet.Split(',');
        return outArr;
    }

    public List<HashTagSet> ParseTopList(string hashtagSet)
    {
        hashtagSet = hashtagSet.Replace("[", "");
        hashtagSet = hashtagSet.Replace("]", "");
        
        //var outArr = hashtagSet.Split('\n');

        Regex r = new Regex(@"{(.+?)}");
        MatchCollection mc = r.Matches(hashtagSet);

        List<HashTagSet> outList = new List<HashTagSet>();


        for (int i = 0; i < mc.Count; i++)
		{
			var val = mc[i].Groups[1].Value;
            val = val.Replace("{","").Replace("}","");
            var split = val.Split(',');
            var setValue = split[0].Replace("<<", "").Replace(">>", "");
            setValue = setValue.Replace("\\", "").Replace("\"", "");
            var setAmount = split[1];
            outList.Add(new HashTagSet(setValue,  Int32.Parse(setAmount)));

		}




        return outList;
    }

    public void Close() 
    {
        if (OfflineMode)
        {
            return;
        }
        client.Close();
    }
}

public class HashTagSet 
{
    public string Value;
    public int Amount;
    public HashTagSet(string value, int amount)
    {
        this.Value = value;
        this.Amount = amount;
    }
}