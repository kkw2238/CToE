using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManagement : MonoBehaviour {

    static public GameManagement instance;

    public int m_MaxObject;
    public int m_NowObject;
    public int m_JumpCount = 0;

    public int m_NowStage = 1;

    public List<int> m_Commands;
    public Player m_Player;
    public CreateMap m_Map;
    public PaticleManager m_PaticleManager;
    public UICommandScript m_LastClickObj;
    public Text m_Board;
    public Text m_ClearBoard;

    public bool m_GameClear = false;
    public bool m_PlayingCommand = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        m_Commands = new List<int>();
        m_MaxObject = 0;
        m_NowObject = 0;
    }
    // Use this for initialization
    void Start() {
        m_Board.enabled = false;
        m_ClearBoard.enabled = false;
    }

    public void SetObjcount(int n) { m_NowObject = m_MaxObject = n; }

    // Update is called once per frame
    void Update() {
        KeyboardInput();
    }

    public int NowStage() {
        return m_NowStage;
    } 

    public void KeyboardInput()
    {
      
        if (Input.GetKeyDown(KeyCode.Escape) && m_PlayingCommand) m_PlayingCommand = false;
        if (Input.GetKeyDown(KeyCode.Delete) && m_LastClickObj != null) Destroy(m_LastClickObj.gameObject);
        if (Input.GetKeyDown(KeyCode.Return) && m_GameClear) NextStage();

    }

    public void PlayCommand(List<int> commands)
    {
        m_Commands = commands;
        StartCoroutine("StartCommand");
    }

    public IEnumerator StartCommand()
    {
        if (!m_GameClear)
        {
            m_PlayingCommand = true;
            int multiforLoop = 0; 
            int repeat = 0;
            List<int> forCommandList = new List<int>();

            while (m_Commands.Count != 0 && m_PlayingCommand)
            {   
                // 플레이어가 이동 가능 상태일 경우 동작
                if (m_Player.IsEnableMove())
                {
                    // 클리어 조건
                    if (m_NowObject == 0)
                    {
                        m_Board.enabled = true;
                        m_ClearBoard.enabled = true;
                        m_GameClear = true;
                        m_PlayingCommand = false;
                    }

                    int i = pop(m_Commands);

                    // Loop 커맨드의 경우 
                    if (i > UICommandScript.COMMAND_LS)
                    {
                        multiforLoop = 0;
                        forCommandList.Clear();

                        // Loop와 end 짝을 탐색하는 코드
                        for (int j = 0; j < m_Commands.Count;)
                        {
                            // Loop start를 한번 더 만날 경우 multiForLoop 카운터 1증가
                            if (m_Commands[j] > UICommandScript.COMMAND_LS)
                                multiforLoop++;
                            // End를 만날 경우 multiForLoop 카운터 1감소
                            if (m_Commands[j] == UICommandScript.COMMAND_LE)
                                multiforLoop--;
                            
                            forCommandList.Add(pop(m_Commands));

                            // Loop Start와 end의 짝이 맞다면 multiForLoop 카운터는 -1
                            if (multiforLoop < 0) break;
                        }
                        repeat = i - UICommandScript.COMMAND_LS;

                        List<int> tmplist = new List<int>();

                        // Loop속 숫자 만큼 반복하여 
                        // 커맨드 오브젝트를 임시 커맨드 리스트에 삽입
                        for (int j = 0; j < repeat; ++j)
                            tmplist.AddRange(forCommandList);

                        // 임시 리스트에 남아있는 커맨드 리스트를 임시 커맨드 리스트에 삽입
                        tmplist.AddRange(m_Commands);
                        // 삽입 후 덮어쓰기
                        m_Commands = tmplist;
                    }

                    else
                        PlayCommand(i);

                }
                else
                    yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.0f);

            if (m_NowObject != 0)
            {
                m_JumpCount = 0;
                ResetObj(false);
            }
            else
            {
                m_Board.enabled = true;
                m_ClearBoard.enabled = true;
                m_GameClear = true;
            }
            m_PlayingCommand = false;
            yield return null;
        }

        if(m_GameClear == true)
        {

        }
    }

    public void NextStage()
    {
        ++m_NowStage;
        if (m_NowStage > MapEditor.MAX_STAGE)
        {
            m_ClearBoard.enabled = true;
            m_ClearBoard.text = "Thanks for playing";
            m_Board.enabled = true;
            m_JumpCount = 0;

            new WaitForSeconds(3.0f);

            Application.Quit();
        }

        else
        {
            CommandManagement.instance.ResetCommandObjects();


            ResetObj(false);

            m_GameClear = false;
        }
    }

    public void ResetObj(bool clickButton)
    {
        m_PlayingCommand = false;
        m_Board.enabled = false;
        m_ClearBoard.enabled = false;
        m_NowObject = m_MaxObject;
 
        m_Map.Create();
        m_Player.ResetObj();
        m_JumpCount = 0;

        if (clickButton)
            CommandManagement.instance.ResetCommandObjects();
    }

    public void PlayCommand(int command)
    {
        if (command == UICommandScript.COMMAND_GO)
        {
            m_JumpCount++;
            m_Player.Go();
        }
        else if (command == UICommandScript.COMMAND_TL)
            m_Player.TurnLeft();
        else if (command == UICommandScript.COMMAND_TR)
            m_Player.TurnRight();
    }
    public void SetPaticle(Vector3 position) { m_PaticleManager.SetPaticle(position); }
    public int pop(List<int> lists) {

        int i = lists[0];
        lists.RemoveAt(0);
        return i;
    }
    static GameManagement Instance() { return instance; }

}
