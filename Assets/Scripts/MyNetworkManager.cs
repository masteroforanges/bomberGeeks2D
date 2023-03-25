using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Security.Cryptography;

public class MyNetworkManager : NetworkManager
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public override void OnStartServer() 
    {
        Debug.Log("Seja bem vindo!");

    }
    public override void OnStopServer()
    {
        base.OnStopServer();

        Debug.Log("Encerrando o Server...");
    }
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Novo jogador conectado!");
    }
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        Debug.Log("Um jogador saiu da partida...");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPoint;

        if(numPlayers == 0)
        {
            startPoint = spawnPoint1;
        }
        else
        {
            startPoint = spawnPoint2;
        }

        GameObject new_player = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, new_player);

    }

}
