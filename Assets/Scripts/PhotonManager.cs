using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions opt = new RoomOptions
        {
            IsVisible = true,
            MaxPlayers = 4,
            PublishUserId = true
        };
        PhotonNetwork.JoinOrCreateRoom("Room", opt, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TeamsManager.Instance.PlayerJoined();
        GameObject player = PhotonNetwork.Instantiate("player", TeamsManager.Instance.playerSpawns[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();
        GameManager.Instance.playerControllers.Add(playerController);
    }
}
