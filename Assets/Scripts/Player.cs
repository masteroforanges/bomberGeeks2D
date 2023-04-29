using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

[Serializable]
public class IntEvent : UnityEvent<int> {}

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;

    [SyncVar]
    public int Coins;

    [SyncVar]
    public Color playerColor;

    //Events
    public IntEvent OnCoinCollect;

    public int coins;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = playerColor;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddPlayerListener(this);
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(inputX, inputY) * speed;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pedindo uma mensagem para o Server!");
                TalkToServer();
            }
        }
    }


    [Command]
    void TalkToServer()
    {
        Debug.Log("Player pediu uma pizza!");
    }

    [Server]
    public void AddCoins()
    {
        coins += 1;
        OnCoinCollect.Invoke(coins);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            AddCoins();
            MyNetworkManager.spawnedCoins--;
            Destroy(collision.gameObject);
        }
    }

}
