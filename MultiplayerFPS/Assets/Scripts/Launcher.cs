using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string titleMenuName = "Title";
    [SerializeField] private string loadingMenuName = "Loading";

    void Start()
    {
        MenuManager.Instance.OpenMenu(loadingMenuName);
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Master...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        Debug.Log("Joining Lobby...");
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu(titleMenuName);
        Debug.Log("Joined Lobby");
    }

    void Update()
    {
        
    }
}
