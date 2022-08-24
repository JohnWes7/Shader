using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderDepth : MonoBehaviour
{
    public Material post;
    public float distence;
    public float farDis;
    public float scanVe;

    void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }


    private void Update()
    {
        distence += Time.deltaTime * scanVe;
        if (distence >= farDis)
        {
            distence = 0;
        }

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        post.SetFloat("_Lenght", distence);
        Graphics.Blit(source, destination, post);
    }
}