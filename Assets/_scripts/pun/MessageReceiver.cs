using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public Action<Message> OnReceived;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }
    
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case  0:
                Message message = MessageParser.FromBytes<Message>((byte[])photonEvent.CustomData);
                OnReceived?.Invoke(message);  
                break;
        }
    }
}
