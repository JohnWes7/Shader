using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour
{
    public Material EffectMaterial;

    public Texture2D _NoiseTex;
    public Texture2D _noiseColor;
    public Texture2D _palleteTexture;

    public float time = 0;

    [Range(0, 1)]
    public float _Intensity;
    [Range(0, 1)]
    public float _Offset;
    [Range(0, 1)]
    public float _UVSlider;
    [Range(0, 1)]
    public float _BlackScreen_Slider;


    private void Start()
    {
        #region 导出生成的随机图片
        _noiseColor = new Texture2D(64, 256, TextureFormat.ARGB32, false)
        {
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Point
        };


        Color color = new Color();

        for (var y = 0; y < _noiseColor.height; y++)
        {
            for (var x = 0; x < _noiseColor.width; x++)
            {
                if (Random.value > 0.9f)
                {
                    Color temp = _palleteTexture.GetPixel(x, y / 4);
                    color = new Color(temp.r, temp.g, temp.b, 1);
                }
                _noiseColor.SetPixel(x, y, color);
            }
        }


        //Rect rect = new Rect(0, 0, 64, 256);
        //_noiseTexture.ReadPixels(rect, 0, 0);

        _noiseColor.Apply();

        //byte[] bytes = _noiseTexture.EncodeToPNG();
        //Debug.Log(bytes[0]);

        //File.Create(Application.persistentDataPath + "2077.png");
        //File.WriteAllBytes(Application.persistentDataPath + "2077.png", bytes);

        SaveTextureToFile(_noiseColor, "2077_noiseColor.png");
        #endregion



        #region 导出随机灰图

        _NoiseTex = new Texture2D(32, 16, TextureFormat.ARGB32, false)
        {
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Point
        };


        for (var y = 0; y < _NoiseTex.height; y++)
        {
            for (var x = 0; x < _NoiseTex.width; x++)
            {
                if (Random.value > 0.7f)
                {
                    color = new Color(0, Random.value, Random.value, Random.value);
                }
                _NoiseTex.SetPixel(x, y, new Color(0, color.g, color.b, color.a));
            }
        }

        _NoiseTex.Apply();

        SaveTextureToFile(_NoiseTex, "2077_NoiseTex.png");

        #endregion

        EffectMaterial.SetTexture("_NoiseColor", _noiseColor);
        EffectMaterial.SetTexture("_NoiseTex", _NoiseTex);
        EffectMaterial.SetFloat("_UVSlider", _UVSlider);
        EffectMaterial.SetFloat("_BlackScreen_Slider", _BlackScreen_Slider);

    }



    private void Update()
    {
        //Shader.SetGlobalFloat("_offTime", time);

        //EffectMaterial.SetFloat("_offTime", time);
        //time += Time.deltaTime;
        //if (time >= 4)
        //{
        //    time = 0;
        //}
    }

    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        #region 2077
        EffectMaterial.SetFloat("_Intensity", _Intensity);
        EffectMaterial.SetFloat("_Offset", _Offset);
        EffectMaterial.SetFloat("_UVSlider", _UVSlider);
        EffectMaterial.SetFloat("_BlackScreen_Slider", _BlackScreen_Slider);
        #endregion



        if (EffectMaterial)
        {
            Graphics.Blit(source, destination, EffectMaterial);
        }
    }

    void SaveTextureToFile(Texture2D texture,string fileName)
    {
        texture = DeCompress(texture);

        var bytes = texture.EncodeToPNG();
        var file = File.Open(Application.streamingAssetsPath + "/" + fileName, FileMode.Create);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }


    public Texture2D DeCompress(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
    //将RenderTexture保存成一张png图片
    public bool SaveRenderTextureToPNG(RenderTexture rt, string contents, string pngName)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        byte[] bytes = png.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(png);
        png = null;
        RenderTexture.active = prev;
        return true;

    }
}
