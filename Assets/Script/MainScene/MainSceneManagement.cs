using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManagement : SceneManagement
{
    static private MainSceneManagement mainSceneManager = null;

    public static MainSceneManagement instance
    {
        get { return mainSceneManager; }
    }

    protected void Start()
    {
        if (mainSceneManager == null)
        {
            mainSceneManager = this;
        }
        uiFadeObject.SetCoroutineOption(new Vector2(1.0f, 0.0f));
    }

    protected void Update()
    {
        if (Input.anyKeyDown && uiFadeObject.IsEndEffect())
        {
            BeforeChangeScene();
        }
    }
}
