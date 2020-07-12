﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Custom NetworkManager that simply assigns the correct racket positions when
// spawning players. The built in RoundRobin spawn method wouldn't work after
// someone reconnects (both players would be on the same side).
[AddComponentMenu("")]
public class NetworkManagerGamma : NetworkManager
{
    public GameObject ChatPrefab;
    public Transform leftSpawn;
    public Transform rightSpawn;
    //GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftSpawn : rightSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball if two players
        //if (numPlayers == 2)
        //{
            //ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            //NetworkServer.Spawn(ball);
        //}
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        //if (ball != null)
        //    NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
