using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    const int STATE_NONE = 0;
    const int STATE_MOVE = 1;
    const int STATE_TURN = 2;
    const int STATE_READY = 3;

    public CreateMap m_Mapaditor;

    public Vector3 m_PrePosition;
    public Vector3 m_DirectionPos;

    public Animator m_PlayerAnimator;
    public float m_MoveDistance;
    public float m_WaitTime;
    public int m_State = STATE_READY;
    public int[] m_Map;

	// Use this for initialization
	void Start () {
        m_WaitTime = 0.0f;
        transform.position = m_Mapaditor.m_Sponposition;
        m_MoveDistance = 0;
        m_DirectionPos = m_PrePosition = transform.position;
    }

    public void SetMapdata(int[] mapdata)
    {
        m_Map = mapdata;
    }

    public void ResetObj()
    {
        m_State = STATE_READY;
        transform.position = m_Mapaditor.m_Sponposition;
        m_MoveDistance = 0;
        m_DirectionPos = m_PrePosition = transform.position;
        m_PlayerAnimator.SetBool("Jump", false);
    }
    // Update is called once per frame
    void Update () {
        if (m_State == STATE_MOVE)
        {
            m_MoveDistance = Mathf.Min(m_MoveDistance + Time.deltaTime * 2.0f, 1.0f);
            transform.position = Vector3.Lerp(m_PrePosition, m_DirectionPos, m_MoveDistance);
        }
        else if(m_State == STATE_TURN)
        {
            m_MoveDistance = Mathf.Min(m_MoveDistance + Time.deltaTime * 2.0f, 1.0f);
            transform.forward = Vector3.Slerp(m_PrePosition, m_DirectionPos, m_MoveDistance);
            if (transform.forward == m_DirectionPos)
            {
                m_State = STATE_NONE;
                m_PlayerAnimator.SetBool("Jump", false);
            }
        }
        if(m_PlayerAnimator.GetBool("Jump"))
        {
            m_WaitTime += Time.deltaTime;
            if (m_WaitTime >= 3.0f)
                GameManagement.instance.ResetObj(false);
        }
        
        GetComponent<Transform>().position = new Vector3(transform.position.x, Mathf.Max(0.5f, transform.position.y), transform.position.z);
    }

    public bool IsEnableMove() { return m_State == STATE_NONE; }

    public void Go()
    {
        m_PlayerAnimator.SetBool("Jump", true);
        m_PrePosition = transform.position;
        m_DirectionPos = transform.position + (transform.forward.normalized) * MapEditor.MAP_OFFSET;
        m_MoveDistance = 0.0f;
        m_State = STATE_MOVE;
        m_WaitTime = 0.0f;
    }

    public void TurnLeft()
    {
        m_PrePosition = transform.forward;
        m_DirectionPos = -transform.right;
        m_MoveDistance = 0.0f;
        m_State = STATE_TURN;
        m_WaitTime = 0.0f;
    }

    public void TurnRight()
    {
        m_PrePosition = transform.forward;
        m_DirectionPos = transform.right;
        m_MoveDistance = 0.0f;
        m_State = STATE_TURN;
        m_WaitTime = 0.0f;
    }

    public void EnableMove()
    {
        if (m_State == STATE_TURN) return;
        m_State = STATE_NONE;
        m_DirectionPos = m_PrePosition;
        m_PlayerAnimator.SetBool("Jump", false);
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MapObj")
        {
            Debug.Log("Collision!");
            
        }
    }
}
