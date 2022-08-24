using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FogPost : MonoBehaviour
{

    public Material fogPost;//后期材质

    public float dis;   //扫描的距离
    public float farDis = 300;  //扫描最远距离
    public float scanVe = 40;   //扫描时的速度
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ScaneControl();
    }




    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        ComputeRay();
        if (fogPost)
        {
            Graphics.Blit(source, destination, fogPost);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void ComputeRay()
    {
        float camFar = Camera.main.farClipPlane;
        float camFov = Camera.main.fieldOfView;
        float camAspect = Camera.main.aspect;

        float fovHalf = camFov * 0.5f;

        Vector3 toTop = Camera.main.transform.up * Mathf.Tan(fovHalf * Mathf.Deg2Rad);
        Vector3 toRight = Camera.main.transform.right * Mathf.Tan(fovHalf * Mathf.Deg2Rad) * camAspect;

        Vector3 TopLeft = Camera.main.transform.forward - toRight + toTop;
        float scale = TopLeft.magnitude * camFar;
        TopLeft = TopLeft.normalized * scale;

        Vector3 TopRight = (Camera.main.transform.forward + toRight + toTop).normalized * scale;
        Vector3 BottomLeft = (Camera.main.transform.forward - toRight - toTop).normalized * scale;
        Vector3 BottomRight = (Camera.main.transform.forward + toRight - toTop).normalized * scale;

        if (fogPost)
        {
            fogPost.SetVector("_TL", TopLeft);
            fogPost.SetVector("_TR", TopRight);
            fogPost.SetVector("_BL", BottomLeft);
            fogPost.SetVector("_BR", BottomRight);
        }
    }

    public void ScaneControl()
    {
        #region 扫描后期特效点击位置触发
        dis = Mathf.Clamp(dis + scanVe * Time.deltaTime, 0, farDis);
        fogPost.SetFloat("_Lenght", dis);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;

            if (Physics.Raycast(ray, out info))
            {
                fogPost.SetVector("_StartPos", info.point);
                dis = 0;
                fogPost.SetFloat("_Lenght", dis);
            }
        }



        #endregion
    }
}
