using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeType { FadeIn, FadeOut }

public class UIFadeInOut : UIEffect
{
    public FadeType fadeType = FadeType.FadeOut;
    private Color objectColor;
    public IEnumerator FadeEffect()
    {
        isRunningEffect = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < 1.0f)
        {
            elapsedTime = Mathf.Min(1.0f, elapsedTime + Time.deltaTime * effectSpeed);
            objectColor.a = Mathf.Lerp(coroutineOption.x, coroutineOption.y, elapsedTime);
            GetComponent<Image>().color = objectColor;

            yield return null;
        }

        StartChainOtherEffect();
        isRunningEffect = false;
        yield return null;
    }

    override public void SetBeforeStartScene()
    {
        objectColor = GetComponent<Image>().color;
        objectColor.a = fadeType == FadeType.FadeOut ? 0.0f : 1.0f;

        GetComponent<Image>().color = objectColor;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        coroutineOption = new Vector2(objectColor.a, 1.0f - objectColor.a);

        Debug.Log(new Vector2(Screen.width, Screen.height));
    }

    override public void StartEffect()
    {
        StartCoroutine("FadeEffect");
    }
}
