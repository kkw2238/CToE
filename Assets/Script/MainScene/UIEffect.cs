using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { Move, Flick };

public class UIEffect : MonoBehaviour
{
    public bool isRunningEffect = false;
    public bool effectAtStartScene = false;
    public float effectSpeed = 1.0f;
    public UIEffect[] chainOtherEffectObjects;
    public Vector2 coroutineOption;
   
    void Start()
    {
        SetBeforeStartScene();

        if (effectAtStartScene)
        {
            StartEffect();
        }
    }

    protected void StartChainOtherEffect()
    {
        foreach (UIEffect uiobject in chainOtherEffectObjects)
        {
            uiobject.StartEffect();
        }
    }
    virtual public void SetBeforeStartScene() { }
    virtual public void StartEffect() { }
    virtual public bool IsEndEffect() { return isRunningEffect; }
}
