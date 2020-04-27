using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClass : MonoBehaviour {

    public enum DIRECTION { UP = 0x01, DOWN = 0x02, LEFT = 0x04, RIGHT = 0x08, FORWARD = 0x10, BACK = 0x20 };

    protected float fMoveValocity, fRotateValocity;
    protected Transform tObjectTransform;
    protected MeshRenderer tMeshRenderer;
    protected Vector3 vMoveDirection;

    protected virtual void Start()
    {
        tObjectTransform = GetComponent<Transform>();
        tMeshRenderer = GetComponent<MeshRenderer>();

        InitObject();
    }

    protected virtual void Update() {
        Move();
    }

    protected virtual void InitObject()
    {
        fMoveValocity = 5.0f;
        fRotateValocity = 5.0f;
    }

    public virtual Vector3 Move()
    {
        Vector3 movePos = (vMoveDirection.normalized * fMoveValocity * Time.deltaTime);

        tObjectTransform.position += movePos;
        return movePos;
    }

    public virtual Vector3 BackMove()
    {
        Vector3 movePos = (-vMoveDirection.normalized * fMoveValocity * Time.deltaTime);

        tObjectTransform.position += movePos;
        return movePos;
    }

    public virtual void SetPosition(Vector3 pos)
    {
        tObjectTransform.position = pos;
    }

    public virtual void SetVisible(bool visible)
    {
        if (tMeshRenderer == null)
            tMeshRenderer = GetComponent<MeshRenderer>();
        tMeshRenderer.enabled = visible;
    }

    public virtual Vector3 RandomVector(float range)
    {
        return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
    }

    public virtual Color RandomColor()
    {
        Color color = Color.white;
        color.r = Random.Range(0.0f, 255.0f) / 255.0f;
        color.g = Random.Range(0.0f, 255.0f) / 255.0f;
        color.b = Random.Range(0.0f, 255.0f) / 255.0f;

        return color;
    }
}
