using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOpts : MonoBehaviour
{
    public Color[] colors;
    public Texture2D[] textures;
    public MeshRenderer[] renderers;
    public int materialSlotTarget = 0;

    void Start()
    {
        textures = new Texture2D[3];
        string path = "Textures/" + gameObject.name;
        textures[0] = Resources.Load(path + 0, typeof(Texture2D)) as Texture2D;
        textures[1] = Resources.Load(path + 1, typeof(Texture2D)) as Texture2D;
        textures[2] = Resources.Load(path + 2, typeof(Texture2D)) as Texture2D;
    }
    public void ChangeTexture(int index)
    {
        foreach (MeshRenderer subobj in renderers)
        {
            subobj.materials[materialSlotTarget].SetTexture("_MainTex", textures[index]);
        }
    }
    public void ChangeColor(int index)
    {
        foreach (MeshRenderer subobj in renderers)
        {
            subobj.materials[materialSlotTarget].color = colors[index];
        }
    }
}
