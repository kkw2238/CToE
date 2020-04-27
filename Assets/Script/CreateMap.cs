using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    private const int MAX_TYPE = 2;
    private const char MAP_VOID = '0', MAP_START_POSITION = '2';

    public GameObject[] originObj;
    public Voxel[] originVoxel;
    public SphereObject originSphere;
    public int m_CurObject;
    public Vector3 m_Sponposition;

    public List<Voxel> m_voxels;
    public List<SphereObject> m_spheres;

    // Use this for initialization
    void Awake () {
        m_CurObject = 0;
        m_voxels = new List<Voxel>();
        m_spheres = new List<SphereObject>();

        MapEditor.SetMapData();
    }

    private void Start()
    {
        Create();
    }

    public void ResetObj()
    {
        foreach (Voxel v in m_voxels)   v.ResetObj();
        foreach (SphereObject s in m_spheres) s.ResetObj();
    }


    public void Create()
    {
        m_CurObject = 0;
        if (m_voxels.Count > 0)
        {
            foreach (Voxel v in m_voxels) Destroy(v.gameObject);
            m_voxels.Clear();
        }
        if (m_spheres.Count > 0)
        {
            foreach (SphereObject s in m_spheres) Destroy(s.gameObject);
            m_spheres.Clear();
        }

        string[] mapdata = MapEditor.GetMapdata(GameManagement.instance.NowStage());

        for(int i = 0; i < mapdata.Length; ++i)
        {
            for(int j = 0; j < mapdata[i].Length; ++j)
            {
                if (mapdata[i][j] != MAP_VOID)
                {
                    CreateVoxel(i, j);
                    CreateSphere(i, j);
                    m_CurObject++;
                    if (mapdata[i][j] == MAP_START_POSITION)
                        m_Sponposition = new Vector3(MapEditor.MAP_OFFSET * j, 2.0f, MapEditor.MAP_OFFSET * i);
                }
            }
        }

        GameManagement.instance.SetObjcount(m_CurObject);
    }

    void CreateVoxel(int i, int j)
    {
        originVoxel[(i + j) % MAX_TYPE].Init(i + j);

        var newObj = Instantiate(originObj[(i + j) % MAX_TYPE]);

        newObj.transform.position = new Vector3(MapEditor.MAP_OFFSET * j, 0.0f, MapEditor.MAP_OFFSET * i);
        m_voxels.Add(newObj.transform.GetComponentInChildren<Voxel>());
    }

    void CreateSphere(int i, int j)
    {
        var newVoxel = Instantiate(originSphere);
        newVoxel.transform.position = new Vector3(MapEditor.MAP_OFFSET * j, 0.0f, MapEditor.MAP_OFFSET * i);
        m_spheres.Add(newVoxel);
    }

    Color MakeColor(float r, float g, float b, float a)
    {
        Color newColor = Color.white;
        newColor.r = r;
        newColor.g = g;
        newColor.b = b;
        newColor.a = a;

        return newColor;
    }
}
