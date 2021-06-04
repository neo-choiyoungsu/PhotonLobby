using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Lobby : MonoBehaviour
{
    public InputField IpfUserName;
    public InputField IpfRoomName;

    private Action<LobbyState> onChangeState;

    private void OnEnable()
    {
        PUNCallback.Instance.AddAction(PUNState.ONCONNECTED_TO_MASTER, OnConnectedToMaster);
        PUNCallback.Instance.AddAction(PUNState.ONDISCONNECTED, OnDisconnected);
        PUNCallback.Instance.AddAction(PUNState.ONCREATE_JOIN_RANDOM_FAILED, OnRandomFailed);
        PUNCallback.Instance.AddAction(PUNState.ONCREATE_ROOM_FAILED, OnCreateFailed);
        PUNCallback.Instance.AddAction(PUNState.ONJOINED_ROOM, OnJoinedRoom);
        PUNCallback.Instance.AddAction(PUNState.ONLEFT_ROOM, OnLeftRoom);
        PUNCallback.Instance.AddAction(PUNState.ONJOIN_ROOM_FAILED, OnJoinRoomFailed);
    }

    private void OnDisable()
    {
        PUNCallback.Instance.RemoveAction(PUNState.ONCONNECTED_TO_MASTER, OnConnectedToMaster);
        PUNCallback.Instance.RemoveAction(PUNState.ONDISCONNECTED, OnDisconnected);
        PUNCallback.Instance.RemoveAction(PUNState.ONCREATE_JOIN_RANDOM_FAILED, OnRandomFailed);
        PUNCallback.Instance.RemoveAction(PUNState.ONCREATE_ROOM_FAILED, OnCreateFailed);
        PUNCallback.Instance.RemoveAction(PUNState.ONJOINED_ROOM, OnJoinedRoom);
        PUNCallback.Instance.RemoveAction(PUNState.ONLEFT_ROOM, OnLeftRoom);
        PUNCallback.Instance.RemoveAction(PUNState.ONJOIN_ROOM_FAILED, OnJoinRoomFailed);
    }

    private void Start()
    {
        if (PhotonNetwork.InLobby)
        {
            ChangeState(LobbyState.LOBBY);
        }
        else
        {
            ChangeState(LobbyState.OUT);

            if (PlayerPrefs.HasKey(Strings.KEY_USERID))
                IpfUserName.text = PlayerPrefs.GetString(Strings.KEY_USERID);
        }
    }

    public void AddAction(Action<LobbyState> action)
    {
        onChangeState += action;
    }
    
    public void RemoveAction(Action<LobbyState> action)
    {
        onChangeState -= action;
    }

    public void OnClickConnect()
    {
        ChangeState(LobbyState.BUSY);

        string userName = IpfUserName.text;
        PlayerPrefs.SetString(Strings.KEY_USERID, userName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.AuthValues = new AuthenticationValues(userName);
        bool reconnect = PhotonNetwork.ReconnectAndRejoin();
        if (reconnect == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public void OnClickRandom()
    {
        ChangeState(LobbyState.BUSY);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnClickCreate()
    {
        CreateRoom();
    }

    public void OnClickJoin()
    {
        string roomName = IpfRoomName.text;
        if (string.IsNullOrEmpty(roomName))
            return;
        
        ChangeState(LobbyState.BUSY);
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickExitLobby()
    {
        ChangeState(LobbyState.BUSY);
        PhotonNetwork.Disconnect();
    }
    
    public void OnClickPlayGame()
    {
        ChangeState(LobbyState.BUSY);
        StartCoroutine(CoPlayGame());
    }
    
    private IEnumerator CoPlayGame()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        yield return Wait.Second(1);

        if (playerCount == PhotonNetwork.CurrentRoom.PlayerCount)
            PhotonNetwork.LoadLevel(1);
    }

    public void OnClickExitGame()
    {
        ChangeState(LobbyState.BUSY);
        PhotonNetwork.LeaveRoom();
    }
    
    private void CreateRoom()
    {
        ChangeState(LobbyState.BUSY);
        
        string roomName = Random.Range(1000, 9999).ToString();
        PhotonNetwork.CreateRoom(roomName, new RoomOptions
        {
            PublishUserId = true, 
            MaxPlayers = 4,
        }, TypedLobby.Default);
    }

    private void ChangeState(LobbyState lobbyState)
    {
        onChangeState?.Invoke(lobbyState);
    }

    private void OnConnectedToMaster(PUNArgs obj)
    {
        if (PlayerPrefs.HasKey(Strings.ROOMNAME))
        {
            bool reconnect = PhotonNetwork.RejoinRoom(PlayerPrefs.GetString(Strings.ROOMNAME));
            if (reconnect == false)
            {
                PlayerPrefs.DeleteKey(Strings.ROOMNAME);
            }
        }
        ChangeState(LobbyState.LOBBY);
    }

    private void OnDisconnected(PUNArgs obj)
    {
        ChangeState(LobbyState.OUT);
    }
    
    private void OnJoinedRoom(PUNArgs obj)
    {
        ChangeState(LobbyState.ROOM);
    }
    
    private void OnLeftRoom(PUNArgs obj)
    {
        ChangeState(LobbyState.LOBBY);
    }

    private void OnJoinRoomFailed(PUNArgs obj)
    {
        ChangeState(LobbyState.LOBBY);
    }
    
    private void OnRandomFailed(PUNArgs arg)
    {
        CreateRoom();
    }

    private void OnCreateFailed(PUNArgs arg)
    {
        ChangeState(LobbyState.LOBBY);
    }
}
