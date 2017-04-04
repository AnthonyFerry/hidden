using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwissArmyKnife;

public class GameController : Singleton<GameController> {

    public GameState State = GameState.running;

}

public enum GameState
{
    running,
    keyboard,
    paused
}