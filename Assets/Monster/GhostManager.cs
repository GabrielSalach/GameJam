using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [SerializeField] List<MonsterAI> ghosts;
    [SerializeField] GameObject player;

    void Start() {
        foreach(MonsterAI ghost in ghosts) {
            AddGhost(ghost);
        }
    }

    public void AddGhost(MonsterAI ghost) {
        ghost.player = player.transform;
        ghost.onPlayerAquired.AddListener(delegate {player.GetComponent<Fear>().StartChasing();});
        ghost.onPlayerLost.AddListener(delegate {player.GetComponent<Fear>().QuitChasing();});
    }
}
