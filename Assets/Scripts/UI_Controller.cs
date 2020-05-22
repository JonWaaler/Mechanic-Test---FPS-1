using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public NetworkController networkController;

    public Text ping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ping.text = PhotonNetwork.GetPing().ToString();
    }

    public void CreateOrJoinLobby()
    {
    }

    public void Quit()
    {
        Application.Quit();
    }
}
