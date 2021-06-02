using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobbyRoomUI : RoomUI
{
    public Button BtnPlay;
    public Text TxtRoomName;
    
    private void OnEnable()
    {
        PUNCallback.Instance.AddAction(PUNState.ONPLAYER_ENTERED_ROOM, UpdateRoom);
        PUNCallback.Instance.AddAction(PUNState.ONPLAYER_LEFT_ROOM, UpdateRoom);
        PUNCallback.Instance.AddAction(PUNState.ONMASTER_CLIENT_SWITCHED, UpdateRoom);
    }   
    
    private void OnDisable()
    {
        PUNCallback.Instance.RemoveAction(PUNState.ONPLAYER_ENTERED_ROOM, UpdateRoom);
        PUNCallback.Instance.RemoveAction(PUNState.ONPLAYER_LEFT_ROOM, UpdateRoom);
        PUNCallback.Instance.RemoveAction(PUNState.ONMASTER_CLIENT_SWITCHED, UpdateRoom);
    }
    
    public override void UpdateRoom(PUNArgs punArgs = default)
    {
        base.UpdateRoom(punArgs);
        
        TxtRoomName.text = PhotonNetwork.CurrentRoom.Name;
        BtnPlay.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient;
    }
}
