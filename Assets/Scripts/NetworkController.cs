using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController networkController;

    

    public GameObject PlayButton;
    public GameObject Cancel;

    private void Awake()
    {
        networkController = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    #region Matchmaking
    public override void OnConnectedToMaster()
    {
        print("Connected to master");

        PlayButton.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        PlayButton.SetActive(false);
        Cancel.SetActive(true);
        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join Random Failed. ");

        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        print("In a room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Create room failed");
        CreateRoom();
    }

    public void OnCancelClick()
    {
        Cancel.SetActive(false);
        PlayButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region Scene Changes

    #endregion

    #region RPCs
    // We define a function an RPC function to be called by  other clients
    [PunRPC]
    public void RPC_Test()
    {

    }

    // Connect to UI button to test
    public void RPC_Test_Inst()
    {
        // call rpc function on all clients
        photonView.RPC("RPC_Test", RpcTarget.All);
    }

    #endregion
}
