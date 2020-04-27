using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleObject : ObjectClass {

    const float fMaxDistance = 100.0f;
    bool bRootObject = true;
    float fMoveRange = 0.0f;

    protected override void InitObject()
    {
        tMeshRenderer.material.color = RandomColor();
        vMoveDirection = RandomVector(100.0f);

        fMoveValocity = 500.0f;
        fRotateValocity = 50.0f;
    }

    public void StartPaticle(Vector3 pos)
    {
        if (tMeshRenderer == null)
            tMeshRenderer = GetComponent<MeshRenderer>();
        InitObject();
        bRootObject = false;
        transform.position = pos;
    }

    protected override void Update()
    {
        if (!bRootObject)
        {
            fMoveRange += Vector3.Distance(Vector3.zero, Move());
            if (fMoveRange >= fMaxDistance) Destroy(this.gameObject);
        }
    }
}
