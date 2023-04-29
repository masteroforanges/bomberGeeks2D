using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Security.Cryptography;
using UnityEngine.Events;

public class MyNetworkManager : NetworkManager
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public List<Transform> coinSpawnPoints;
    public int maxCoinsInGame = 2;
    public static int spawnedCoins = 0;

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

    public UnityEvent OnPlayerConnect;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        OnPlayerConnect.Invoke();
        Transform startPoint;
        Color color;

        if (numPlayers == 0)
        {
            startPoint = spawnPoint1;
            color = Color.red;
            InvokeRepeating("SpawnCoin", 2, 2);
        }
        else
        {
            startPoint = spawnPoint2;
            //InvokeRepeating("SpawnCoin", 2, 2);
            color = Color.green;
        }

        GameObject new_player = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);

        new_player.GetComponent<Player>().playerColor = color;

        NetworkServer.AddPlayerForConnection(conn, new_player);

    }

    public void SpawnCoin()
    {
        if (spawnedCoins < maxCoinsInGame)
        {
            Vector3 local = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)].position;

            GameObject new_coin = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Coin"), local, transform.rotation);

            NetworkServer.Spawn(new_coin);
            spawnedCoins++;
        }
    }
}
