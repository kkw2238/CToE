using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlickEffect : UIEffect
{
    public float startAlphaValue = 0.0f;
    public int maximumRepeatCount = int.MaxValue;
    public Color objectColor;

    public IEnumerator FlickEffect()
    {
        isRunningEffect = true;
        int repeatCount = 0, direction = 1;
        float elapsedTime = (startAlphaValue > 0.0f) ? ((coroutineOption.x + coroutineOption.y) / startAlphaValue) : 0.0f;

        while (repeatCount < maximumRepeatCount)
        {
            elapsedTime = Mathf.Max(Mathf.Min(1.0f, elapsedTime + Time.deltaTime * effectSpeed * direction), 0.0f);
            objectColor.a = Mathf.Lerp(coroutineOption.x, coroutineOption.y, elapsedTime);
            GetComponent<Text>().color = objectColor;

            if (elapsedTime == 1.0f || elapsedTime == 0.0f) {
                direction *= -1;
                ++repeatCount;
            }
            yield return null;
        }

        StartChainOtherEffect();
        StartChainedFunction();

        isRunningEffect = false;
        yield return null;
    }

    override public void SetBeforeStartScene()
    {
        objectColor = GetComponent<Text>().color;
        objectColor.a = startAlphaValue;
        GetComponent<Text>().color = objectColor;
    }

    override public void StartEffect()
    {
        StartCoroutine("FlickEffect");
    }
}
