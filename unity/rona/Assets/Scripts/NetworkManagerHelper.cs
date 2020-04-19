using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class NetworkManagerHelper : NetworkManager {

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
            AssignPlayer2Authorities();
        }
    }

    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
        onClientConnected.Invoke();
        Debug.Log("I connected");
    }

    public override void OnServerChangeScene(string newSceneName) {
        base.OnServerChangeScene(newSceneName);
    }


    public override void OnServerSceneChanged(string newSceneName) {
        base.OnServerSceneChanged(newSceneName);

        Debug.Log("I AM HERE IN THE NEW SCENE: " + newSceneName);

        if (newSceneName != "NetworkGameScene") {
            Debug.Log("Not doing anything");
            return;
        }

        // Assign authorities to clients.
        AssignPlayer1Authorities();

    }


    public void AssignPlayer1Authorities() {
        // Assign NetworkPlayer to player1.
        FindObjectOfType<PlayerStatus>().GetComponent<NetworkIdentity>().AssignClientAuthority(player1);
        // Player 1 should be able to have authority on the network events.
        var networkEvents = FindObjectOfType<NetworkEvents>();
        if (networkEvents) {
            networkEvents.GetComponent<NetworkIdentity>().AssignClientAuthority(player1);
        }


        // Camera control.

        //foreach (UIStatusBar go in FindObjectsOfType<UIStatusBar>()) {
        //    Debug.Log("Assigning authority");
        //    go.GetComponent<NetworkIdentity>().AssignClientAuthority(player1);
        //}

    }

    public void AssignPlayer2Authorities() {
        // Player 2 should be able to have authority on the network events.
        //var networkEvents = FindObjectOfType<NetworkEvents>();
        //if (networkEvents) {
        //    networkEvents.GetComponent<NetworkIdentity>().AssignClientAuthority(player2);
        //}

        //foreach (UIStatusBar go in FindObjectsOfType<UIStatusBar>()) {
        //    Debug.Log("Assigning authority");
        //    go.GetComponent<NetworkIdentity>().AssignClientAuthority(player2);
        //}

    }

    /*
    [Command]
    void CmdSetAuthority(NetworkIdentity identity, NetworkConnection connectionToClient) {
        identity.RemoveClientAuthority();
        identity.AssignClientAuthority(connectionToClient);
    }
    */

}

