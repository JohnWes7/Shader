using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class XRayReplacement : MonoBehaviour
{
    public Shader XRayShader;
    public Color color;

    void OnEnable()
    {
        GetComponent<Camera>().SetReplacementShader(XRayShader, "XRay");
        Shader.SetGlobalColor("_EdgeColor", color);
    }
}