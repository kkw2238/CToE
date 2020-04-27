using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleManager : MonoBehaviour {

    public PaticleObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPaticle(Vector3 position)
    {
        obj.SetVisible(true);
        for (int i = 0; i < 100; ++i)
        {
            PaticleObject paticle = Instantiate<PaticleObject>(obj);
            paticle.SetVisible(true);
            paticle.StartPaticle(position);
        }
        obj.SetVisible(false);
    }
}
