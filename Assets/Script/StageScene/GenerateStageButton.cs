using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStageButton : MonoBehaviour
{
    public List<StageButton> buttons = null;
    void Start()
    {
        if(buttons.Count == 0)
        {
            buttons.Add(FindObjectOfType<StageButton>());
        }
        Generate(MapEditor.StageCount);
    }

    void Generate(int count)
    {
        for(int i = 1; i <= count; ++i)
        {
            buttons.Add(Instantiate(buttons[0]));
            buttons[i].ResetTransform(transform);
            buttons[i].SetStageIndex(i + 1);
            buttons[i].transform.localPosition = new Vector2(i * 100, 100);
        }
    }
}
