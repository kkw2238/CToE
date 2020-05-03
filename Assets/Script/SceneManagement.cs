using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public SceneType nowSceneType, nextSceneType;
    public UIFadeInOut uiFadeObject;
    public List<UIEffect> uiEffects;

    protected void BeforeChangeScene()
    {
        uiFadeObject.AddChainFunction(ChangeScene);
        uiFadeObject.SetCoroutineOption(new Vector2(0.0f, 1.0f));
        uiFadeObject.StartEffect();
    }

    public void ChangeScene()
    {
        GameManager.instance.ChangeScene(nextSceneType);
    }
}
