using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
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
}
