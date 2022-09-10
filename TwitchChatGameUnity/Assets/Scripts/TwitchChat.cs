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
        //Debug.Log(TwitchAccountCredentials.username);
        //Debug.Log(TwitchAccountCredentials.password); Probably shouldn't keep this line in, for obvious reasons.
        //Debug.Log(TwitchAccountCredentials.channelName);

        reconnectAfter = 60.0f;
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (twitchClient.Available == 0)
        {
            reconnectTimer += Time.deltaTime;

            if(reconnectTimer >= reconnectAfter)
            {
                Connect();
                reconnectTimer = 0f;
            }
        }

        //Debug.Log(twitchClient.Available);
        //Debug.Log(twitchClient.GetStream());
        ReadChat();
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());
        writer.WriteLine("PASS " + TwitchAccountCredentials.password);
        writer.WriteLine("NICK " + TwitchAccountCredentials.username);
        writer.WriteLine("USER " + TwitchAccountCredentials.username + " 8 *:" + TwitchAccountCredentials.username);
        writer.WriteLine("JOIN #" + TwitchAccountCredentials.channelName);
        writer.Flush();
    }

    public void ReadChat()
    {
        if(twitchClient.Available > 0)
        {
            string message = reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                Debug.Log(message);
            }
        }
    }
}
