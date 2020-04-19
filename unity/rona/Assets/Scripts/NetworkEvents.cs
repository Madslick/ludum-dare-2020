using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkEvents : NetworkBehaviour
{

    private NetworkManagerHelper networkManagerHelper;

    // Start is called before the first frame update
    void Start() {
        networkManagerHelper = FindObjectOfType<NetworkManagerHelper>();
    }

    [Command]
    public void CmdLoadScene(string sceneName) {
        networkManagerHelper.GetComponent<NetworkManager>().ServerChangeScene(sceneName);
    }


    /*
    [Command]
    void CmdSetAuthority(NetworkIdentity identity, NetworkConnection connectionToClient) {
        identity.RemoveClientAuthority();
        identity.AssignClientAuthority(connectionToClient);
    }
    */
}
