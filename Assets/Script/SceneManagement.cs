using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public SceneType nowSceneType, nextSceneType;
    public UIFadeInOut uiFadeObject;
    public List<UIEffect> uiEffects;

    private void Start()
    {
        uiFadeObject.SetCoroutineOption(new Vector2(1.0f, 0.0f));
    }

    public void Update()
    {
        if(Input.anyKeyDown && uiFadeObject.IsEndEffect())
        {
            BeforeChangeScene();
            Debug.Log("1");
        }
    }

    private void BeforeChangeScene()
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
