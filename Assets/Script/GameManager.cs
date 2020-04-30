using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType { MainScene, StageSelect, GameScene }
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
            else
            {
                var obj = FindObjectOfType<GameManager>();
                if(obj != null)
                {
                    manager = obj;
                }
                else
                {
                    var newGameManager = new GameObject("GameManager").AddComponent<GameManager>();

                    manager = newGameManager;
                }
            }

            return manager;
        }
    }
    public static State gameState
    {
        get { return gamestate; }
        set { gamestate = value; }
    }

    void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();
        if(obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(SceneType type)
    {
        SceneManager.LoadSceneAsync((int)type);
    }
}
