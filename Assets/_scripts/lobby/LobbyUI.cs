using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class LobbyUI : MonoBehaviour
{
    public Lobby Lobby;
    [FormerlySerializedAs("LobbyRoom")] public LobbyRoomUI lobbyRoomUI;

    public GameObject ObjOut;
    public GameObject ObjLobby;
    public GameObject ObjRoom;
    public GameObject ObjBusy;
    
    private void OnEnable()
    {
        Lobby.AddAction(StateUpdated);
    }
    
    private void OnDisable()
    {
        Lobby.RemoveAction(StateUpdated);
    }

    private void StateUpdated(LobbyState lobbyState)
    {
        ObjOut.SetActive(lobbyState == LobbyState.OUT);
        ObjLobby.SetActive(lobbyState == LobbyState.LOBBY);
        ObjRoom.SetActive(lobbyState == LobbyState.ROOM);
        ObjBusy.SetActive(lobbyState == LobbyState.BUSY);
        
        if(lobbyState == LobbyState.ROOM)
            lobbyRoomUI.UpdateRoom();
    }
}
