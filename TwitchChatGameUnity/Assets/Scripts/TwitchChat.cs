using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;
    private float reconnectTimer;
    private float reconnectAfter;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TwitchAccountCredentials.username);
        //Debug.Log(TwitchAccountCredentials.password); Probably shouldn't keep this line in, for obvious reasons.
        Debug.Log(TwitchAccountCredentials.channelName);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
