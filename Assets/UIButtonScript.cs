using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonScript : MonoBehaviour
{
    public enum buttonType{ UI_START_BUTTON, UI_RESET_BUTTON, UI_QUIT_BUTTON };

    public buttonType m_MyType;

    public void OnButtonClick()
    {
        if (GameManagement.instance == null) return;

        Debug.Log(m_MyType);

        switch (m_MyType)
        {
            case buttonType.UI_START_BUTTON:
                if (!GameManagement.instance.m_PlayingCommand)
                {
                    GameManagement.instance.PlayCommand(CommandManagement.instance.CreateCommandList());
                }
                break;
            case buttonType.UI_RESET_BUTTON:
                GameManagement.instance.ResetObj(true);
                break;
            case buttonType.UI_QUIT_BUTTON:
                Application.Quit();
                break;
        }
    }
}

