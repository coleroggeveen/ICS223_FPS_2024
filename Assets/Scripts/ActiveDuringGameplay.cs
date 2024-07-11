using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDuringGameplay : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger.AddListener(GameEvent.GAME_INACTIVE, OnGameInactive);

    }

    private void OnGameActive()
    {
        this.enabled = true;
    }

    private void OnGameInactive()
    {
        this.enabled = false;
    }
}
