using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthCamera : MonoBehaviour
{
    public Shader ReplacementShader;
    public Color col;
    public float slide = 0;
    public float scan = 0;
    public float frequency = 2;

    private float hor;
    private float vec;

    
    // Start is called before the first frame update
    private void Start()
    {
        if (ReplacementShader)
        {
            GetComponent<Camera>().SetReplacementShader(ReplacementShader, "XRay");
        }
        Shader.SetGlobalColor("_EdgeColor", col);
    }

    // Update is called once per frame
    void Update()
    {
        //Shader.SetGlobalFloat("_Slide", slide / 4);

        //scan += Time.deltaTime / frequency;
        //if (scan >= 1.3)
        //{
        //    scan = 0;
        //}
        //Shader.SetGlobalFloat("_Scan", 1.3f - scan);


    }
}
