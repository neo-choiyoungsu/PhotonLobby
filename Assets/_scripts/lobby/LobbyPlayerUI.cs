using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerUI : MonoBehaviour, IPlayerUI
{
    public Color ColorMe;
    public Color ColorOthers;
    
    public Text TxtName;
    public void SetInfo(Player player)
    {
        TxtName.text = player.UserId;
        TxtName.color = player.IsLocal ? ColorMe : ColorOthers;
    }
}
