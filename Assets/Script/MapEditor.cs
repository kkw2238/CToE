using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

static public class MapEditor {

    static bool initialized = false;
    static public float MAP_OFFSET = 1.2f;
    static public int MAX_STAGE = -1;
    static Dictionary<int, string[]> m_mapdata;

    static public int StageCount
    {
        get {
            if(!initialized)
            {
                SetMapData();
            }
            return MAX_STAGE;
        }
    }

    static public void SetMapData()
    {
        if (initialized)
        {
            return;
        }
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".\\Assets\\MapData\\");
        System.IO.FileInfo[] fis = di.GetFiles("*.txt");

        MapEditor.MAX_STAGE = fis.Length;

        if (m_mapdata == null)
            m_mapdata = new Dictionary<int, string[]>();
        else
            m_mapdata.Clear();

        string line;
       
        for (int i = 0; i < MAX_STAGE; ++i)
        {
            System.IO.StreamReader file =
            new System.IO.StreamReader(".\\Assets\\MapData\\Stage" + (i + 1).ToString() + ".txt" );
            
            List<string> tmpDatas = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                tmpDatas.Add(line);
            }

            file.Close();

            if(tmpDatas.Count != 0)
                m_mapdata[i + 1] = tmpDatas.ToArray();
        }

        initialized = true;
    }

    static public string[] GetMapdata(int n)
    {
        if (m_mapdata[n] != null)
            return m_mapdata[n];
        else {
            return null;
        }
    }
}
