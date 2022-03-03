using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string titleMenuName = "Title";
    [SerializeField] private string loadingMenuName = "Loading";
    [SerializeField] private string roomMenuName = "Room";
    [SerializeField] private string errorMenuName = "Error";
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private TMP_Text errorText;

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

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text)) return;

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        Debug.Log("Creating room " + roomNameInputField.text);
        MenuManager.Instance.OpenMenu(loadingMenuName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.Instance.OpenMenu(roomMenuName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed " + message;
        MenuManager.Instance.OpenMenu(errorMenuName);
    }

    public void LeaveRoom()
    {
        Debug.Log("Leaving room " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu(loadingMenuName);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu(titleMenuName);
    }
}
