using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool playingGame;
    public List<PlayerController> playerControllers;
    public int playersReady = 0;
    public PhotonView photonView;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        photonView = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
    }
    private void StartGame()
    {
        playingGame = true;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Ball", transform.position, transform.rotation);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    public void ResetAfterGoal()
    {
        PhotonNetwork.Instantiate("Ball", transform.position, transform.rotation);
    }
    public void FinishGame(string winnerTeam)
    {
        playingGame = false;
        Debug.Log($"Ganó el equipo {winnerTeam}!!!");
    }
    [PunRPC]
    public void RPC_PlayerReady()
    {
        playersReady++;
    }
    [PunRPC]
    public void RPC_StartGame()
    {
        StartGame();
    }
}
