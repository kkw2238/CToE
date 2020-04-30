using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { Move, Flick };

public class UIEffect : MonoBehaviour
{
    public delegate void ChainedFunction();

    public bool isRunningEffect = false, effectAtStartScene = false;
    public bool runChainObjectOnce = false;
    public float effectSpeed = 1.0f;
    public List<UIEffect> chainOtherEffectObjects;
    public Vector2 coroutineOption;

    ChainedFunction functions = null;

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

        if(runChainObjectOnce)
        {
            chainOtherEffectObjects.Clear();
        }
    }

    protected void StartChainedFunction()
    {
        if (functions != null)
        {
            functions();
            functions = null;
        }
    }

    public void SetCoroutineOption(Vector2 option)
    {
        coroutineOption = option;
    }

    virtual public void SetBeforeStartScene() { }
    virtual public void StartEffect() { }
    virtual public bool IsEndEffect() { return !isRunningEffect; }
    
    public void AddChainFunction(ChainedFunction chainedFunction)
    {
        functions += chainedFunction;
    }

}
