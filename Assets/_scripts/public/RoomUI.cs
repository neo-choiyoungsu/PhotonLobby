using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    public Transform TrfPlayers;
    public GameObject ObjPlayer;

    public virtual void UpdateRoom(PUNArgs punArgs = default)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (TrfPlayers.childCount <= i)
            {
                Instantiate(ObjPlayer, TrfPlayers);
            }
            TrfPlayers.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = PhotonNetwork.CurrentRoom.PlayerCount; i < TrfPlayers.childCount; i++)
        {
            TrfPlayers.GetChild(i).gameObject.SetActive(false);
        }

        int index = 0;
        foreach (KeyValuePair<int,Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            IPlayerUI playerUI = TrfPlayers.GetChild(index).GetComponent<IPlayerUI>();
            playerUI.SetInfo(pair.Value);
            index++;
        }
    }
}
