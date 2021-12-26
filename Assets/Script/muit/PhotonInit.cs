using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class PhotonInit : MonoBehaviourPunCallbacks
{
	public enum ActivePanel
	{
		LOGIN = 0,
		ROOMS = 1
	}
	public ActivePanel activePanel = ActivePanel.LOGIN;

	private string gameVersion = "0.1";
	public string uesrId = "Kim";
	public byte maxPlayer = 20;

	public TMP_InputField txtUserId;
	public TMP_InputField txtRoomName;

	public GameObject[] panels;

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}
	private void Start()
	{
		//PhotonNetwork.GameVersion = this.gameVersion;
		//PhotonNetwork.NickName = uesrId;

		//PhotonNetwork.ConnectUsingSettings();
		txtUserId.text = PlayerPrefs.GetString("USER_ID", "USER_" + Random.Range(1, 999));
		txtRoomName.text = PlayerPrefs.GetString("ROOM_NAME", "ROOM_" + Random.Range(1, 999));
	}
	#region SELF_CALLBACK_FUNCTIONS
	public void OnLogin()
	{
		PhotonNetwork.GameVersion = this.gameVersion;
		PhotonNetwork.NickName = txtUserId.text;

		PhotonNetwork.ConnectUsingSettings();

		PlayerPrefs.SetString("USER_ID", PhotonNetwork.NickName);
		ChangePanel(ActivePanel.ROOMS);

	}

	public void OnCreateRoomClick()
	{
		PhotonNetwork.CreateRoom(txtRoomName.text
								, new RoomOptions { MaxPlayers = this.maxPlayer });

	}

	public void OnJoinRandomRoomClick()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	#endregion
	private void ChangePanel(ActivePanel panel)
	{
		foreach (GameObject _panel in panels)
		{
			Debug.Log(panels);
			_panel.SetActive(false);
		}
		panels[(int)panel].SetActive(true);
	}
	#region PHOTON_CALLBACK_FUNCTIONS
	public override void OnConnectedToMaster()
	{
		Debug.Log("Connect To Master");
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Failed join room");
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayer });
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined Room !!!");
		// photonNetwork�� ������ ����� ��� ���� �����ش�. 
		// gamemanager���� creatTank�ϰ� ���� �ٽ� �����Ų��
		PhotonNetwork.IsMessageQueueRunning = false;
		SceneManager.LoadScene("Level01");
	}
	#endregion
}
