using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocalTransform
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public void Apply(Transform transform)
    {
        transform.localPosition = position;
        transform.eulerAngles = rotation;
        transform.localScale = scale;
    }
}
