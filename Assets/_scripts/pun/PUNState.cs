using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public enum PUNState
{
    ONCONNECTED,
    ONLEFT_ROOM,
    ONCREATE_ROOM_FAILED,
    ONJOIN_ROOM_FAILED,
    ONCREATED_ROOM,
    ONJOINED_LOBBY,
    ONLEFT_LOBBY,
    ONDISCONNECTED,
    ONJOINED_ROOM,
    ONPLAYER_ENTERED_ROOM,
    ONPLAYER_LEFT_ROOM,
    ONCREATE_JOIN_RANDOM_FAILED,
    ONCONNECTED_TO_MASTER,
    ONMASTER_CLIENT_SWITCHED
}

public struct PUNArgs
{
    public short ReturnCode { get; set; }
    public string Message { get; set; }
    public Player Player { get; set; }
    public DisconnectCause DisconnectCause { get; set; }
}
