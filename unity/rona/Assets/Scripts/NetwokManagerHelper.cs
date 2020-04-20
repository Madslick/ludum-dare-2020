using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class NetwokManagerHelper : NetworkManager {

    NetworkConnection player1 = null;
    NetworkConnection player2 = null;

    [Serializable]
    public class ClientConnectedEvent : UnityEvent { }
    public ClientConnectedEvent onClientConnected;

    public override void OnServerConnect(NetworkConnection conn) {
        Debug.Log("A client connected");

        if (player1 == null) {
            player1 = conn;
        } else {
            player2 = conn;
        }
    }

    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
        onClientConnected.Invoke();
        Debug.Log("I connected");
    }


    public override void OnServerSceneChanged(string sceneName) {
        base.OnServerSceneChanged(sceneName);

        Debug.Log("I AM HERE IN THE NEW SCENE: " + sceneName);
        Debug.Log(player1);
        Debug.Log(player1.clientOwnedObjects);

        Debug.Log("About to assign authority");
        //CmdSetAuthority(FindObjectOfType<PlayerStatus>().gameObject.GetComponent<NetworkIdentity>(), player1);
        FindObjectOfType<Status>().gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(player1);

        Debug.Log(player1.clientOwnedObjects);
    }

    /*
    [Command]
    void CmdSetAuthority(NetworkIdentity identity, NetworkConnection connectionToClient) {
        identity.RemoveClientAuthority();
        identity.AssignClientAuthority(connectionToClient);
    }
    */

}

