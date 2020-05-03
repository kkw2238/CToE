using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public int myStageIndex;

    public void SetStageIndex(int index)
    {
        myStageIndex = index;
        GetComponentInChildren<Text>().text = myStageIndex.ToString();
    }
    
    public void ResetTransform(Transform parent)
    {
        transform.SetParent(parent);
        transform.SetAsFirstSibling();
        transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); 
    }
}
