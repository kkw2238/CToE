﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveEffect : UIEffect
{
    IEnumerator MoveEffect()
    {
        float elapsedTime = 0.0f;
        Vector2 startPos = transform.localPosition;

        while(elapsedTime < 1.0f)
        {
            elapsedTime = Mathf.Min(1.0f, elapsedTime + Time.deltaTime * effectSpeed);
            transform.localPosition = Vector2.Lerp(startPos, coroutineOption, elapsedTime);

            Debug.Log(elapsedTime);
            yield return null;
        }

        StartChainOtherEffect();
        yield return null;
    }

    override public void StartEffect()
    {
        StartCoroutine("MoveEffect");
    }
}