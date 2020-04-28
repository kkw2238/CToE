using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Normal,Login, Stage, Game, Menu }
public class GameManager : MonoBehaviour
{
    static private GameManager manager = null;
    static private State gamestate = State.Normal;

    public static GameManager instance
    {
        get {
            if (manager != null)
            {
                return manager;
            }

            manager = new GameManager();
            return manager;
        }
    }
    public static State gameState
    {
        get { return gamestate; }
        set { gamestate = value; }
    }

    void Start()
    {
        if(manager == null)
        {
            manager = new GameManager();
        }
    }
}
