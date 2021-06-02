using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = System.Collections.Hashtable;

public class PUNCallback : MonoBehaviourPunCallbacks
{
    public static PUNCallback Instance
    {
        get
        {
            if (instance == null && Destroyed == false)
            {
                GameObject obj = new GameObject(Strings.PUN_CALLBACK); 
                instance = obj.AddComponent<PUNCallback>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    public static bool Destroyed;
    
    private static PUNCallback instance;

    private Dictionary<PUNState, Action<PUNArgs>> actionDict = new Dictionary<PUNState, Action<PUNArgs>>()
    {
        [PUNState.ONCONNECTED] = null,
        [PUNState.ONLEFT_ROOM] = null,
        [PUNState.ONCREATE_ROOM_FAILED] = null,
        [PUNState.ONJOIN_ROOM_FAILED] = null,
        [PUNState.ONCREATED_ROOM] = null,
        [PUNState.ONJOINED_LOBBY] = null,
        [PUNState.ONLEFT_LOBBY] = null,
        [PUNState.ONDISCONNECTED] = null,
        [PUNState.ONJOINED_ROOM] = null,
        [PUNState.ONPLAYER_ENTERED_ROOM] = null,
        [PUNState.ONPLAYER_LEFT_ROOM] = null,
        [PUNState.ONCREATE_JOIN_RANDOM_FAILED] = null,
        [PUNState.ONCONNECTED_TO_MASTER] = null,
        [PUNState.ONMASTER_CLIENT_SWITCHED] = null
    }; 
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            Destroyed = true;
        }
    }

    public void AddAction(PUNState punState, Action<PUNArgs> action)
    {
        actionDict[punState] += action;
    }
    public void RemoveAction(PUNState punState, Action<PUNArgs> action)
    {
        actionDict[punState] -= action;
    }
    
    public override void OnConnected()
    {
        actionDict[PUNState.ONCONNECTED]?.Invoke(default);
    }
    
    public override void OnLeftRoom()
    {
        PlayerPrefs.DeleteKey(Strings.ROOMNAME);
        actionDict[PUNState.ONLEFT_ROOM]?.Invoke(default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        actionDict[PUNState.ONCREATE_ROOM_FAILED]?.Invoke(new PUNArgs
        {
            ReturnCode = returnCode,
            Message = message
        });
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        actionDict[PUNState.ONJOIN_ROOM_FAILED]?.Invoke(new PUNArgs
        {
            ReturnCode = returnCode,
            Message = message
        });
    }

    public override void OnCreatedRoom()
    {
        actionDict[PUNState.ONCREATED_ROOM]?.Invoke(default);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        actionDict[PUNState.ONMASTER_CLIENT_SWITCHED]?.Invoke(new PUNArgs
        {
            Player = newMasterClient
        });
    }
    
    public override void OnJoinedLobby()
    {
        actionDict[PUNState.ONJOINED_LOBBY]?.Invoke(default);
    }

    public override void OnLeftLobby()
    {
        actionDict[PUNState.ONLEFT_LOBBY]?.Invoke(default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        actionDict[PUNState.ONDISCONNECTED]?.Invoke(new PUNArgs
        {
            DisconnectCause = cause
        });
    }

    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetString(Strings.ROOMNAME, PhotonNetwork.CurrentRoom.Name);
        actionDict[PUNState.ONJOINED_ROOM]?.Invoke(default);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        actionDict[PUNState.ONPLAYER_ENTERED_ROOM]?.Invoke(new PUNArgs
        {
            Player = newPlayer
        });
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        actionDict[PUNState.ONPLAYER_LEFT_ROOM]?.Invoke(new PUNArgs
        {
            Player = otherPlayer
        });
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        actionDict[PUNState.ONCREATE_JOIN_RANDOM_FAILED]?.Invoke(new PUNArgs
        {
            ReturnCode = returnCode,
            Message = message
        });
    }

    public override void OnConnectedToMaster()
    {
        actionDict[PUNState.ONCONNECTED_TO_MASTER]?.Invoke(default);
    }

    //public override void OnRegionListReceived(RegionHandler regionHandler)
    //{
    //}

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //}

    //public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    //{
    //}

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    //{
    //}

    //public override void OnFriendListUpdate(List<FriendInfo> friendList)
    //{
    //}

    //public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    //{
    //}

    //public override void OnCustomAuthenticationFailed (string debugMessage)
    //{
    //}

    //public override void OnWebRpcResponse(OperationResponse response)
    //{
    //}

    //public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    //{
    //}

    //public override void OnErrorInfo(ErrorInfo errorInfo)
    //{
    //}
}
