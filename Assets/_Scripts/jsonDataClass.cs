using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class jsonDataClass
{
    public List<ModelEntity> models;
}

[Serializable]
public class ModelEntity
{
    public string name;
    public List<float> position;
    public List<float> rotation;
    public List<float> scale;
}
