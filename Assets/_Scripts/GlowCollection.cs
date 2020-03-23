using cakeslice;
using UnityEngine;
using System.Collections.Generic;

public class GlowCollection : MonoBehaviour
{
    public List<Outline> autoCollection;

    void Start()
    {
        autoCollection = new List<Outline>(GetComponentsInChildren<Outline>());
        //foreach (Outline item in autoCollection)
        //{
        //    item.enabled = false;
        //}
    }

    void OnEnable()
    {
        foreach (Outline item in autoCollection)
        {
            item.enabled = true;
        }
    }
    void OnDisable()
    {
        foreach (Outline item in autoCollection)
        {
            item.enabled = false;
        }
    }
}
