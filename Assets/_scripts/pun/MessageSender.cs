using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MessageSender
{
    public static Action<Message> OnMessageSent;
    
    private static RaiseEventOptions option = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCache
    };
    
    public static void Send(Message message)
    {
        byte[] bytes = MessageParser.GetBytes(message);
        PhotonNetwork.RaiseEvent(0, bytes, option, SendOptions.SendReliable);
        OnMessageSent?.Invoke(message);
    }
}
