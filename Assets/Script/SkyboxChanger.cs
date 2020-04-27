using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextureArray
{
    public Texture[] m_textures = new Texture[6];
}

public class SkyboxChanger : MonoBehaviour
{
    const int COUNT_OF_SKYBOX = 5;
    public TextureArray[ ] Skyboxes;
   
    public float m_ElpasedTime = 0.0f;
    public int m_NowSkyboxIndex = 0;
    public Material m_SkyboxMat;
    public const float CHANGE_TIMER = 5.0f;

    public float scrollSpeed = 0.5F;
    readonly string[] ShaderResourceNames1 = new string[] { "_FrontTex", "_BackTex", "_LeftTex", "_RightTex", "_UpTex", "_DownTex" };

    public void Awake()
    {
    }

    public void Start()
    {
        for (int i = 0; i < 6; ++i)
        {
            m_SkyboxMat.SetTexture(ShaderResourceNames1[i], Skyboxes[m_NowSkyboxIndex].m_textures[i]);
            m_SkyboxMat.SetTexture(ShaderResourceNames1[i] + "2", Skyboxes[(m_NowSkyboxIndex + 1) % COUNT_OF_SKYBOX].m_textures[i]);
        }
    }

    public void Update()
    {
        float offset = Time.time * scrollSpeed;
       
        m_ElpasedTime += Time.deltaTime;

        if (m_ElpasedTime > CHANGE_TIMER)
        {
            m_NowSkyboxIndex = (m_NowSkyboxIndex + 1) % COUNT_OF_SKYBOX;

            for (int i = 0; i < 6; ++i)
            {
                m_SkyboxMat.SetTexture(ShaderResourceNames1[i], Skyboxes[m_NowSkyboxIndex].m_textures[i]);
                m_SkyboxMat.SetTexture(ShaderResourceNames1[i] + "2", Skyboxes[(m_NowSkyboxIndex + 1) % COUNT_OF_SKYBOX].m_textures[i]);
            }

            m_ElpasedTime = 0.0f;
        }

        m_SkyboxMat.SetFloat("_Blend", m_ElpasedTime / CHANGE_TIMER);
    }

}