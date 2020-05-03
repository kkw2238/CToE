using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneManagement : SceneManagement
{
    static private StageSceneManagement stageSceneManager = null;

    public static StageSceneManagement instance
    {
        get { return stageSceneManager; }
    }

    private void Start()
    {
        if (stageSceneManager == null)
        {
            stageSceneManager = this;
        }

        if (uiFadeObject == null)
        {
            uiFadeObject = FindObjectOfType<UIFadeInOut>();
        }
        uiFadeObject.SetCoroutineOption(new Vector2(1.0f, 0.0f));

        MapEditor.SetMapData();
    }
}
