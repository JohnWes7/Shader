using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour
{

    GameObject target;
    //1.Scene面板：相当于拍电影的片场，Unity程序员可以对片场的演员进行控制或者编辑
    //2.Game面板：相当于片场导演的监视器，就是玩家玩游戏所看到的画面
    //也就是摄像机所拍摄的画面
    //3.Hierarchy：相当于场景的资源管理器
    //4.Inspector：可以看到游戏物体的所有组件
    //5.Project：模板等道具的存放仓库
    //6.Console：控制台面板

    //操作
    //Center：有游戏物体父子关系的时候，中心点位置
    //Global/local：世界坐标系/自身坐标系    东南西北和你的前后左右

    //快捷键
    //Alt 按住后鼠标左键拖拽可以调视角
    //鼠标滚轮键按下可以切换到Q手形工具：可以拖拽
    //Ctrl+Shift+F：选中摄像机按快捷键，快速将视角切换到当前位置
    //Ctrl+D    可以复制一个到当前目录
    //Ctrl+S    命运之键

    //新建物体
    //层级那里可以
    //顶部可以
    //层级右键可以

    //物体和组件的关系
    //      物体包含一个以上的组件（一个物体至少有一个Transform组件）
    //      物体是组件的盛放容器，组件不能独立存在
    //      （每一个组件都属于某一个物体）

    //      物体的各个特性是由组件组合起来形成的效果

    //预制体
    //在Hierarchy面板种是蓝色的
    //Selet选择该预制体的本体  Revert撤销对预制体的操作 Apply对预制体本体修改会印象其他引用
    //在Hierarchy面板种删除预制体的某一个子物体，会丢失预制体的引用，颜色会变成白色
    //但是还是可以通过Revert恢复成预制体
    //如果删除Project面板里预制体的本体文件，就会丢失预制体的引用，
    //Hierachy面板中预制体会变成红色，预警本地miss

    //继承MonoBehaviour的时候代码的文件名和类名要保持一致
    //否则不能作为组件添加到物体上

    //Transform.Rotate
    //  按照API要求的参数进行数值传递
    //  Space.Self和Space.World 以自身坐标系旋转，以世界坐标系来进行旋转

    //Vector3.up就是new Vector（0，1，0）

    //Transform 移动增量，如果需要移动游戏物体，就需要写在update里面

    //public GameObject target;
    //public Transform target_transform


    //设置父物体
    //  transform.parent = target.transform;
    //  transform.SetParent(target.transform);
    //
    //查找子物体
    //transform.childCount 子物体数量
    //transform.GetChild（int index）根据下标拿到子物体  找不了子物体的子物体
    //
    //target = transform.Find("Cube (2)").gameObject; 用名字查找（注意大小写空格）不能查找子物体的子物体
    //GameObject.Find("name");  能找一切，但是别多用
    //
    //当有多个tag是同样的时候，查找到的是最后创建的tag是NPC的游戏物体
    //查找物体tag是NPC
    //GameObject instance = GameObject.FindGameObjectWithTag("NPC");
    //if (instance)
    //{
    //    Debug.Log("instance:" + instance.name);
    //}
    //
    //当场景中有多个物体的tag是NPC，同时查找到，并返回（注意是数组）
    //GameObject[] instancs = GameObject.FindGameObjectsWithTag("NPC");
    //for (int i = 0; i<instancs.Length; i++)
    //{
    //    Debug.Log("name" + i + instancs[i].name);
    //}


    //InPut
    //按下A 从0通过很多小数渐变到-1
    //按下D 从0通过很多小数渐变到1
    //float h = Input.GetAxis("Horizontal");
    //float v = Input.GetAxis("Vertical");
    //
    //按下A 直接切换到-1
    //按下D 直接切换到1
    //float h = Input.GetAxisRaw("Horizontal");
    //float v = Input.GetAxisRaw("Vertical");
    //
    //按下执行，按住重复执行，按完执行,返回的bool类型
    //if (Input.GetKeyDown(KeyCode.A))
    //{
    //    Debug.Log("A");
    // }
    //if (Input.GetKey(KeyCode.S))
    //{
    //    Debug.Log("S");
    //}
    //if (Input.GetKeyUp(KeyCode.D))
    //{
    //    Debug.Log("D");
    //}
    //0鼠标左键 1鼠标右键 2鼠标中键
    //if (Input.GetMouseButton(0))
    //{
    //    Debug.Log("鼠标左键");
    //}
    //
    //可以检测按下Fire
    //if (Input.GetButton("Fire1"))
    //{
    //    Debug.Log("fire1");
    //}


    //生命周期函数
    //gameObject.SetActive(false);设置物体的启用关闭
    //gameObject.SetActive(true);
    //Destroy();销毁
    //
    //private void Awake()   在start前面执行，最先执行的方法，只执行一次
    //{
    //
    //}
    //
    //private void OnEnable()   第二执行的方法，每启用/激活一次物体时执行一次
    //{     相当于在检查器打开一个物体
    //      打开一个物体
    //}
    //private void OnDisable()   每关闭/隐藏/失活物体时执行一次
    //{     相当于在检查器关掉一个物体
    //      隐藏一个物体
    //}
    //private void OnDestroy() 游戏物体被销毁时，先调用ondisable再调用ondestroy
    //{     
    //      销毁一个物体时    
    //}
    //void Start()  只执行一次
    //
    //物理更新写在Fuxed里面
    //更新频率更具Timestep来的
    //private void FixedUpdate()  默认每过0.02s执行一次 时间步0.02
    //{
    //    
    //}
    //private void LateUpdate()  顾名思义 可以用来写跟随
    //{
    //
    //}
    //隐藏和启用
    //gameObject.SetActive(false);
    //gameObject.SetActive(true); 这个放update里面如果把这个物体删了之后就回不来了


    //灯光组件
    //  Directional 平行光 模仿太阳光
    //  Spot 聚光灯
    //  point 点光源
    //  Area 区域光
    //
    //  color 颜色值
    //
    //  Mode realtime 实时的
    //  Mix 混合显示
    //  Baked 烘焙
    //
    //  Intensity 光照强度
    //  Indirect 间接乘数
    //  
    //  Shadow Type 影子设置  No Shadow没有影子 HardShadow硬阴影 SoftShadow软影音
    //  Cookie 剪影 要图片
    //  DrawHalo 光晕
    //  Flare 眩光 要图片
    //  Render Mod 渲染模式
    //  Culing Mask剔除遮罩


    //注意用了自身transform.forward就用space.world世界坐标系
    //不然就用vector3.forward+space.self自己坐标系
    //获取组件
    //GetComponent<>();
    //添加组件
    //gameObject.AddComponent<>() 有返回值可以获取后对添加的组件进行操作
    //删除组件
    //Destroy()
    //克隆
    //Instantiate（）
    //启用不启用组件 激活/不激活
    //组件名.enabled = true/fales；


    //is：判断这个变量是否能装换成对应类型，如果可以返回true，否则返回false
    //as：尝试转换，如果能，返回转换过后的对象，如果不能，返回null
    //
    //is和as要搭配使用，可以避免空引用报错
    //
    //
    //Project面板种Resources文件不能错任何子母大小写
    //从Resources里面加载资源
    //加载Resources文件夹中的资源   泛型加载
    //GameObject newObj = Resources.Load<GameObject>("Prefabs/Tank");
    //实例化游戏资源（clone）
    //Instantiate（newObj）
    //记得克隆完把名字改成克隆源的名字（就是把后面的"clone"去掉）


    //OnGUI（不是每一帧都会执行）
    //
    //  东西必须写在private void OnGUI()里面
    //
    //  显示文字    GUI.Label（GUI.Label(new Rect(50, 100, 50, 50), "OnGUI Test"); 
    //
    //  显示按钮    注意GUI.Button（）有返回值 可以用在if里面
    //if (GUI.Button(new Rect(100,200,50,50),"test Btn"))
    //{
    //    labelShow = "这里是btu";
    //}
    //
    //  显示开关    toggle1= GUI.Toggle(new Rect(100, 100, 50, 50), toggle1, "开关");
    //  注意开关括号里面的bool值外面要接受一下，不然开关切换不了
    //
    //  显示拖动条   t1= GUI.VerticalSlider(new Rect(50, 150, 20, 100), t1, 10, 0);竖的
    //                      GUI.HorizontalSlider横着的
    //  也要有个值接受不能写死
    //
    //  选择块     
    //      select= GUI.Toolbar(new Rect(20, 100, 300, 75), select, new string[] { "bar1", "bar2", "bar3" });
    //      可以在几个选项中选择
    //
    //  进度条
    //      scroba= GUI.VerticalScrollbar(new Rect(100, 200, 20, 75), scroba, 5, 10, 0);
    //      据说是可以做进度条
    //
    //  获取物体在摄像机中的位置
    //      Vector3 pos = Camera.main.WorldToScreenPoint(tank.transform.position);
    //      注意是和GUI中的y是反的
    //      GUI.Label(new Rect(pos.x, Screen.height- pos.y-30, 50, 50), "血条");
    //      需要用Screen.height - pos.y



    //Time
    //  Time.time
    //      运行到现在总共的时间
    //  Time.deltaTime
    //      每一帧之间的时间
    //  Time.unscaledTime
    //      不受时间缩放影响的时间，而且一启动游戏就开始计算
    //
    //  Time.fixedTime
    //      fixedUpdate里面运行到现在的总时间
    //  Time.fixedDeltaTime
    //      fixedUpdate里面每一次运算到下一次运算之间隔的时间（默认0.02）
    //
    //
    //  Time.timeScale
    //      改时间流逝速度
    //      直接改Time.timeScale=0.5f


    //Mathf
    //  Clamp
    //      Clamp(int value, int min, int max);
    //      将给定值夹在给定的最小整数和最大整数值定义的范围之间。(小于最小值取最小值，大于最大值取最大值)
    //      如果给定值在最小值和最大值内，则返回给定值。
    //  Lerp
    //      Lerp(float a, float b, float t);（a每次变成ab插值得一半）
    //      a = a + (b - a) * t
    //      （插值）
    //  Mathf.SmoothDamp平滑插值
    //      dampTest = Mathf.SmoothDamp(dampTest, 10, ref dampSpeed, 2, 10);
    //      Debug.Log(dampTest);
    //      target.transform.position = new Vector3(dampTest, 0, 0);


    //PlayerPrefs
    //  存客户端的数据，是键值对方式存在的
    //      
    //  存
    //      PlayerPrefs.SetInt(string key, int value);
    //  取
    //      PlayerPrefs.GetInt(string key);
    //      可以是其他的类型有float，int，string
    //  判断有没有key
    //      PlayerPrefs.HasKey(string key);
    //  根据值删除
    //      PlayerPrefs.DeleteKey(string key);
    //  删除全部
    //      DeleteAll();


    //Rigidbody：刚体
    //  Mass:质量
    //  Drag：阻力/摩擦力
    //  AngularDrag：角阻力/转弯摩擦力
    //  UseGravity：是否使用重力
    //  IsKinematic：运动学刚体，当两个刚体发生碰撞，他不会位移
    //  Collision Detection：碰撞检测精度
    //          Discrete：离散性监测
    //          Continuous：连续性检测
    //          Continuous Dynamic：连续动态检测
    //
    //  Interpolate：插值
    //      none 没有插值运算
    //      interpolate 根据上一帧的位置做插值运算
    //      Extrapolate 根据上一帧的运动预测下一帧的插值
    //      
    //  Constraint：
    //      Freeze Position：锁轴
    //      Freeze Rotation：锁轴旋转
    //
    //  碰撞检测
    //      1.两个游戏物体发生碰撞，必须有一方有刚体组件，两方都必须有碰撞体组件
    //      
    //  碰撞器
    //      
    //  触发器
    //      即使当前游戏物体没有刚体，当带有刚体游戏组件的游戏物体撞到我
    //      也会触发
    //      碰撞器和触发器只要两个发生碰撞的物体有一个物体有刚体组件就会触发
    //      碰撞器和触发器不会因为当前代码失活而失效
    //      








    //W2


    //三角函数
    //Mathf.Sin
    //Mathf.Deg2Rad
    //Mathf.Rad2Deg

    //坐标系
    //  左手坐标系
    //  


    //向量
    //  向量相加  三角形方法 Vector3(a,b,c)+Vector3(x,y,z)=Vector3(a+x,b+y,c+z)
    //  向量相减
    //
    //  相等向量：大小和方向相等的就是相等向量
    //  0向量：方向是任意方法，模长为0
    //  单位向量：模长为1的向量
    //  Vector3
    //
    //  Lerp    pos1---->pos2
    //
    //  SLerp               --------
    //                      ---           ---
    //            pos1--                   -->pos2


    //四元数
    //  Quaternion(x,y,z,w)
    //  轴角对<x,y,z> theta
    //  x=nx*sin(theta/2)
    //  y=ny*sin(theta/2)
    //  z=nz*sin(theta/2)
    //  w=cos(theta/2)
    //
    //  Quaternion.Euler()把Vector3变成四元数
    //  
    //  AngleAxis(float angle, Vector3 axis);
    //      传出一个按照哪个轴转多少度的四元数
    //      可以后续再*就可以转向
    //
    //  rotation.eulerAngles
    //      unity把四元数用欧拉角的形式展示再Inspector面板上（0~360），更号理解
    //
    //  LookRotation
    //      看向一个向量(Vector3 forward);
    //
    //  RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta);
    //      调至另一个Quaternion但又最大度数限制
    //
    //  四元数和四元数相乘等于叠加
    //      (按照前面的四元数的坐标系旋转)
    //          ==前面的坐标系按照后面的角度发生变化
    //  四元数和Vector3相乘等于把Vector3转个方向
    //  
    //  四元数和欧拉角旋转
    //  欧拉角旋转：优点容易理解   缺点：会造成万向节死锁，而且不可避免
    //  四元数旋转：难理解 但是不会万向节死锁


    //audio组件
    //  Mute 静音
    //  
    //  Bypass Effects 绕过效果
    //  Bypass Listener Effects 绕过监听器效果
    //  Bypass Reverb Zone 绕过混响区域
    //  
    //  Play On Awake 唤醒时播放
    //  Loop 循环
    //
    //  Priority 优先级（滑动）
    //  Volume 音量
    //  Pitch 音调
    //  Stereo Pan  立体声像（左  右）
    //  Spatial Blend 空间混响（2D  3D）
    //  Reverb Zone Mix 混响区混音

    //Audio Listener
    //  监听声音
    //  一个场景只能有一个这个组件

    //Unity支持的音频格式
    //  .aiff               适用于较短得声音片段
    //  .wav              适用于较短得声音片段
    //  .mp3,OGG    适合较长得音乐片段
    //
    //  Force To Mono   强制将多声道转换成单声道
    //  Normalize  勾选后会对声音有优化
    //  Load In Background 后台加载，这样子声音加载不会卡顿
    // 
    //  Decompress On Load 加载时解压
    //  Compress In Memory 在内存中是以压缩得形式存在的 播放的时候要解压
    //  Streaming   以流得形式存在，播放得时候要解码，占用得内存最少，但是播放的时候要现解码
    //  （一般占用内存越小，可能播放会卡顿）
    //
    //  Preload Audio Data 如果勾选进入场景马上加载
    //              如果不勾选第一次使用才会加载进来 加载到内存
    //
    //  Compression Format压缩格式
    //      PCM 最高质量格式存储
    //      Vorbis 相对来说压缩得更小 可以根据Quality调质量
    //      ADPCM 压缩的最小 噪音很多 脚步声


    //SceneManager.LaodScene(“场景名字”)可以使用生成设置里面的index加载，但最好不用
    //      加载的场景一定要放到生成设置里面区
    //      可以加载自己=重新加载
    //      老版本加载方法
    //          Application.LoadLevel("");
    // 玩家设置
    //  Bundle Identifier资源包标识符（特别重要）
    //          以公司域名倒着写的 com.tence.wechat
    //  Version 版本号
    //    1.0.2；
    //
    //  Scripting Define Symbols  脚本定义符号
    //  
    //  安卓 SDK  JDK
    //
    //  




    //W3

    //Camera
    //  Clear Flags(清除标志):
    //      Skybox: 天空盒
    //      Depth Only: 仅考虑深度，而且不受其他不渲染层的遮挡印象
    //      Don't Clear: 不清除上一帧留下的渲染数据
    //      
    //  Culling Mask(裁剪层，剔除遮罩):
    //      这里面是控制摄像机会渲染哪些层的游戏物体
    //      是通过二进制的形式去控制的
    //
    //   Projection: 
    //      Perspective:透视摄像机
    //              一般渲染3D场景或者说是3D摄像机，能渲染出Z轴值（相对）的变化效果
    //      Orthographic:正交摄像机
    //              一般渲染2D场景或者2D游戏，游戏物体在Z轴运动效果不能显示
    //  
    //  Size/Field of view: 摄像机渲染范围大小
    //  Clipping Planes:  Near/Far  渲染最近/最远距离   所需渲染的物体必须在这个范围那味，否则不会被渲染
    //
    //  Viewport Rect:  xy 以屏幕左下角为起始点，按比例来计算当前摄像机渲染画面的位置
    //                  wh 当前摄像机渲染的画面在整个屏幕中大小
    //  Depth:  值越大越后渲染，后渲染的画面覆盖前面渲染的画面，所以值越大显示越靠前
    //
    //  Target RenderTexture：
    //      目标纹理
    //      1 创建RenderTexture
    //      2 创建一个新的摄像机，作为当前物体的渲染摄像机当给相机的TargetTexture
    //          赋值RenderTexture时，这个摄像机的Depth就会失效，不会跟其他摄像机进行对比
    //          渲染到主屏幕，会将当前摄像机拍摄到的内容显示到RenderTexture


    //Line Renderer
    //  Materrial 划线的材质
    //  Positions 多个点，划线的点集
    //  UseWorldSpace 是否使用世界坐标系
    //  Loop 是否循环(是否首位相接)
    //  Width 划线的宽度，通过曲线来控制


    //力 AddForce
    //力是矢量，有大小有方向
    //牛顿第二定律  a = F/m;
    //动量守恒定律  M1V1 = M2V2;
    //动量定理 Ft = m*ΔV ;
    //冲量 I = Ft = m*ΔV ;
    //
    //1. AddForce施加力
    //2. AddTorque施加扭矩
    //3. AddForceAtPosition
    //
    //  ForceMode.Force
    //      施加一个力 ΔT=0.02s
    //  ForceMode.Acceleration
    //      施加一个加速度 ΔT=0.02s
    //  ForceMode.Impulse
    //      执行一次 施加一个冲量
    //  ForceMode.VelocityChange
    //      执行一次 增加ΔV一次
    //
    //3. AddForceAtPosition
    //  功能说明：此方法用于为参数position增加一个力force,其参考坐标系为世界坐标系，作用方式为mode，默认值为ForceMode.Force。此方法与方法AddForce不同，AddForce方法对刚体的施加力时，不会产生扭矩使物体发生旋转，而AddForceAtPosition方法是在某个坐标点对刚体施加力，这样很可能会产生扭矩使刚体产生旋转，具体如下
    //  1、当力的作用点在刚体重心时，刚体不会发生旋转；
    //  2、当力的作用点不在刚体重心时，由于作用点的扭矩会使刚体发旋转，但是，当作用力的方向经过刚体 的重心坐标时，不发生旋转。
    //
    //4.AddRelativeForce
    //  以自身坐标系来施加力冲量
    //5.AddRelativeTorque
    //  以自身坐标系施加扭矩


    //RayCast
    //
    //  声明射线
    //  new Ray(Vector3 origin, Vector3 direction);
    //  直接从相机发出射线
    //  Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //  
    //  物理射线检测
    //  Physics.Raycast()
    //  返回值是bool类型，根据返回值只能确定当前射线有没有碰撞到其他物体
    //
    //  RaycastHit；(通常命名hitinfo)
    //  当我们的射线碰撞到了一个游戏物体的碰撞体后，返回一个hit值这个对象包含的信息
    //  下面的属性
    //  public Collider collider { get; }   碰撞体
    //  public Vector3 point { get; set; }  实际碰撞到的点的世界坐标位置
    //  public Vector3 normal { get; set; } 碰撞点所在平面的法向量
    //  public Vector3 barycentricCoordinate { get; set; }
    //  public float distance { get; set; } 碰撞体和射线起始点的距离
    //  public int triangleIndex { get; }   
    //  public Vector2 textureCoord { get; }    
    //  public Vector2 textureCoord2 { get; }
    //  public Vector2 textureCoord1 { get; }
    //  public Transform transform { get; } 碰撞到的物体的Transform
    //  public Rigidbody rigidbody { get; } 碰撞到物体的刚体
    //  public ArticulationBody articulationBody { get; }
    //  public ArticulationBody articulationBody { get; }
    //  
    //  Physics.Raycast()里面可以填layer层，记得加括号(1 << LayerMask.NameToLayer("UI"))
    //      layer的int类型是1左移对应层数所得来的，这样操作可以一次检测到多个层的游戏物体
    //
    //  Physisc.RaycastAll()
    //      同理一条射线能够触碰多个碰撞体，并返回碰撞信息数组，如果没有碰撞到碰撞体，返回的数组长度为零

    //test = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //从屏幕空间到视窗空间的变换位置。
    //屏幕空间以像素定义。屏幕的左下为（0,0）；右上是(pixelWidth,pixelHeight)，Z的位置是以世界单位衡量的到相机的距离。
    //视口空间是规范化的并相对于相机。相机的左下为（0,0）；右上是（1,1），Z的位置是以世界单位衡量的到相机的距离。
    //
    //Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10))
    //从屏幕空间到世界空间的变化位置。
    //屏幕空间以像素定义。屏幕的左下为(0,0)；右上是(pixelWidth,pixelHeight)，Z的位置是以世界单位衡量的到相机的距离。



    //延迟函数
    //  Destroy(object, float time);time时间之后销毁object类型（不写立即销毁）
    //  DestroyImmediate()  强烈建议使用Destroy
    //
    //  Invoke
    //  Invoke(string methodName,float time)
    //      延迟执行函数，通过方法名延迟调用对应函数
    //      注意：当此脚本或脚本所在的游戏物体失活，延迟函数依然会执行
    //      当此脚本或者游戏物体被销毁，延迟函数就不会执行
    //
    //  CancellInvoke()
    //      不传名字就会取消当前代码内所有延迟函数
    //      如果传参数，就取消对应函数延迟调用
    //      
    //  InvokeRepeating("TestInvoke",2,1);
    //  两秒之后调用之后每一秒调用一次
    //

    //异步加载
    //  不会引起代码阻塞
    //协程
    //  协程不是多线程，是假的
    //  应用场景WWW加载网络数据
    //  异步加载资源
    //  
    //
    //启动协程
    //StartCoroutine("PrintA", 4);
    //StartCoroutine(PrintA(4));
    //IEnumerator ie = PrintA(2);
    //StartCoroutine(ie);
    //
    //TEST
    //Debug.Log("Start 1");
    //Debug.Log("Start 2");
    //StartCoroutine(PrintA(3));
    //Debug.Log("Start 3");
    //Debug.Log("Start 4");
    //
    //
    //StartCoroutine("PrintA",1);
    //StopCoroutine("PrintA");//能关闭协程
    //
    //StartCoroutine(PrintA(2));//不建议使用
    //StopCoroutine(PrintA(2));//不能关闭协程只能通过关闭所有协程来关闭
    //关闭所有协程：StopAllCoroutines();
    //
    //IEnumerator ie = PrintA(4);
    //StartCoroutine(ie);
    //StopCoroutine(ie);//能关闭协程
    //
    //协程失活
    //注意：
    //  当协程开启后，当前代码组件失活不影响协程继续执行
    //  当协程开启后，当前代码组件销毁，协程就不继续执行
    //  当协程开启后，当前代码组件所在的游戏物体失活，协程也不会继续执行
    //   和invoke不一样
    //      所以以后有协程的代码不能挂在经常失活或者激活的游戏物体上
    //
    //yield
    //
    //  yield return new WaitForSeconds(2);//等多少秒
    //  yield return new WaitForEndOfFrame();//运行到当前帧最后执行
    //  yield return new WaitForFixedUpdate();//0.02s
    //  yield return null;//等下一帧再执行
    //  yield return 0;//等下一帧再执行
    //  yield break;//直接跳出协程
    //  yield return www;
    //  yield return async;
    //  yield return PrintB();//等待这个协程执行完
    //
    //协程异步加载Resources资源
    //  ResourceRequest resourceRequest;
    //  resourceRequest = Resources.LoadAsync(path);
    //
    //判断协程有没有执行
    //可以Coroutine x = StartCoroutine(PrintA(3));
    //然后判断x是不是null


    //Unity特殊文件夹
    //Resource：资源文件夹，可以通过路径直接用API Resource进行加载
    //  工程文件打包时，不在特殊文件夹时，跟其他文件都没有依赖关系时，此资源不会打进包
    //  但是Resource文件内所有资源，无论是否跟其他资源有无依赖关系，都会打进包
    //  为了减小包的大小，resource不能乱发资源
    //
    //Standard Assets：默认文件夹 
    //  此文件夹内资源会被优先编译，代码也会
    //
    //Plugins：插件一般放这个文件夹（不能像Resource一样乱放）
    //  还有一些Android和iOS平台区分的文件
    //
    //Editor
    //  存放Unity工具
    //  继承Editor的类的工具文件
    //  这个文件夹的所有资源都不会打进包
    //
    //Application.persistentDataPath:固定数据路径
    //Application.dataPath


    //图片资源
    //TextureType
    //  Default：    默认图片类型
    //  Normal map： 法向贴图
    //  Editor GUI： 编辑器使用图片
    //  Sprite： 精灵图片
    //  Cursor： 鼠标贴图
    //  Cookie： 剪影
    //  Lightmap：   光照贴图
    //  Single Channel： 单通道贴图
    //  
    //Alpha Sources：alpha通道资源
    //  None 没有Alpha通道资源
    //  InputTextureAlphy: 自带Alpha资源
    //  FromGrayScale：根据RGB均值计算得来的Alpha值
    //
    //Non Pow of 2
    //  none:   不做图片处理
    //  To Nearest： 处理图片大小最接近
    //  To Larger：  图片分辨率最大尺寸来
    //  To Smaller： 图片分辨率最小尺寸来
    //
    //Read/Write enable：可读写
    //
    //Generate Mip Maps
    //  根据不同距离显示不同分辨率的图片，耗费内存
    //  跟3D的LOD功能类似
    //
    //Wrap Mode
    //  贴图拼接模式
    //
    //Filter Mode
    //      过滤模式
    //  Point 点（模糊过度填充）
    //  Bilinear 双线性
    //  Trilinear 三线性
    //
    //Aniso Level：摄像机视野和场景角度很小时，是否增强纹理精细度等级
    //  等级越高，精细度越高，



    //SpriteRenderer 2D图片渲染组件
    //
    //Sprite：存放2D图片资源，2D图片TextureType必须时Sprite
    //Color：这个值轻易不要改
    //Flip：x轴反转，y轴反转
    //DrawMode：绘制模式
    //  simple：简单模式
    //  sliced：已切片（九宫格切图）
    //  Tiled： 填充
    //      tiledMode：连续的（会出现半个的情况）
    //                 Adaptive（自适应）
    //  
    //SortingLayer 排序图层
    //Order in layer 图层顺序
    //  先看排序图层，越下面的越靠前
    //  后看图层顺序，越大越靠前


    //Collider2D
    //  Density：    密度
    //  Material：   物理材质
    //  Is Trigger： 是触发器
    //  Used by effect：由效果器使用 
    //  Used by Composite：  由复合使用
    //
    //Rigidbody2D
    //  Simulate：   模拟的
    //  Sleeping Mode：  休眠的模式
    //          从不休眠
    //          开始唤醒
    //          开始休眠
    //
    //  Gravity Scale：重力缩放
    //      控制受到的重力大小
    //  

    //效应器
    //Point Effector:模仿的磁铁的功能
    //  需要勾选BoxCollider的istriger选项和UsedByEffector功能
    //  ForceMagnitube： 正的表示斥力，负的表示引力，数值大小也就是力的大小
    //  ForceSource：效果器计算所有力的的源，可以用该选项将点定义为质量中心或者碰撞器位置
    //  ForceTarget: 效果器施加力的目标，该目标允许力施加在质量中心或者碰撞器位置
    //  ForceMode：  力模式
    //                  常量施加同样且与距离无关的点力，
    //                  而InverseLinear和InverseSquared将力作为一个和距离相关的函数施加
    //

    //Surface Effector 模拟的传送带
    //  需要勾选上boxcollider的Usebyeffector
    //
    //  Speed:  当数据为正时，会有沿表面的速度，如果速度为负，则刚体将沿与其正速度时相反的方向移动。
    //          会自动施加加速或减速的力，以便刚体达到该速度
    //
    //

    //AreaEffector2D 模仿的风
    //  需要勾选BoxCollider的istriger选项和UsedByEffector功能
    //  Use Global angle 是否使用世界坐标系
    //  Force angle 力的角度x轴正方向为0°逆时针方向为正
    //  Force Magnitude：力的大小
    //  力变化
    //  力目标


    //Platform Effector 2D 平台通过性
    //  需要勾选上boxcollider的UsedByEffector功能
    //  Surface arc 平台通过性的角度
    //  使用单向
    //  表面弧度

    //Buoyancy Effector2D 水
    //  需要勾选BoxCollider的istriger选项和UsedByEffector功能
    //  Density 密度
    //  Surface Level 水平面

    //扩展方法
    //声明扩展方法
    //public static GameObject FindChild(this GameObject root, string name)
    //{
    //    return null;
    //}
    //https://www.bbsmax.com/A/B0zqgeKN5v/






    //Animation
    //  Unity对老版的Animation动画支持越来越弱，推荐使用的是新版Animator
    //
    //添加帧事件
    //  当动画片段播放到帧事件位置时，触发这个函数
    //  一般用作特效播放死亡动作播放完毕销毁函数
    //
    //Animation组件
    //  Animation：这个是当前默认播放动画片段
    //  Animations：是Animation数组，可以放多个animation动画片段
    //  Play Auto：是否自动播放 idle站立动画
    //
    //Play()
    //
    //CrossFade()
    //  动画之间切换是由一个时间
    //  类似渐入渐出
    //
    //

    //动画文件的设置
    //  动画文件或者模型文件是以fbx格式存在在project目录里的
    //  
    //模型的设置
    //  Model选项 Scale Factor 模型比例缩放，一般是0.01，如果所在工程不一样则以工程为准
    //  Rig选项 AnimationType：
    //          legacy：老板动画系统；
    //              如果用Animation组件播放，需要将动作切换到Legacy
    //          Generic：通用设置
    //              一般非任务模型动作可以选这个
    //          Humanoid：新版动画系统人类
    //          当选择此选项之后，会要求定义Avatar文件（骨骼）
    //          动作文件可以使用其他模型的Avatar文件
    //Animation 选项
    //  有可能是一个完整的动画合计分动画是根据帧数来分的
    //  也有可能是拿到的一个整的动画
    //  
    //  剪切动画时，许哟检查右侧灯的颜色，绿色灯代表循环匹配的很好
    //  黄色灯一般，红色灯匹配很差（指动画第一帧和动画最后一帧是否匹配）


    //AstarPathFinding
    //
    //Navigation unity自带的寻路系统
    //
    //AgentRadius:烘培路径可行区域和非可行区域的间隔
    //AgentHeight:路径时高度小于这个值得地方就是不可行区域
    //Max Slope：最大可行区域坡度
    //Step Height：步高（台阶高度）
    //Drop Height：下落高度
    //Jump Distance：跳跃距离
    //
    //实现低点到高点得跳跃
    //  Off Mesh link
    //


    //  NavMeshAgent
    //AgentType
    //
    //Speed:速度
    //Acceleration 加速度
    //StoppingDistance：距离目标点多远就停下来（速度不能太快，加速度不能太小，否则跟目标点距离等于stoppingdistance
    //                      的时候可能停不下来）
    //
    //Radiu 障碍躲避半径
    //Height 障碍躲避高度
    //Quality 障碍躲避精细度，速度越快对精细度要求越高
    //Priority 权重，值越小优先级越高，优先级高的绕着优先级低的物体走
    //
    //Auto traverse off mesh link 是否支持off mesh link功能
    //Auto repath 自动重新规划路径
    //Area Mask：区域覆盖，当前寻路组件没有覆盖的区域，即使有烘培好的地方
    //              这个物体也不会过去
    //
    //Move 把xz坐标加到这个agent上



    //Animator
    //新建Animator Controller
    //  AnyState  任意状态常用作播放死亡状态
    //            不管当前角色在播放什么状态，都可以被杀死让后播放死亡动作
    //  Entry/Exit    进入状态机和退出状态机，进入状态机默认连接默认状态动画
    //  Idle  橙色一般是是默认动画播放待机动画
    //  Run（灰色）   一般状态
    //
    //  Create Sub-State Machine  创建子状态机
    //      嵌套在同一图层的状态机内
    //
    //状态机的图层和参数
    //  Layer
    //  Paramters 状态切换参数（int float bool trigger）
    //
    //动作状态切换
    //  Solo    动画切换优先
    //  Mute    动画切换禁止
    //
    //  HasExitTime 有退出时间（如果是想在动作的任意事件切换就不点）
    //  Setting
    //      ExitTime    退出时间
    //      Fixed Duration  固定持续时间
    //      Transition Duration 过度持续时间
    //      Transition Offset   过度偏移
    //      Interruption Source 中断源
    //
    //重要Conditions
    //  这里面的切换动作参数是paramers里定义的，条件可以是大于等于不等于小于
    //  tigger 触发类型，从true到false都是触发
    //  
    //Animator组件
    //  Controller AnimatorController   文件
    //  Avator  放骨骼文件
    //  Apply Root Motion   勾选表示用动画中的位移
    //      当游戏物体上的代码组件中有OnAnimatorMove内置函数
    //      位移就收代码控制
    //  Update Mode 
    //      正常更新，物理更新，不受时间缩放影响
    //  Culling Mode
    //      始终有动画
    //      当摄像机没有渲染该物体，就会停止
    //      当摄像机没有渲染该物体，整个动画都不会被渲染


    //Animator1D混合树
    //  新建：
    //      CreatState->From New Blend Tree
    //  混合树控制：
    //      Parameter:  控制当前混合树的参数
    //                  示意图里面的蓝色三角形是三个动画的权重
    //      Threshold:  阈值(-1~0左走动画权重越来越低，直走权重越来越高
    //                  0~1右走权重越来越高)阈值可以自己设定
    //      Automate Threshold  自动计算
    //                  通过动画片段的位移来计算阈值，我们一般用自己设置的
    //      
    //Animator2D混合树
    //  参数变成了两个，分别控制两个轴
    //  那个秒表一样的符号是播放速度
    //  可以改成负的来倒着播
    //
    //  SimpleDirectional
    //      在同一个方向上不能有多个动画片段
    //  FreeformDirectional
    //      在同一方向上可以有多个动画片段
    //  FreeformCartesian
    //      是不在方向上操作的动画片段
    //  

    //多layer层动画状态机
    //  baselayer层的设置
    //  和新建的layer层设置对比
    //
    //  Weight权重，baselayer层默认是1而且不能改
    //  如果希望new layer也有播放权限
    //  需要将new layer层的weight也设置喂1
    //  
    //  new layer的Mask是只有上身是活跃的，单独新建了一个AvatarLayerMask
    //  （这个是在project面板中新建的一个文件）
    //  
    //  Blending    Override覆盖baselayer的动作
    //              Additive两个动画融合
    //
    //  

    //IK动画
    //  记得在animator中开IK
    //  什么是IK：人物的四肢点AvatarIKGoal.RightHand
    //
    //  设置IK权重，匹配四个IK位置和方向
    //  
    //  权重0关闭，1开启
    //  MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    //  MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
    //
    //  MyAnimator.SetIKPosition(AvatarIKGoal.RightHand, target.transform.position);
    //  MyAnimator.SetIKRotation(AvatarIKGoal.RightHand, target.transform.rotation);


    //动作匹配Animator.Matchtarget()
    //要开 应用跟运动
    //不然不会动

    //OverrideAnimatiorController
    //  可以重写状态机的状态
    //  但是不能改变原来状态机的切换逻辑

    //动画的重定向
    //  什么叫动画的重定向
    //      其他模型的动画用作自身模型，虽然是不同模型，但是播放的是同一个动作
    //  注意，动作的重定向必须是两个骨骼动画是一样的或者很接近的
    //      否则不能实现动画的重定向




    //UI


    //Canvas组件
    //  UI绘制在Canvas上，可以理解为画画的纸
    //  Canvas在两个模式下(Overlay和Camera)会覆盖整个屏幕
    //      做屏幕UI显示时，只需处理好Canvas中的显示即可
    //  *UI所有UI元素必须建立在Canvas下，否则就会出现不显示的问题*
    //      
    //三种渲染模式
    //  Overlay：Canvas贴在相机镜头上，*所有UI元素在场景前方显示*
    //      Pixel Perpect：高清像素清晰度渲染
    //      Sort Order：多Canvas时，控制Canvas的显示顺序，越大的在前方显示
    //  Camera：使用相机显示Canvas
    //      修改Canvas下Canvas组件Render Mode属性：Camera
    //      将相机拖给Canvas下Canvas组件的RenderCamera属性
    //      将cubu调整到到Canvas前
    //      调整Canvas中Plane Distance 控制Canvas到渲染相机的距离
    //  
    //  Plane Distence：Canvas到相机的距离
    //          当Order In Layer（图层顺序）和Sortinglayer相同时 距离相机越近显示越靠前
    //  排序关系
    //      排序层一致时，Order in Layer越大越靠前
    //      Sorting Layer：配置的排序层顺序，越靠下，显示越靠前
    //
    //  一般SortingLayers配置
    //  Default
    //  BackCanvas
    //  BackEffect
    //  MiddleCanvas
    //  MiddleEffect
    //  FrontCanvas


    //Canvas缩放
    //  该组件会拉伸所有Canvas下的子元素
    //UI Scale Mode：拉伸模式
    //  静态模式
    //  像素计算模式
    //  
    //  Reference Resolution 参考分辨率（参考图）（也就是画布的大小是恒定分辨率的）
    //  Screen Match Mode 适配方式
    //      宽度或高度自适应
    //      完全自适应（自动计算宽高变化量作为缩放倍率）
    //      世界模式中一米对应多少像素


    //项目中
    //  使用根据屏幕尺寸进行拉伸
    //  设计分辨率，填写效果图的像素值（覆盖整个屏幕）
    //  选择Expand：自动计算高度自适应拉伸还是宽度自适应拉伸
    //  覆盖满整个屏幕的背景图计算
    //      默认拿到1280x720的背景图
    //      计算方法
    //          18：9下：缺失两边（设计分辨率高度/9）*18
    //          16：9下：一般都是16：9
    //          4：3下：缺失上下，（设计分辨率宽度/4）*3


    //RectTransform组件
    //  出现位置：出现在所有Canvas子元素中，所有UI元素应该都有RectTransform组件
    //
    //  Pos（XYZ）：位置（XYZ），单位：像素
    //  Width，Height：宽和高，单位（像素）
    //  Anchors：锚点，*以父物体参考*
    //      三种情况
    //      单锚点
    //      双锚点
    //      四锚点
    //  Pivot：轴点（篮圈），以当前物体参考
    //      调整轴点后，位置计算，旋转，缩放都会受到影响，一般轴点都保留在中心点


    //元素渲染顺序
    //  以Hierarchy参考
    //      下方物体在上方物体前显示
    //      子物体在父物体前显示
    //      下方物体永远在前显示，无论上方层次结构


    //目录建立
    //字体导入
    //图片导入
    //  图片考入项目
    //  类型转换为精灵（才能被UI使用）
    //  使用Image组件


    //基础组件
    //Panel组件
    //  一个完整页面一个Panel
    //  Panel位于Canvas子物体，四锚点在四角，边距为0，所以Panel和
    //  Canvas一样大，Canvas和屏幕一样大，所以Panel和屏幕一样大
    //  元素显示处理在Pane中正常，则屏幕中也正常
    //Text组件
    //Text
    //  Text 文本内用
    //Character
    //  FontStyle 字库选择
    //  Font Size 字体样式（斜体，加粗）
    //  Font Size 字体大小（字号）
    //  Line Spacing 行间距
    //  Rich Text   是否开启富文本
    //      加粗<b>文字</b>
    //      斜体<i>文字</i>
    //      大小<size=字号>文字</size>
    //      修改颜色
    //          <color=颜色名>文字</color>
    //          <color=#颜色数(十六进制)>文字</color>
    //Paragraph
    //  Alignment
    //  Align By Geometr    几何对齐（参考字占用的几何空间）
    //  Horizontal Wrap/Overflow 水平溢出（折行，溢出{出右边框}）
    //  Vertical Truncate/Overflow   垂直溢出（截断不显示，溢出[出下边框]）
    //  Best Fit    字号自适应
    //      关闭：使用原始字号
    //      开启：字号会在最小值和最大值之间自动调整
    //Color
    //Material
    //Raycast Target

    //阴影
    //  Shadow（Script）组件
    //外发光
    //  Outline（Script）组件


    //Image组件
    //  Color：用于混色，美术有时提供白图和色号，程序员对图处理，
    //      染成想要的颜色，可以减少包体大小
    //  四种类型
    //      普通模式（图标）
    //          Preserve Aspect：保持图片宽高比（高度或宽度自适应）
    //          Set Native Size：可以快速恢复图片原始尺寸
    //          SetNativeSize（）；
    //      裁剪模式（九宫格，三公格）
    //      瓦片模式（无缝贴图）
    //      填充模式（进度条）
    //          Fill Method 填充方式
    //          Fill Origin 填充起始点
    //          Fill Amount 填充百分比
    //          Clockwise 顺时针或逆时针
    //          Preserve Aspect 保持图片的高宽比，高度或者宽度自适应
    //          Set Native Size 可以快速恢复图片原始尺寸


    //屏幕显示过程
    //  CPU->加载数据->内存->显存->显卡->显示器
    //  Batchs 当前渲染屏幕所有内容需要的绘制调用数
    //  每一张独立的UI图，会产生一个DrawCall

    //优化DrawCall（UI切片）
    //  将多个UI切片，合并成一张UI图，UI界面只加载一张UI图片显示
    //制作图集
    //  AllowRotation 允许旋转
    //  Tight Packing 允许合并


    //双相机叠加
    //  建立Camera
    //  将UI相机拖给Canvas
    //  UI相机设置为深度值填充，UI相机的深度要高于主相机
    //  将主相机不渲染UI元素，将UI相机只渲染UI元素（CullingMask）

    //UI事件交互*三个条件*
    //Canvas添加Graphic Raycaster（Script）
    //交互元素（Button）
    //场景中存在事件系统

    //Canvas的Graphic Raycaster（Script）
    //Ignore Reversed Graphic 是否忽略Canvas反向的事件操作
    //Blocking Objects 遮挡事件的物体（2D物体[精灵片]，3D物体）
    //Blocking Mask遮挡事件物体所在的渲染层

    //UI的射线检测会依次检测对象树下的所有元素，有任何射线检测成功
    //  则会反馈到带有事件响应的组件（Button）中
    //如果想实现射线穿透，可以将前面显示的UI元素的射线检测关闭


    //using UnityEngine.UI
    //Button组件
    //Interactable 是否可交互
    //Transition过度
    //  Target Graphic  目标图形
    //  Normal Color    正常颜色
    //  Highlighted Color   高亮颜色（鼠标放在上面的颜色）
    //  Pressed Color   按下颜色
    //（Selected Color）  选择颜色(新加的)
    //  Disabled Color  已禁用颜色
    //  Color Multiplier    色彩乘数
    //  Fade Duration   淡化持续时间
    //
    //Navigation导航
    //
    //回调结构
    //按钮添加事件（Unity拖拽）
    //  添加脚本，内部写回调函数（挂在场景对象下）
    //  找到Button，OnClike事件，添加（脚本所在对象，并选择回调函数）
    //  运行项目，点击按钮（*做一些事情*）
    //  触发回调函数


    //自动添加事件
    //
    //获取Button组件所在的对象
    //GameObject.Find("/Canvas/Button");gameobjct查找不能找到没有活性的物体
    //


    //Toggle组件
    //*is On 用于判断当前Toggle是否为开启状态*
    //
    //Interactable 是否禁用
    //Transition 变化方式
    //Target Graphic 背景图
    //如果Toggle构成组，则需要将ToggleGroup拖给Toggle

    //因为所有的Toggle公用一个回调函数
    //  所以单靴Toggle切换时，一个Toggle关闭回调执行，另一个Toggle开启回调执行
    //  只处理开启的Toggle的回调

    //Toggle事件处理
    //  判定当前的开关是否打开
    //动态传参和静态传参
    //动态传参会根据Toggle的IsOn属性传递bool数据
    //静态传参会根据Toggle事件处配置值传递bool数据

    //组单选时，获取选中的Toggle
    //  Toggle的回调函数拖给左右的Toggle组件
    //  不应关闭事件
    //  相应开启事件，通过ToggleGroup中ActiveToggle（）获取开启的Toggle
    //  根据开启Toggle的名称执行相应代码


    //Slider组件
    //  省略到导航下面
    //Fill Rect 填充滑动条选中区域的背景图部分（填充矩形）
    //HandleRect 滑动条的球（处理矩形）
    //
    //Direction 滑动条的滑动方向
    //Min Value 起始值
    //Max Value 结束值
    //Whole Numbers 是否必须为整数
    //Value 滑动条的当前值

    //InputField
    //  省略到导航下面
    //Text Component 输入内容时显示文本的文本组件
    //Text  当前输入的内容值
    //Character Limit   最大输入字符数
    //Content Type  输入的文本类型（数字，实数，数字英文，名称，密码）
    //  LineType    行类型（当行，回车跳出，回车换行）
    //Placeholder   输入框原始内容（默认显示）
    //
    //Caret Blink Rate  光标闪动频率
    //Caret Width   光标宽度
    //Custom Caret Color    自定义光标颜色
    //Selection Color   被选中文字的颜色
    //Hide Mobile Input 移动设备下是否隐藏键盘
    //Read Only 是否只读（文本框不可编辑）


    //Mask组件
    //实现圆形头像
    //  参数：是否显示父物体本图
    //  Mask组件配合Image组件使用
    //  带有Mask组件的图，会显示子物体图片中，父物体Alpha通道不为0的部分


    //ScrollView组件
    //Content   实际显示区域的UI对象
    //Horizontal    是否开启横向滚动
    //Vertical  是否开启纵向滚动
    //Movement Type 滚动类型
    //  无边界自由滚动
    //  有边界带回弹效果（回弹系数：Elasticity）
    //  有边界无回弹效果
    //Inertia   拖拽惯性
    //Scroll Sensitivity    滚轮系数
    //Horizontal Scrollbar 横向滚动条
    //  Visibility 可见性
    //      一直显示
    //      自动隐藏
    //      自动隐藏，并支持自动扩展区域空间
    //  横纵滚动条交叉区域预留空间
    //Vertical Scrollbar    纵向滚动条
    //  Visibility 可见性
    //      一直显示
    //      自动隐藏
    //      自动隐藏，并支持自动扩展区域空间
    //  横纵滚动条交叉区域预留空间


    //Layout排列组件
    //VerticalLayoutGroup   纵向自适应排列组件
    //Padding
    //  Left
    //  Right
    //  Top
    //  Bottom
    //外框的内边距（左右上下）
    //Spacing
    //  元素间距
    //ChildAlignment    起始角落（外框的九个点）
    //Child Controls Size   排列组件是否控制子元素的宽高（是否控制宽度，是否控制高度）好像要必须要求子元素强制自适应才可以控制子元素宽高自适应
    //Child Force Expand    子元素强制自适应（宽[和外框对其]，高[根据外框等分计算后设置子元素高]）


    //Grid Layout Group
    //Padding 
    //  外框的内边距（左右上下）
    //Cell Size（X，Y）
    //  内部元素的宽高不能自由修改子元素的宽高
    //Spacing
    //  子元素间距（横向，纵向）
    //Start Corner 第一个子元素位于的角（左上，右上，左下，右下）
    //Start Axis 开始排列的轴方向（横向，纵向）
    //ChildAlignment 子元素的对其方式
    //Constraint 固定行列数（自适应，设置固定列[列数]，试着固定行[行数]）


    //Dropdown组件
    //Template  下拉菜单的ScrowllView
    //Caption Text  当前选中的选项对应的文字组件（选中的选项的文字内容，显示在这个组件中）
    //Caption Image 当前选中的图片对应的图片组件（选中的选项图片，显示在这个组件中）
    //
    //Item Text 下拉菜单中，储存选项的文本组件（来自Template下，每个选项会复制一份）
    //Item Image    下拉菜单中，储存选项的图片组件（来自Template下，每个选项会复制一份）
    //
    //Value 当选下拉菜单选中的选项列表，选项所在的索引值
    //Options   一组选项列表（List，选项可以是文字，也可以是图片）
    //          当选项组更改时，回调函数会获得对应选项的索引值


    //类扩展需要的是静态类，名称任意
    //第一个参数表示当前方法是扩展string类的方法
    //str表示string对象
    //public static void Say（this string str）
    //{ }
    //
    //DOtween
    //Image.DOFade（） 淡入淡出
    //Lex.transform.DOLocalMove()   位置变化
    //DOScale（） 变大变小，改变scale
    //DOColor（） 颜色变化
    //DOText    文本逐渐展开
    //默认是一起执行，动画同时执行
    //
    //DOKill(true)  瞬间变成执行完的状态返回一共执行的个数
    //
    //复合动画
    //  队列执行动画
    //  DOTween.Sequece()   *主义要用这个生成新队列*
    //  Sequece.Append()    在动画队列后方追加动画
    //  Sequece.Prepend()   在动画队列最前方追加动画
    //  Sequece.Insert()    在特定时间添加一个动画
    //动画回调方法
    //  OnComplete()    动画执行完成时，执行回调函数（tweener和队列都有）
    //  



    //编写自定义事件步骤
    //  引入自定义事件命名空间（UnityEngine.EventSystems）
    //  实现自定义事件Interface
    //  用户交互会触发回调函数

    //显式实现接口的抽象方法（避免多个接口中，抽象方法重名）
    //
    //编写自定义事件步骤
    //  引入自定义事件命名空间 using UnityEngine.EventSystems;
    //  实现自定义事件Interface
    //  用户交互会触发回调函数
    //
    //点击事件接口系列
    //  IPointerEnterHandler    鼠标光标移入射线检测区域，触发回调函数
    //  IPointerExitHandler     鼠标光标移出射线检测区域，触发回调函数
    //  IPointerDownHandler     鼠标在射线检测区域中按下。触发回调函数
    //  IPointerUpHandler       
    //      前提  需要先触发按下事件
    //      情况1 鼠标在射线检测区域中按下并抬起（正常点击）
    //      情况2 鼠标在射线检测区域中按下，移出射线检测区域后抬起（点击取消）
    //  IPointerClickHandler    鼠标在射线检测区域中按下并抬起
    //                          触发回调函数，Up的情况2是不会触发Click事件的
    //
    //拖拽事件实现
    //
    //  半透明渐变贴图在iOS设备下，使用压缩会造成突变质量损失
    //  所以可以将班头面渐变UI切片单独制作成真色彩图集
    //
    //拖拽事件组
    //  IBeginDragHandler   检测到射线后，当拖拽动作开始时执行*一次*回调函数
    //  IDragHandler    拖拽开始后，有拖拽位置变化时，执行回调函数（每个移动执行）不是每一帧都执行，移动才执行
    //  IEndDragHandler 拖拽进行中时，当鼠标或手抬起时，执行一次回调函数
    //
    //  *先Down->OnStartDrag->OnDrag->OnUp->OnEndDrag*
    //  
    //如何获取物体位置
    //  位置      相对量，需要有参照物图
    //  屏幕坐标  手点击屏幕时生成
    //  DragArea本地坐标    控制遥感（DragBar）的位置
    //
    //


    //PointerEventData是Unity从设备硬件接受到的数据和事件相关的一些数据
    //拖拽移动的实现
    //手指触摸屏幕，产生坐标点
    //移动实现，需要将屏幕的坐标点，转换为被移动物体的的本地坐标系下的位置点
    //使用被移动物体的transform，通过本地坐标系的点实现位置的改变

    //相对的父物体是谁
    //屏幕的坐标点
    //摄像机是谁
    //需求：如何通过屏幕坐标系下的点，转换到DragArea本地坐标系下的点

    //开发部分
    //  DOTween得按钮动画
    //  页面跳转
    //  创建引导脚本（单入口得思想，场景中只有一个脚本，所以引导脚本中Start会在程序一开始执行时运行，类似Main函数）





    //M3W2



    //  服务器
    //      C++,Java,NodeJS(JavaScript),PHP,Python,Go
    //  Unity的开发语言
    //      C#，Lua
    //  JSON
    //      全名  JavaScript Object Notation
    //      功能  JavaScript对象标记语言，是一种跨平台，跨语言轻量级的数据交换和储存
    //      格式
    //  JSON最简单的写法
    //      JSON写法
    //        变量名   = 值   分割 变量名     值 
    //      {"Username":"admin","Passwold":"123"}
    //      {"name":"johnwes7","id":"000"}
    //      bit 位
    //      byte 8位
    //
    //  数字型
    //  字符串 "" ''
    //  布尔 true
    //  null
    //  数组（列表） [值1，值2]
    //  对象（字典） {"键1":"值1","键2":"值2"}
    //
    //字符含义
    //  大括号组：对象字典
    //  中括号组：数组列表
    //  冒号：赋值，左侧是变量或键，右侧为值
    //  逗号：元素分割符，最后一个元素后没有都好
    //  双引号组：修饰变量（可以不加），表示string数据类型
    //  单引号组：同双引号组
    //JSON
    //  储存在服务器中的数据
    //  储存在策划配置的Excel中

    //将Excel中的数据导出为JSON
    //  填写Excel数据
    //  将Excel数据，导出为CSV
    //  通过文本编辑器，打开CSV内部内容，复制
    //  将数据粘贴到转换工具上
    //  分隔符表示，列间的分隔符，CSV通过逗号分割，所以填写逗号（如果是粘贴的Excel 就不填）
    //  http://www.bejson.com/
    //
    //  execl插件
    //
    //  值全部使用字符串（X）不选，影响C#的数据类型
    //  转换方式，按行转换为对象（单元格数据和表头的列名产生赋值）

    //*tips*：
    //  如果使用代码进行CSV文件转JSON文件
    //  记得Excel导出的CSV文件时GB2312的字符编码，转换前，
    //  需要将文件内容字符集转换为UTF-8


    //C#使用JSON数据
    //  数据存储（序列化）：将C#的数据格式，转化为JSON字符串，储存或传输
    //  数据使用（反序列化）：将JSON字符串中存储的数据，转化为C#可用的数据格式
    //      实现代码逻辑
    //  *序列化*   （程序数据 -> JSON）
    //  *反序列化*  (JSON字符串 -> 程序数据)

    //TextAsset
    //  读取Resources下Json/test文件的内容
    //

    //注意：如果使用Unity的内置解析类，JSON最外层结构必须是对象结构
    //  当一个类需要存储在另外一个成员变量中时，需要给类声明加特性
    //  [System.Serializable]
    //  (表示对象数据可以被序列化后存储)
    //JSON存东西存路径
    //
    //注意：如果用的是Unity自带的解析类
    //最外层必须是对象（所以要套一层壳）
    //解决方案：外侧包一个对象，对象里面就放一个list就好


    //TextAsset
    //文件支持类型
    //  txt
    //  html
    //  htm
    //  xml
    //  bytes
    //  json
    //  csv
    //  yaml
    //  fnt

    //litJson
    //  导入src文件夹中的C#文件
    //  删除C#以外的文件
    //		删除导致Warning的特性（中括号）代码
    //
    //  序列化
    //
    //      JsonMapper.ToJson()
    //  反序列化
    //      获得JsonData（类似C#的object）：JsonMapper.ToObject()
    //		获得指定类型：JsonMapper.ToObject<T>()
    //      JsonData的下层数据，也是JsonData，所以使用前需要进行转换
    //      JsonData包含ToJson方法，可以直接序列化
    //      JsonData有个Add方法
    //  注意
    //      反序列化不要求最外层是对象
    //      对象内部存储对象不需要[System.Serializable]修饰Class声明
    //      List<Dictionary<string, int>> DicList = new List<Dictionary<string, int>>();
    //      json["Items"] = DicList;会报错，jsondata
    //
    //JsonData
    //  增加Add或者直接键名=XXX
    //  删除Remove（）
    //      jsonData.Remove("Cost");//填键名



    //XML
    //  JSON相对于XML来说，数据体积学校，传输和解析的速度更快
    //  http://www.w3school.com.cn/xml/index.asp
    //  http://www.cnblogs.com/fish-li/archive/2013/05/05/3061816.html
    //  XML已经被JSON完全替代了
    //  在此我们极力向您传递的理念是：元数据（有关数据的数据）应当存储为属性，而数据本身应当存储为元素。


    //读写要C#命名空间，Syetem开头
    //using System.IO;
    //
    //Appliction应用的相关数据储存在内部
    //Unity开发的应用必定可写目录
    //  Application.persistentDataPath
    //
    //检测文件是否存在
    //  File.Exists(path)
    //
    //读取文件所有内容
    //linux unix路径朝右/，windows默认\都可以
    //  File.ReadAllText(path)
    //
    //写入文件所有内容
    //File.WriteAllText(path, "saveData");System.text里面的encoding.UTF8
    //
    //流写入方法
    //FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/Test.json");
    //StreamWriter streamWriter = fileInfo.CreateText();
    //streamWriter.WriteLine("[1,0,5,3,6,7,5]");
    //streamWriter.Close();
    //streamWriter.Dispose();

    //Config静态类来储存JSON位置
    //transform.Find可以填相对路径HeaderCount/Gold/Add


    //MVC分层开发思想
    //
    //显示部分实现（View视图）
    //数据处理实现（Modle数据模型）
    //逻辑判定实现（Controller控制器）
    //
    //MVC
    //  C控制器
    //  V视图
    //  M数据模型
    //MVC的开发步骤
    //  1页面预制体处理
    //  2处理数据（数据模型脚本）
    //    JSON读写操作
    //    数据的CURD操作
    //      C Creat 增加数据
    //      U Update 修改数据
    //      R Read 读取数据
    //      D Delet 删除数据
    //    根据控制器调用模型的方式数量，在模型中编写对应数量的函数，以供调用
    //  3显示（视图脚本）
    //    文本的显示
    //    图片的显示
    //    列表的显示
    //    其他的UI资源（模型，动作，特效）
    //  4逻辑控制（控制器脚本）
    //    生命周期函数
    //    逻辑控制语句
    //  事件响应

    //MODLE
    //  有数据库交互
    //  SELECT * FROM Items WHERE ItemID IN（写ID）


    //图集的命名空间 Unity.Engine.U2D
    //Resource加载
    //  SpriteAtlas类型
    //  GetSprite(string name) 这个方法可以从图集里面调图片
    //  string.Slipe("分隔符")返回的string数组 https://www.cnblogs.com/mingforyou/p/3952357.html


    //StreamingAssets文件夹
    //  这是一个只读、不可写的目录。
    //  官方推荐使用Application.streamingAssetsPath来获得该文件夹的实际位置，其可以规避平台差异。
    //  https://docs.unity3d.com/Manual/StreamingAssets.html
    //  对于ios平台，其等价于:Application.dataPath + "/Raw";
    //  对于android平台，其等价于:"jar:file://" + Application.dataPath + "!/assets/";
    //  *注意，不支持写入*


    //Instatiate执行是，会调用Awake周期函数，同时执行的代码会优先于Srart执行
    //  所以Start可以取到传递过来的数据

    //代码实现按钮静态传参
    //  lambda表达式卡住变量，i一直向上累加，导致lambda内部i越界问题

    //CardInfo GetInfo(string style)
    //{
    //  CardInfo info = null;
    //  infoDic.TryGetValue(style, out info);
    //      这样可以直接得到info
    //      比if里判断ContainsKey要快
    //      因为判断完取的时候又要遍历一遍
    //}



    //添加动态传参的回调函数
    //IsStudentToggle.onValueChanged.AddListener(on =>
    //{
    //    if (on)
    //    {
    //        Debug.Log("ON");
    //    } 
    //});

    //添加静态传参就是包一层
    //buttons[i].onClick.AddListener(delegate { OnSelect(i); });


    //where T:class
    //where实现约束
    //1.接口约束。
    //例如，可以声明一个泛型类 MyGenericClass，这样，类型参数 T 就可以实现 IComparable<T> 接口
    //
    //2.基类约束：指出某个类型必须将指定的类作为基类（或者就是该类本身），才能用作该泛型类型的类型参数。
    //class MyClassy<T, U>
    //  where T : class
    //  where U : struct
    //  {
    //  }
    //
    //  3.where 子句还可以包括构造函数约束。
    //  可以使用 new 运算符创建类型参数的实例；但类型参数为此必须受构造函数约束 new() 的约束。
    //  new() 约束可以让编译器知道：提供的任何类型参数都必须具有可访问的无参数（或默认）构造函数
    //  new() 约束出现在 where 子句的最后。
    //
    //  4.对于多个类型参数，每个类型参数都使用一个 where 子句，
    //interface MyI { }
    //class Dictionary<TKey, TVal>
    //where TKey : IComparable, IEnumerable
    //where TVal : MyI
    //{
    //    public void Add(TKey key, TVal val)
    //    {
    //    }
    //}
    //
    //5.还可以将约束附加到泛型方法的类型参数，例如：
    //public bool MyMethod<T>(T t) where T : IMyInterface { }
    //请注意，对于委托和方法两者来说，描述类型参数约束的语法是一样的：
    //delegate T MyDelegate<T>() where T : new()
    //  https://www.cnblogs.com/soundcode/p/5798769.html


    //Directory (System.IO)
    //  里面可以创建文件夹，判断文件夹存不存在

    //Debug.Log(Input.mousePosition + "  " + eventData.position);这两个是一样的
    #region Unity拖拽
    //public bool dragOnSurfaces = true;

    //private GameObject m_DraggingIcon;
    //private RectTransform m_DraggingPlane;

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    //找到有Canvas组件的物体
    //    var canvas = FindInParents<Canvas>(gameObject);

    //    if (canvas == null)
    //        return;

    //    //We have clicked something that can be dragged.
    //    // What we want to do is create an icon for this.
    //    //给实例化的新GameObject命名
    //    m_DraggingIcon = new GameObject(this.name);
    //    //放到指定路径
    //    m_DraggingIcon.transform.SetParent(canvas.transform, false);

    //    //Move the transform to the end of the local transform list.
    //    //Puts the panel to the front as it is now the last UI element to be drawn.
    //    m_DraggingIcon.transform.SetAsLastSibling();//移动到末尾

    //    //给新GameObject添加<Image>组件
    //    var image = m_DraggingIcon.AddComponent<Image>();
    //    //把当前脚本所挂载的物体的图片赋给新GameObject
    //    image.sprite = GetComponent<Image>().sprite;
    //    image.SetNativeSize();//恢复初始大小
    //    image.raycastTarget = false;


    //    if (dragOnSurfaces)
    //        m_DraggingPlane = transform as RectTransform;
    //    else
    //        m_DraggingPlane = canvas.transform as RectTransform;


    //    SetDraggedPosition(eventData);
    //}

    //public void OnDrag(PointerEventData data)
    //{
    //    if (m_DraggingIcon != null)
    //        SetDraggedPosition(data);
    //}

    //private void SetDraggedPosition(PointerEventData data)
    //{

    //    if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
    //        m_DraggingPlane = data.pointerEnter.transform as RectTransform;

    //    var rt = m_DraggingIcon.GetComponent<RectTransform>();
    //    Vector3 globalMousePos;
    //    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
    //    {
    //        rt.position = globalMousePos;
    //        rt.rotation = m_DraggingPlane.rotation;
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    if (m_DraggingIcon != null)
    //        Destroy(m_DraggingIcon);

    //    Debug.Log(eventData.pointerEnter.name);
    //    Debug.Log(m_DraggingPlane.anchoredPosition);
    //}

    ////实现不断往相应的上层parent查找所需组件
    ////Component: Base class for everything attached to GameObjects.
    //static public T FindInParents<T>(GameObject go) where T : Component
    //{
    //    //如果go为null，返回null
    //    if (go == null) return null;


    //    //查找go身上相应组件（Canvas）
    //    //找到后返回comp
    //    var comp = go.GetComponent<T>();
    //    if (comp != null)
    //        return comp;

    //    //查找t的parent
    //    //循环查找，不断往上层找parent，直到找到相应组件（Canvas）
    //    Transform t = go.transform.parent;
    //    while (t != null && comp == null)       //t有上层parent && 第1步里未找到组件
    //    {
    //        comp = t.gameObject.GetComponent<T>();
    //        t = t.parent;
    //    }
    //    return comp;
    //}
    #endregion





    //M3W3
    //[System.Serilizable]
    //用于在C#运行时传递程序中各种元素（类，结构体，方法，枚举，组件）
    //的行为信息的声明标签，一个声明标签是通过放置在他所在的应用元素的前面的方括号[]中来描述
    //
    //[HideInInspector]
    //public int ID = 99;
    //隐藏成员变量，防止Inspector的值影响到他
    //同时保证脚本变量的可访问度
    //
    //私有变量，检视面板可见
    //Unity会将对象进行序列化存储，所以即使是私有的，那么标记为可序列化后，就会显示
    //public变量默认是可序列化的
    //[SerializeField]
    //private string Name
    //
    //监视面板显示对象
    //public Numerical Num
    //[Serializable]加在这个类上面
    //  如果对象不标记为可序列化，则Unity在存储的时候，会认为他不可被序列化
    //  也就无法被现实
    //Unity的内置JSON工具运行原理与之类似
    //
    //[Range(0,150)]
    //public int Age;
    //这个可以限定范围（最小0，最大150）
    //
    //[Tooltip("XXXXXX")]添加变量悬浮提示文字
    //[Range(0,150)]
    //public int Age;
    //一个成员变量可以添加多个特性
    //
    //[Space(20)]//当前成员变量上方留20像素
    //
    //[Header("Data")]//成员变量上方加入一个标题文字
    //
    //[Multiline(4)]//指定输入框有4行
    //
    //[TextArea(3,6)]//框默认显示3行，最大显示6行，超过用滚动条控制
    //
    //[RequireComponent(typeof(BoxCollider))]当前组件依赖于
    //  当前组件挂载在对象上时，盒子碰撞器会一起被添加上去
    //  当Player组件没有被移出时，盒子碰撞器不能被删除
    //      BoxCollider 类名
    //      typeof(BoxCollider) 类型 System.Type
    //
    //[ExecuteInEditMode]
    //在编辑模式下运行
    //使生命周期函数在编辑器状态下可以执行
    //游戏中也可以正常使用后
    //Update（）在场景中对象发生变化或项目组织发生变化时
    //  会在编辑器下执行
    //
    //[AddComponentMenu("M3W3/监视器扩展",1)]
    //将组件添加到AddComponent上
    //第一个参数：分类名/组件名
    //第二个参数：列表中显示的顺序
    //
    //
    ////给小齿轮添加一个回调函数
    //[ContextMenu("打印年龄")]
    //public void DebugAge()
    //{
    //    Debug.Log(Age);
    //}
    //
    //给属性添加回调函数
    //[ContextMenuItem("打印Email", "DebugEmail")]
    //[Tooltip("右键试试")]
    //public string Email;
    //public void DebugEmail()
    //{
    //    Debug.Log(Email);
    //}




    //特殊目录
    //  回顾
    //      Plugins：需要跨语言调用的代码存储目录，手机SDK接入
    //      Resources：存储跟随游戏包的资源目录
    //      StreamingAssets：只读，存储跟随游戏包的资源目录
    //  编辑器目录
    //      Editor
    //      制作多目录合并
    //      项目中建立Editor目录，编辑器相关的逻辑和资源回放到其内部
    //      相关内容在打包生成，不会一起生成到项目中，玩家也不会使用到编辑器相关的内容
    //      Editor目录下的脚本，无法挂在在场景对象下
    //  命名空间
    //      Unity代码逻辑命名空间：UnityEngine
    //      Unity编辑器命名空间：UnityEditor
    //          此命名空间*不要出现在游戏被发布的逻辑代码中，会导致项目打包失败*


    //异或位运算（加密算法）
    //  相同为0，不同为1
    //  A与B异或得到C，那么
    //      B与C异或，可再次获得A
    //      A与C异或，可再次获得B
    //
    //  如果A是明文，B是密钥，C就是密文
    //  使用B^C (密钥异或密文) 可以得到A（明文）




    //检视器高级修改（外挂式编程）
    //  项目中建立Editor目录
    //  Editor目录下建立外挂式开发脚本
    //  将编辑器脚本与原始脚本关联
    //
    //
    //1引入编辑器命名空间，检视器属于编辑器开发范畴
    //  using UnityEditor;
    //2继承Editor类，使用编辑器相关的成员变量和生命周期函数
    //  public class M3W3EditorTest : Editor 
    //  外挂脚本因为存储在Editor目录下，所以不会被打入最终的游戏包
    //  不继承自Mono，而是继承自Editor
    //3*关联脚本编辑器开发脚本和组件脚本建立外挂关联关系*
    //[CustomEditor(typeof(M3W3.M3W3))]
    //
    //
    //base.OnInspectorGUI();
    //
    ////值类型layout
    ////标题显示
    //EditorGUILayout.LabelField("人物相关属性");
    //
    ////string类型
    //m3W3.name = EditorGUILayout.TextField("名字 name", m3W3.name);
    //
    ////int类型
    //m3W3.Age = EditorGUILayout.IntField("年龄 age", m3W3.Age);
    //
    ////bool类型
    //m3W3.isHard = EditorGUILayout.Toggle("是否是硬核模式", m3W3.isHard);
    //
    ////vector3类型
    //m3W3.position = EditorGUILayout.Vector3Field("位置 position", m3W3.position);
    //
    //////////////////////////////
    //引用类型
    //对象数据绘制
    //参数1，标题
    //参数2，原始组件的值
    //参数3，成员变量的类型
    //参数4，是否可以将*场景中*的对象拖给这个成员变量
    //Unity类
    //m3W3.weapon = EditorGUILayout.ObjectField("武器 weapon", m3W3.weapon, typeof(GameObject), true) as GameObject;
    //
    //m3W3.sprite = EditorGUILayout.ObjectField("精灵 sprite", m3W3.sprite, typeof(Sprite), false) as Sprite;
    //
    /////////////////////////////////////////////////////////////
    //枚举数据类型绘制
    //整数转枚举
    //int id = 0;
    ////单选枚举
    //m3W3.singleEnum = (M3W3SingleEnum)EditorGUILayout.EnumPopup("单选枚举", m3W3.singleEnum);
    ////多选枚举
    //m3W3.multipleEnum = (M3W3MultipleEnum)EditorGUILayout.EnumFlagsField("多选枚举", m3W3.multipleEnum);
    //*多选枚举Nothing是0，everything是-1，第一个是2的0次方*
    //
    ////滑动条绘制/////////////////////////////////////////////////////
    ////滑动条显示（1标题，2原始变量，最小值，最大值）
    ////m3W3.M3W3Data.atk = EditorGUILayout.IntSlider(new GUIContent("int滑动条"), m3W3.M3W3Data.atk, 0, 150);
    //m3W3.M3W3Data.atk = (int)EditorGUILayout.Slider(new GUIContent("int滑动条"), m3W3.M3W3Data.atk, 0, 150);
    //
    //m3W3.M3W3Data.def = EditorGUILayout.Slider(new GUIContent("float滑动条"), m3W3.M3W3Data.def, 0, 100);
    //
    //////消息框显示///////////////
    //if (m3W3.M3W3Data.atk>100)
    //{
    //    EditorGUILayout.HelpBox("Warning atk过高", MessageType.Warning);
    //}

    //if (m3W3.M3W3Data.atk<20)
    //{
    //    EditorGUILayout.HelpBox("Error atk过低", MessageType.Error);
    //}
    //
    ////按钮显示////////////////////////////
    //GUILayout.Button("V buttom");
    //GUILayout.Button("V buttom");
    //
    ////开始横向排列
    //EditorGUILayout.BeginHorizontal();
    //
    //GUILayout.Button("H buttom");
    //GUILayout.Button("H buttom");
    //
    ////结束横向排列
    //EditorGUILayout.EndHorizontal();
    //
    //按钮的按下触发（和GUI一样）
    //if (GUILayout.Button("H buttom"))
    //{
    //    Debug.Log("H buttom");
    //}




    //顶部菜单实现按钮 不需要继承任何的类，但要使用命名空间
    //MenuTest
    //[MenuItem("ToolTest/导出AB资源包")]
    //static void BuildAB()
    //{
    //    Debug.Log("导出AB资源包");
    //}
    //
    //[MenuItem("ToolTest/打印persistentDataPath")]
    //public static void DebugPersistentDataPath()
    //{
    //    Debug.Log(Application.persistentDataPath);
    //}




    //弹窗开发
    ////参数1 效用 true不可以和unity的窗口和一起 false可以
    ////参数2 窗口标题
    ////参数3 是否马上focus到该窗口
    //GetWindow<PopWindowTest>(true, "PopWindowTest窗口", false);
    //
    //继承: EditorWindow
    ////开窗口调用
    //private void OnEnable()
    //{
    //
    //}
    //
    ////关窗口调用
    //private void OnDisable()
    //{
    //
    //}
    //
    ////开窗口后一直调用
    //private void Update()
    //{
    //
    //}
    //
    //private void OnGUI()
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    GUILayout.Button("Test");
    //    GUILayout.Button("Test");
    //    EditorGUILayout.EndHorizontal();
    //
    //
    //}
    //
    ////场景结构发生变化是，执行回调函数
    //private void OnHierarchyChange()
    //{
    //
    //}
    //
    ////项目结构发生变化时，执行回调函数
    //private void OnProjectChange()
    //{
    //
    //}
    //
    ////选中物体发生变化，执行回调函数
    //private void OnSelectionChange()
    //{
    //    Debug.Log(Selection.activeObject.name);
    //}



    //获取被选中的物体
    //Selection.activeObject
    //
    ////有点类似Update，可以发送射线
    ////当鼠标在Scene视图下发生变化时，执行该方法，比如鼠标移动，鼠标的点击
    ////当选中关联的脚本挂载的物体
    //private void OnSceneGUI()
    //{

    //    if (!isEditor)//不在编辑状态就直接返回
    //    {
    //        return;
    //    }

    //    //鼠标左键按下时发射一条射线
    //    //非运行时，使用Event类
    //    //Event.current.button 判断是按的哪个键
    //    //Event.current.button 判断鼠标事件的方式
    //    if (UnityEngine.Event.current.button == 0 && UnityEngine.Event.current.type==EventType.MouseDown)
    //    {
    //        //从鼠标的位置发射射线
    //        //因为是从Scene视图下发射射线，跟场景中的摄像机并没有关系，所以不能使用相机发射射线的方法
    //        //从编辑器GUI中的一个点向世界定义一条射线，参数一般都是鼠标的坐标

    //        Ray ray = HandleUtility.GUIPointToWorldRay(UnityEngine.Event.current.mousePosition);
    //        RaycastHit hitInfo;
    //        if (Physics.Raycast(ray,out hitInfo , 100, 1<<LayerMask.NameToLayer("Default")))
    //        {
    //            Debug.Log(hitInfo.collider.name);
    //        }
    //    }
    //}
    //Destroy may not be called from edit mode! Use DestroyImmediate instead.
    //Destroying an object in edit mode destroys it permanently.
    //编辑器模式下不能用Destroy要用DestroyImmediate





    //AssetBundle
    //字符串总结
    //https://www.cnblogs.com/tianjifa/p/9207241.html
    //
    //导出AB包
    //[MenuItem("ToolTest/导出AB资源包")]
    //static void BuildAB()
    //{
    //    string path = Application.dataPath;
    //
    //    path = path.Substring(0, path.Length - 6);
    //    path += "assetbundle/";
    //
    //    //判断文件夹存不存在
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }
    //
    //    //导出AB包核心代码
    //    //参数1：ab包文件存储路径
    //    //参数2：导出选项
    //    //参数3：平台（不同平台的ab包是不一样的）
    //    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    //
    //}
    //
    //存储在Resources下的资源，最终会存储在游戏的主体包中，发送给用
    //户，手机系统上，如果需要做资源的更新，是无法使用Resources即时更新（不
    //需要关闭手机游戏，即可实现游戏更新）
    //
    //AB包是独立于游戏主包存在的资源存储文件，使用内部资源时，需要单独下
    //载和加载。

    //AssetBundle的定义
    //AssetBundle是把一些资源文件，场景文件或二进制文件以某种紧密的方式保存在
    //一起的一些文件。

    //AssetBundle内部不能包含C#脚本文件，AssetBundle可以配合Lua实现资源和游戏
    //逻辑代码的更新

    //AB包配置修改后或AB内部的资源修改后，都需要重新生成AB包

    //AB包名称如果配置为这样的结构”ui/package”，ui会作为AB包存储的父目录，
    //package是AB包的名称
    //导入到AB包的资源，包内部是不存在目录的

    //AB包压缩方式（实际选择根据项目情况分析）
    //  不压缩：AB包比较大，下载较慢，加载速度快（CPU不用运算解压缩）
    //  LZMA算法压缩：默认压缩模式，文件尺寸居中，加载速度居中
    //  LZ4算法压缩：5.3以后可用，压缩比例高，文件小，加载速度偏慢

    //编写顶部菜单栏按钮
    //AB包分平台，所以导出的API不同
    //  Android
    //  iOS
    //  Windows
    //  Mac
    //
    //BuildPipeline.BuildAssetBundles()
    //  参数1 ：AB存储路径
    //  2：导出选项
    //      None：无任何选项
    //      UncompressedAssetBundle：默认压缩模式为LZMA，开启这个选项就不会压缩AB包
    //      CollectDependencies：默认开启，开启依赖记录机制
    //  主Manifest记录了依赖关系
    //      DeterministicAssetBundle：将AssetBundle的哈希校验值，存储在ID中（默认开启）
    //      ForceRebuildAssetBundle：强制重新导出所有的AB包
    //      ChunkBasedCompression：使用LZ4算法压缩AB包
    //      StrictMode：严格模式(基本不用)
    //和存储目录同名的文件和文件.manifest，是主AB包及主AB包配置文件
    //  *无扩展名文件：AB包*
    //  *文件名.manifest文件：AB包对应的配置文件*
    //
    //  *注意！！！：AB包不能重复加载*
    //
    //如果想要处理依赖关系的加载，必须加载主AB包，因为依赖关系的存储
    //  都存储在主AB包的配置文件中
    //
    //依赖加载
    //string path = Application.dataPath;
    //
    //path = path.Substring(0, path.Length - 6);
    //path += "assetbundle/DependTest";
    //
    //加载主AB包
    //AssetBundle main = AssetBundle.LoadFromFile(path + "/DependTest");
    //
    ////获得AB包配置文件
    //AssetBundleManifest mainFest = main.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //
    //获得依赖
    //string[] dependents = mainFest.GetAllDependencies("prefabs");
    //
    //加载依赖
    //for (int i = 0; i < dependents.Length; i++)
    //{
    //    AssetBundle.LoadFromFile(path + "/" + dependents[i]);
    //}
    //
    //加载要的ab包
    //AssetBundle prefabs = AssetBundle.LoadFromFile(path + "/prefabs");
    //
    //加载预制体
    //GameObject go = prefabs.LoadAsset<GameObject>("ABTest");
    //
    //实例化
    //Instantiate<GameObject>(go, Vector3.zero, Quaternion.identity, GameObject.Find("/Canvas").transform);





    //异步加载
    //  异步加载Resource
    //IEnumerator LoadImage()
    //{
    //    ResourceRequest rr = Resources.LoadAsync<Sprite>("UI/Support/DD_000");
    //
    //    yield return rr;
    //
    //    GameObject.Find("/Canvas/Image").GetComponent<Image>().sprite = rr.asset as Sprite;
    //}
    //
    //  不开协程实现异步加载
    //StartCoroutine(LoadImage());
    //ResourceRequest rr = Resources.LoadAsync<Sprite>("UI/Support/DD_000");
    //rr.completed += (AsyncOperation ao) => { GameObject.Find("/Canvas/Image").GetComponent<Image>().sprite = rr.asset as Sprite; };
    //
    //
    //  异步加载AB
    //IEnumerator LoadAB()
    //{
    //    string path = Application.dataPath;
    //
    //    path = path.Substring(0, path.Length - 6);
    //    path += "assetbundle/new/ui";
    //
    //    AssetBundleCreateRequest abr = AssetBundle.LoadFromFileAsync(path);
    //
    //    yield return abr;
    //    
    //    //AssetBundleCreateRequest里面有个assetBundle就是加载出来的AssetBudle
    //    Sprite sprite = abr.assetBundle.LoadAsset<Sprite>("DD_000");
    //    GameObject.Find("/Canvas/Image").GetComponent<Image>().sprite = sprite;
    //}
    //
    //
    //  不开协程异步加载AB
    //string path = Application.dataPath;

    //path = path.Substring(0, path.Length - 6);
    //path += "assetbundle/new/ui";

    //AssetBundleCreateRequest abr = AssetBundle.LoadFromFileAsync(path);

    //abr.completed += (AsyncOperation ao) =>
    //{
    //    Sprite sprite = abr.assetBundle.LoadAsset<Sprite>("DD_000");
    //    GameObject.Find("/Canvas/Image").GetComponent<Image>().sprite = sprite;
    //};



    //  卸载AB包
    //ab.Unload(false);
    //  false   不把所调用的资源一并卸载
    //  true    把所有调用的资源一并卸载
    //
    //GC Resources.UnloadUnusedAssets();
    //
    //引用超过系统认为安全的内存占用，就会闪退
    //
    //


    #region Lua
    //lua
    //语言执行方式
    //  编译型语言:  
    //      代码在运行前要使用编译器，先将程序源代码编译为可执行文件
    //      再执行
    //      C/C++
    //      Java  C#
    //      Go   Objective-C
    //
    //  解释型语言(脚本语言):    
    //      需要提前安装语言解析器，运行时使用解析器运行程序源代码
    //      JavaScript(TypeScript)
    //      Python PHP Perl Ruby
    //      SQL
    //      Lua
    //      特点：运行才能调试，速度更慢
    //
    //
    //  编程方法
    //      交互式编程
    //      脚本式编程（将脚本的路径，传递给编译器）

    //  --  lua单行注释
    //  --[[
    //
    //  lua 多行注释
    //
    //  ]]
    //
    //lua会自动推导变量类型
    //name = "xxx"
    //--Lua会自动推导变量类型
    /*
    
    1   HelloLua
    --Lua可以不写分号，也能编写分号

    Name = "johnwest"

    --调用变量
    --C# Debug.log()  Console.WriteLine
    print(Name)

    --内置变量
    --看lua的版本
    print(_VERSION)--内置的系统变量

    --可以调用一个不存在的变量
    --值式nil，等同于null







    2   variable
    print(id)

    --销毁一个已经定义的变量
    Name = nil
    print(Name)

    --全局变量和局部变量
    --Name = "johnwest"定义了一个全局变量
    local LName = "V" --定义了一个局部变量，当前这个只可在当前文件使用
    print(LName)
    --一般开发中，轻易不会使用全局变量，因为全局变量可以被随意的修改
    --不安全不稳定
     





    3   DataType
    local name="jw"

    --获得变量的类型使用type()
    --type()是一个函数结构,是一个代码片段，可以重复调用执行
    --有参数有返回值
    print(type(name))
    
    --获得type()返回值类型
    --type()返回值类型是string
    print(type(type(name)))
    
    --对一个没有定义的变量获取类型
    print(type(bb))
    
    --判断一个未定义的变量
    print(type(bb) == "nil")
    
    --数字类型的数值
    --C#中多种数字类型，被归纳到number数据类型下
    print(type(123))
    print(type(123.53))
    --布尔类型的数值
    --布尔还是true和false
    print(type(true))
    --单引号字符串是允许的
    print(type('c'))
    print(type('johnwest'))

    --lua的表







    4   String
    --单行定义字符串
    local str1 = "abcdef"
    local str2 = 'ghi'
    
    --多行定义字符串
    local str3 =[[
      *
     ***
    *****]]
    
    print(str3)
    
    --C#字符串拼接
    --str1 + str2
    --lua字符串相加是..
    print(str1 .. str2)--lua
    --print(str1 + str2) X
    
    --但这样可以
    --C#写多了要注意
    print("1" + "2")
    --lua对两个字符串相加时，会将他们转换为数字类型
    --转换失败会报错，如果转换成功
    --则进行加运算后，得到数字结果
    
    --获取字符串长度
    print(#str1)
    
    
    
    --字符全部大写化
    --C# string.ToUpper()
    --   string.ToLower() (小写化)
    
    --lua字符串相关函数
    --string.函数名()
    
    --字符串大写化
    print(string.upper(str1))
    print(str1)
    --字符串小写化
    print(string.lower(str1))
    
    --lua支持多返回值
    --*字符串的下标是从1开始的*
    
    --字符串查找
    --参数1 被查找的字符串
    --参数2 查找的内容
    --返回值 起始的下标位置和结束找到的下标位置
    --*lua的下标是从1开始*
    print(string.find(str1,"cde"))
    
    --字符串反转
    print(string.reverse(str1))
    
    --截取字符串
    --方式1（提供起始位置，截取到结尾）
    print(string.sub("abcdefg",3))
    
    --方式2（提供其实位置，提供结束位置下标）
    print(string.sub("abcdefg",3,6))
    
    print(string.sub("abcdefg", 3, #"abcdefg" - 1))
    
    --字符串格式化
    --参数一：需要被格式化的字符串，其中包含占位符，%d表示数字
    --参数二：填入格式化字符串中的内容
    print(string.format("Im the %d player, the other is %d",1,3))
    
    --字符串重复
    print(string.rep("abc",3))
    
    --ASCI码转字符
    print(string.char(65,66,67))
    --字符转ASCI码
    print(string.byte("abc",1,#"abc"))
    
    --字符串替换
    --参数一 原始字符串
    --参数二 需要替换的内容
    --参数三 替换后的结果
    --返回值
    --返回值一 替换后结果
    --返回值二 一共替换了几次
    print(string.gsub("abcde","cd","**"))







    5   array
    --数组初始化
    --C# string[] data = new string[20];
    
    --lua其实不存在数组概念
    --lua一切皆表
    local data = {}
    
    -- 起始索引是1开始
    -- 类型可以混合
    -- 索引值可以为负数
    -- 即使索引从1开始，也可以赋值0索引
    -- 索引可以断开
    -- 初始化时，对于没有索引的值，索引是从1开始向上累加的
    -- 初始化提供索引的赋值方法，[索引值] = 数值
    data = {"abc", 1234, [-1] = 100, [0] = 50, [4] = 233, [3] = 666}
    
    print(type(data))
    print(data) --打印的内存地址
    
    print(data[1])
    print(data[2])
    print(data[4])
    
    -- 获取长度
    -- 只是获取从1开始,索引连续的数据个数
    -- 中间断开，计数结束
    -- * 这种方式不稳定 *
    print(#data)
    
    --修改某一个值
    data[1] = "defg"
    print(data[1])
    
    local date2 = {{"aa", "nmsl", "fnmdp"}, {123, 456}}
    print(date2[2][2])
    
    --数组在Lua中是用Table实现的




    6   operator
    -- 2的三次方
    print(2 ^ 3)
    
    -- C# 不等于 !=
    --lua
    print(2 ~= 3)
    
    -- lua没有 ++ --
    local i = 1
    i = i + 1
    
    -- 也没有 += -= *=





    7   if
    --[[
    C#

    if(条件)
    {
        条件达成的语句块
    }
    ]]

    local con1 = true
    
    if (con1) 
    then
        print("con1 为 true")
    end
    
    --[[
        C#
    
        if(条件)
        {
            条件达成的语句块
        }
        else
        {
    
        }
    ]]
    
    if false  
    then
        print(true)
    else
        print(false)
    end
    
    --[[
        C#
    
        if(条件)
        {
            条件达成的语句块
        }
        else if
        {
    
        }
        else
        {
    
        }
    ]]
    
    --嵌套和elseif
    if con1 then
        print("con1 达成")
        if true then
            print("嵌套")
        end
    elseif con1 == false then
        print("con1 没有达成")
    else
        print("error")
    end
    
    --lua没有switch






    8   while
    local num = 0

    --循环
    while num < 3 do
        --local修饰的num 虽然是局部变量，但作用域为当前文件
        --所以可以在循环体内，取得值
        print(num)
        num = num + 1
    end






    9   repeat
    --[[
    C# do while
    
    do
    {

    }
    while(条件)

    至少执行一次do结构体内的代码，
    while条件满足时，再do执行代码
    ]]
    
    local num = 1
    
    repeat
        print(num)
        num = num + 1
    until num > 5--直到条件满足时，跳出循环
    
    
    --[[
        do
        {
            if()
            {
                break;
            }
            if()
            {
    
            }
        }while(true)
    ]]
    
    repeat
        if true then
            print("跳出")
            break
        end
        
        if true then
            print("第二次跳出")
            break
        end
    until true
    
    print("继续执行")






    10  for
    local date = {"aa", "bb", "abc", "dd", "ee"}

    -- 参数1：变量i的初始值，遍历Lua表，使用1
    -- 参数2：增长到多少
    -- 参数3：增量大小，增长步长，默认是1
    for i = 1, #date, 1 do
        print(date[i])
    end
    
    -- 倒叙遍历
    for i = #date, 1, -1 do
        print(date[i])
    end
    
    -- * lua没有continue *







    11  iterator
    --迭代器（遍历table）
    --one是不加中括号的字符串索引
    --"aa" "bb" 自动加1，2索引
    --[4]指定数字索引
    --[-1]指定负数索引
    --["two"]是加中括号的字符串索引写法
    
    local date = { one = "c", "a", "bb", [3] = 3, [-1] = 4, ["two"] = "dd"}
    
    
    -- 连续索引数值迭代器
    --迭代器就是指向table的指针，连续数字索引迭代器，只会获取从1开始的数字索引
    --，且必须索引是连续的才能持续获得值
    
    for index, value in ipairs(date) do
        print("index:"..index..",value:"..value)
    end
    
    -- 索引数值迭代器
    -- 可以通过迭代计数获取table长度的最稳定的方法
    -- 就是使用所有数值迭代器获取
    print("------------------------------")
    
    for index, value in pairs(date) do
        print("index:"..index..",value:"..value)
    end





    12  function
    
    --第一种声明函数
    local function func1()
        print("这是func1")
    end
    
    -- 脚本语言只能从上往下执行
    -- 脚本型语言不能先调用再定义
    
    func1()
    
    local func2 = function ()
        print("这是function2")
    end
    
    func2()
    
    local func3 = function (a, b)
        print(a + b)
    end
    
    --函数的调用，参数可以多于形参，但是不能少于形参
    func3(5, 7)
    func3(5, 7, 9)
    
    local func4 = function (...)
        --将无固定参数，转换为table
        --arg的作用域是func4函数体
        local arg = {...}
        local total = 0;
    
        for index, value in pairs(arg) do
            total = total + value
        end
    
        print(total)
    end
    
    func4(1, 2, 3, 4)
    func4(1, 2, 3, 4, 5, 6)
    
    
    --多返回值
    
    local function func5()
        return 99, 100
    end
    
    print(func5())
    --多返回值，同时赋值给两个变量
    local num1, num2 = func5()
    print(num1 .. "," .. num2)








    13  table
    --table支持数字索引存储数据
    --table支持字符串索引（关联索引）存储数据
    
    local data = { one = "c", "a", "bb", [3] = 3, [-1] = 4, ["two"] = "dd"}
    
    print(data[2])
    print(data["one"])
    print(data.two)
    
    -- 因为函数在lua里面是一种数据类型，所以将func1索引下定义了一个函数
    data.func1 = function ()
        print("这是function1")
    end
    
    -- 所以data.func1调用数据，数据是函数
    --也就调用了函数
    data.func1();
    
    
    ----------------------------------------------
    --调用自己的第一种方式
    data.func2 = function ()
        print("function2:" .. data.two)
    end
    
    data.func2()
    --别这么用，名字一改就用不了了
    
    ----------------------------------------------
    --调用自己的第二种方式
    
    --第一种self调用方法
    --成员函数定义时，显示加入self变量，对应C#的this关键字
    --函数内部可以通过self变量获取当前table的其他值或者函数
    data.func3 = function (self)
        print("function3:" .. self.two)
    end
    
    --调用时必须使用":"，因为这样调用才可以对self关键字赋值
    data:func3()
    
    -------------------------------------------------
    
    --第二种self调用方法
    --隐式给self赋值
    function data:func4()
        print("function4:" .. self[1])
    end
    
    data:func4()









    14  require
    local config = require "subfile.config"
    -- 实现代码的切分
    
    --将提供的lua文件中的代码执行一遍
    --文件名包含"."会导致加载错误(除了下划线特殊符号都会错误)
    --文件扩展名".lua"不需要编写，因为会自动添加
    require("./subfile/subfile1")
    
    --预加载目录
    print(package.path)
    
    --表会记录已加载的文件记录
    print(package.loaded["./subfile/subfile1"])
    
    --每次加载文件时，都会检查package.loaded里面的内容防止重复加载
    --存储的是"*加载时*"用的路径
    --如果向重复多次加载一个文件，则需要再次加载前清除这个状态
    package.loaded["./subfile/subfile1"] = nil
    
    require("./subfile/subfile1")
    
    
    --------------------------------------------------
    --加载文件的相对路径
    --"./"表示当前编写的lua文件，所在的目录
    --"../"表示当前编写的lua文件，所在的上级目录
    --"../../"表示上级的上级目录
    
    
    --获取另一个文件的局部变量
    --子文件的return会返回给主文件变量
    local config = require("./subfile/config")
    require("./02variable")
    
    print(config.name)
    print(GameName)
    print(Name)









    15  matetable
    --https://www.runoob.com/lua/lua-metatables.html

    local t1 = {1, 2, 3}
    local t2 = {2, 3, 4}
    
    --直接打印表，显示的是内存地址
    --期望打印table时，时显示"{1, 2, 3}"
    print(t1)
    
    --打印时，把表作为字符串输出
    --实现的功能，当需要将表作为字符串使用时，应该有一种办法
    --这种办法，lua提供了这种语法特性，matatable扩展，元素扩展
    
    
    --参数1：需要进行元素扩展的数据表
    --参数2：原表扩展表（拥有扩展表的原表）
    --      只要在原表中实现一些特殊的函数，则t1就可以实现一些特殊功能
    --      比如让表可以作为字符串使用
    setmetatable(
        t1,
        {
            __tostring = function (self) -- 元方法（该方法会在被当成string方式调用时调用）
                local str = ""
                for index, value in ipairs(self) do
                    str = str .. value .. " "
                end
    
                return str
            end,
    
            __add = function (mytable,newtable)
                local mylenth = #mytable
    
                for i = 1, #newtable, 1 do
                    table.insert(mytable, mylenth + i, newtable[i])
                end
    
                return mytable
            end
        }
    )
    
    
    -- t1会被传给t
    print(t1)
    
    print(t1+t2)
    
    
    ----------------------------------------------------------
    --排序方法
    local sort = function (...)
        local arg = {...}
    
        for i = 1, #arg - 1, 1 do
            for j = 1, #arg - 1, 1 do
                if arg[j] > arg[j + 1] then
                    local temp = arg[j]
                    arg[j] = arg[j + 1]
                    arg[j + 1] = temp
                end
            end
        end
    
        return arg
    end
    
    
    local arg = sort(15, 5, 35, 45 ,2 ,3 ,8 ,6, 20)
    
    for index, value in ipairs(arg) do
        print(index .. ": " .. value)
    end





            tips 上下方法有用




    tips
    ---------------------------------------------
    --获得准确长度的方法
    local length = function (mytable)
        local count = 0
    
        for key, value in pairs(mytable) do
            count = count + 1
        end
    
        return count
    end
    
    print(length(data))
    --方法也占长度







    */

    #endregion

    /*
     * 常见的Unity热更新插件
		sLua：最快的Lua插件
		toLua：由uLua发展而来的，第三代Lua热更新方案
		xLua：特性最先进的Lua插件
		ILRuntime：纯C#实现的热更新插件


    根据加载提示，发现可以将Lua文件放在StreamingAssets目录下

    StreamingAssets被抛弃，所以移动到Resources下（以txt为结尾）

     */



    /*
     public void Loader()
    {
        LuaEnv luaEnv = new LuaEnv();//生成新的环境
        luaEnv.AddLoader(ProjectLoader);//添加新的加载器

        luaEnv.DoString("require('test')");//执行require函数

        luaEnv.Dispose();//释放
        
    }

    系统加载器，会在require()命令调用时，加载一些特定目录下的Lua文件
    Resource目录下
    StreamingAssets目录下

    //自定义加载器
    //自定义加载器会先于系统内置加载器执行，当自定义加载器加载到文件后，后续的加载器则不会继续执行
    //当Lua代码执行require()函数是，自定义加载器会尝试获得文件的内容
    //参数：被加载lua文件的路径
    //如果加载的文件不存在，记得返回null，这样文件才能继续执行下去
    
    public byte[] ProjectLoader(ref string filepath)
    {
        //filepath是来自于Lua的require("文件名")
        //构造路径，才能将require加载的文件指向到我们想放Lua的路径下去
        //路径可以任意定制（能不能将Lua代码放入AB包）
        //因为Application.dataPath在打包之后不可用，所以上线的项目不能用 dataPath

        string path = Application.persistentDataPath + "/" + filepath + ".lua";

        if (File.Exists(path))//如果文件存在
        {
            //将Lua文件读取为字节数组
            //xlua的解析环境，会执行我们自定义加载器返回的lua代码
            return File.ReadAllBytes(path);
        }

        //文件不存在返回空
        return null;
    }


    接触一个新的Lua项目时，先要弄懂Lua的加载器规则，只有这样，才能弄懂项目
	的Lua执行流程





    静态类，静态属性用点 .
    实例用 :

    静态类

    -- Lua调用静态类
    -- 规则"CS.命名空间.类名.成员变量"

    print(CS.LearnTest.TestStatic.ID)

    -- 给静态属性赋值
    CS.LearnTest.TestStatic.Name = 'admin'
    print(CS.LearnTest.TestStatic.Name)
    
    -- 静态成员方法调用
    -- 规则"CS.命名空间.类名.方法名()"
    print(CS.LearnTest.TestStatic.Output())
    
    
    -- 使用默认值调用方法
    CS.LearnTest.TestStatic.Default()
    -- 使用Lua传递的值调用方法
    CS.LearnTest.TestStatic.Default("abc123")



    实例化类

    -- lua实例化类
    -- C# LuaTest = new LuaTest
    -- 一样是通过构造还是创建对象
    local luatest = CS.LuaTest()
    luatest.Hp = 100
    luatest.name = "abc"
    
    print(luatest.Hp .. "  " .. luatest.name)
    
    local luatest1 = CS.LuaTest("admin")
    print(luatest1.name)
    
    -- 表方法希望调用表成员变量(表:函数())
    -- 对象引用成员变量时，会隐性调用this，等同于lua中的self
    print("Output:" .. luatest1:Output())
    -- *要用冒号！！*
    
    一定要用冒号:

    加组件   typeof可以直接用
    local obj = CS.UnityEngine.GameObject("LuaCreatObj")
    obj:AddComponent(typeof(CS.UnityEngine.BoxCollider))





    Lua调C#结构体
    --和对象调用一样
    local struct = CS.LuaStructTest()
    
    struct.name = "admin"
    
    print("LuaStructTest:  " .. struct:Output())






    枚举类型

    -- C# TestEnum.
    -- CS.命名空间.枚举名.枚举值
    local engin = CS.LuaEnumTest.None
    print(engin)
    -- 转换获得枚举值
    -- CS.命名空间.枚举名.__CastFrom(数字，或者字符串)
    
    print(CS.LuaEnumTest.__CastFrom("超空间引擎"))
    print(CS.LuaEnumTest.__CastFrom(2))
    print("type: " .. type(CS.LuaEnumTest.__CastFrom(3)))





    C#重载
    --lua支持重载，但是float和int之类的都是字符类型
    --所以会出问题
    --会有一个用不了
    CS.LuaCallOverload.Test(150)
    CS.LuaCallOverload.Test("admin")
    CS.LuaCallOverload.Test(15,"admin15")
    CS.LuaCallOverload.Test(0.6)




    继承重写  也支持  直接用
    --调用父类构造函数来生成类

    local fa = CS.LuaCallFathe()
    print(fa.Name)
    fa:Talk()
    fa:Move()
    
    local child = CS.LuaCallChild()
    print(child.Name)
    child:Talk()
    child:Move()




    扩展方法
    //类扩展需要给扩展方法编写的静态类添加[LuaCallCSharp]，否则lua无法调用到
    [LuaCallCSharp]
    public static class MyExtent
    {
        public static void ShowSomething(this LuaCallExtend obj)
        {
            Debug.Log("类扩展");
        }
    }


    local lua = CS.LuaCallExtend()

    lua:Output()
    lua:ShowSomething()

    注意，类扩展出来的不是静态方法是需要实例的方法 
    等于是带了一个self






    委托

    -- C#委托赋值
    -- LuaDelegate.Static = LuaDelegate.action
    -- LuaDelegate.Static += LuaDelegate.action
    -- LuaDelegate.Static -= LuaDelegate.action
    -- LuaDelegate.Static()
    
    
    -- 静态
    CS.LuaDelegate.staticDel = CS.LuaDelegate.action
    CS.LuaDelegate.staticDel()
    -- Lua中如果添加了函数到静态委托变量中，再委托不再使用后记得释放添加的委托
    CS.LuaDelegate.staticDel = nil
    
    
    local func = function ()
        print("这是lua的函数")
    end
    
    -- 覆盖添加委托
    CS.LuaDelegate.staticDel = func
    -- 加减操作前一定要确定已经添加过回调函数 Lua有必要 C#不需要
    -- 加和减之前也要判空
    if CS.LuaDelegate.staticDel == nil then
        CS.LuaDelegate.staticDel = CS.LuaDelegate.action
    else
        CS.LuaDelegate.staticDel = CS.LuaDelegate.staticDel + CS.LuaDelegate.action 
    end
    
    
    --调用前判定
    if CS.LuaDelegate.staticDel ~= nil then
        CS.LuaDelegate.staticDel()
    end
    
    
    CS.LuaDelegate.staticDel = CS.LuaDelegate.staticDel - CS.LuaDelegate.action
    --调用前判定
    if CS.LuaDelegate.staticDel ~= nil then
        CS.LuaDelegate.staticDel()
    end
    
    CS.LuaDelegate.staticDel = nil
    
    
    ------------------------------------------------------
    -- 动态
    -- 动态可以结束是不清空，但保险起见还是清空
    local delclass = CS.LuaDelegate()
    delclass.dynamic = func
    delclass.dynamic()
    
    delclass.dynamic = nil




    写lua时注意大小写
    event

    -- C#添加事件
    -- TestEvent.staticEvent += TestEvent.action
    local func = function ()
        print("来自lua")
    end
    
    --lua添加事件
    CS.LuaEvent.staticEvent("+", CS.LuaEvent.StaticAction)
    CS.LuaEvent.CallStatic()
    CS.LuaEvent.staticEvent("-", CS.LuaEvent.StaticAction)
    
    
    local obj = CS.LuaEvent()
    obj:dynamicEvent("+", func)
    obj:CallDynamic()
    obj:dynamicEvent("-", func)
    obj:dynamicEvent("+", CS.LuaEvent.StaticAction)
    obj:CallDynamic()
    obj:dynamicEvent("-", CS.LuaEvent.StaticAction)




    泛型
    local generic = CS.TestGenericType()
    --lua没有泛型，只能外套一层重载来区分类型
    generic:Output(2333)
    generic:Output("admin15")
    
    local go = CS.UnityEngine.GameObject("LuaCreatObj")
    -- xlua实现了typeof关键字，所以可以用类型API替代Unity内置的泛型方法
    go:AddComponent(typeof(CS.UnityEngine.BoxCollider))
    */






    //为什么要C#调用Lua
    /*
    Unity是C#语言开发，所以所有的生命周期函数都是基于C#实现
    xlua本身是不存在Unity的生命周期函数的
    如果希望xlua能有声明周期函数，那么我们可以实现C#作为Unity的原始调用
    再使用C#调用lua



    C#全局调用
    object[] data = xluaEvn.Instance.DoString("return require('L2C/CsharpCallVariable')");
    //Debug.Log(data[0]);

    //LuaEnv提供了一个成员变量Global，它可以用于获取Lua的全局变量
    //Global的数据类型是C#实现的LuaTable，LuaTable是xlua实现C#和Lua表对应的数据结构
    //xlua会将lua中的全局变量以table的方式全部存储再global中

    //通过运行环境，导出全局变量，类型是LuaTable
    //LuaTable是C#的数据对象，用于和Lua中全局变量和存储的table对应
    LuaTable global = xluaEvn.Instance.Global;

    //从lua中将全局变量提取出来
    //参数：提供全局变量的名称
    //泛型：lua中全局变量的类型
    //返回值：变量的值
    int Num = global.Get<int>("Num");
    float Float = global.Get<float>("Float");
    bool IsStudent = global.Get<bool>("IsStudent");
    string Name = global.Get<string>("Name");

    Debug.Log("Num:" + Num);
    Debug.Log("Float:" + Float);
    Debug.Log("IsStudent:" + IsStudent);
    Debug.Log("Name:" + Name);


    Lua：
    -- 隐性的做了{Num = 100, Float = 3.14, IsStudent = true, Name = "admin15"}

    Num = 100
    Float = 3.14
    IsStudent = true
    Name = "admin15"
    
    return 1478





    C#调用全局方法
    public delegate void func1();
    public delegate void func2(string name);
    public delegate string func3();

    （多返回值记得加）
    [CSharpCallLua]//映射产生时，xlua提示添加的
    public delegate void func4(int num, out string str, out int outint);

    // Start is called before the first frame update
    void Start()
    {
        xluaEvn.Instance.DoString("require('L2C/CsharpCallFunction')");

        LuaTable table = xluaEvn.Instance.Global;
        //lua的函数，会导出为C#的委托类型
        table.Get<func1>("func1")();

        //向lua函数传递参数
        table.Get<func2>("func2")("admin");

        //接受lua函数的返回值
        Debug.Log(table.Get<func3>("func3")());

        //lua多返回值
        int outint;
        string str;
        table.Get<func4>("func4")(1478, out str, out outint);
        Debug.Log(str + " " + outint);
    }


    lua 方法
    func1 = function ()
    print("这是lua的func1")
    end
    
    func2 = function (name)
        print("这是lua中的func2，参数是：" .. name)
    end
    
    func3 = function ()
        return "这是lua中的func3"
    end
    
    func4 = function (num)
        return "这是func4返回两个参", num
    end



    C#调用Tabel
    //获得是全局变量core，因为它是在Lua中是表，所以取出来的是Luatable
    LuaTable table = xluaEvn.Instance.Global;
    LuaTable core = table.Get<LuaTable>("core");
    //获取name
    //参数：table中的索引名
    //类型：索引对应值类型
    Debug.Log(core.Get<string>("name"));
    Debug.Log(core.Get<bool>("IsStudent"));

    core.Set<string, bool>("IsStudent", true);

    Debug.Log(core.Get<bool>("IsStudent"));

    core.Get<func1>("func1")("aa");

    core.Get<PrintName>("PrintName")(core);




    https://shenjun-coder.github.io/LuaBook/
    https://shenjun7792.github.io/




    用结构体映射
    //Lua的table映射过来，可以实现C#运行无GC
    [GCOptimize]
    public struct LuaCore
    {
        public delegate void Func1(string name);
        [CSharpCallLua]
        public delegate void printName(LuaCore core);
    
        public int ID;
        public string name;
        public bool IsStudent;
    
        public Func1 func1;
        public printName PrintName;
    }


    //将luatable导出为core
    LuaCore core = xluaEvn.Instance.Global.Get<LuaCore>("core");

    core.func1("johnwest");

    Debug.Log(core.name);
    core.PrintName(core);
    */










    //W4 GPU
    /*
    
    //从硬盘加载
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Capsule");
    //将数据放置到内存中
    GameObject capsule = Instantiate<GameObject>(prefab);
    //物体上面带有材质球，材质球会引用Shader着色器，因为Shader是GPU编程，所以就会将数据传输给GPU
    注意，Unity采用的是材质球调用Shader，所以Unity是通过材质球将数据传递给GPU


     
     
    */

    /*
     *  套接字
     *      TCP用主机的IP地址加上主机上的端口号作为TCP连接的端点，这种端点就叫做套接字（socket）或插口
     *      套接字用（IP地址：端口号）表示
     *      
     *      IP协议实现主机的网络定位
     *      操作系统的端口实现数据的流入与流出
     *      
     *      套接字就是将IP地址与主机的端口号合并在一起后的数据，IP地址定位主机的位置
     *      端口号知道通讯的入口与出口，从而就可以实现主机的数据交换
     *      
     *  //using System.Net.Sockets;
     *  //网络类型 基于数据流方式 基于TCP协议
     *  Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
     *  
     *  
     *  TCP编程方法
     *  
     *  字节长度关系
     *      日常所说的G，M T指的是数据容量
     *      一个int 4字节 32位
     *      UTF-8编码 长度从1字节 到 6字节 其中中文是3个字节
     *      B:  一个字节（Byte）
     *      KB: 1024字节
     *      MB: 1024KB
     *      GB: 1024MB
     *      TB: 1024GB
     *      PB: 1024TB
     *      
     *  连接  （三次握手）
     *      创建套接字 
     *      Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
     *      调用连接方法
     *      异步
     *      socket.BeginConnect(Host, Port, _EndConnect, null);
     *      //异步结束的时候把异步操作挂起
     *      socket.EndConnect(asyncResult);
     *
     *  断开  （四次挥手）
     *      同步
     *      //下次使用，会创建全新的套接字
     *      socket.Disconnect(false);
     *      //关闭套接字连接，释放资源
     *      socket.Close();
     *      异步
     *      
     *  监听绑定    （服务器开发）
     *      Bind() 函数实现
     *  接收
     *  发送
     *  
     *  
     *  数据分包
     *      当接收到数据后，需要将每一个定制的数据包格式分离出来，所写的代码就是
     *      分包代码，有事是硬件将数据包拼接在一起，有时是代码将数据包拼接在一起
     *      拼接后的代码发送效率更高
     *  数据粘包
     *      发送数据前，如果有多个数据包需要一起发送，则可以将数据包，拼接在一起再发送
     *      这样涮熟效率更高
     *  数据打包
     *      对原始数据，添加协议头的过程，就是数据打包的过程
     *      （前四个字节记录长度，拼接上包体，成为一个数据包）
     *  数据解包
     *      接收到数据包时，读取包头，并根据包头记录信息，获取到包内的原始数据的过程
     *      就是解包
     *      
     *  数据包生命周期
     *      1 对原始数据打包
     *      2 对多个数据包粘包
     *      3 套接字（连接，发送）
     *      4 套接字（接收）
     *      5 有可能同时接到多个数据黏再一起，分包
     *      6 取得单个数据包，解包
     *      7 根据数据包，执行代码逻辑
     *      
     */

    private string path;
    public string Path
    {
        get
        {

            if (path == null || path == "")
            {
#if UNITY_STANDALONE_WIN//宏判断平台

                //只有windows下这个可读可写
                path = Application.streamingAssetsPath;

#elif UNITY_ANDROID || UNITY_IOS
                path = Application.persistentDataPath;
#endif
            }

            return path;
        }
    }

    //Sytem.IO下
    //Directory.Exists判断文件夹存不存在
    //DirectoryInfo CreateDirectory(string path); 生成文件夹

    private void OnDestroy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        //设置该物体的父物体
        transform.parent = target.transform;
        transform.SetParent(target.transform);

        //查找子物体
        //但是不能找子物体的子物体
        for (int i = 0; i < transform.childCount; i++)
        {
            target = transform.GetChild(i).gameObject;

        }

        //通过名字查找
        //也不能找子物体的子物体
        target = transform.Find("Cube (2)").gameObject;
        if (target)
        {
            Debug.Log("target:" + target.name);
        }

        //能根据游戏物体名字在所有物体中查找，好用但是效率很低，谨慎使用
        GameObject.Find("name");

        //当有多个tag是同样的时候，查找到的是最后创建的tag是NPC的游戏物体
        //查找物体tag是NPC
        GameObject instance = GameObject.FindGameObjectWithTag("NPC");
        if (instance)
        {
            Debug.Log("instance:" + instance.name);
        }

        //当场景中有多个物体的tag是NPC，同时查找到，并返回
        GameObject[] instancs = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < instancs.Length; i++)
        {
            Debug.Log("name" + i + instancs[i].name);
        }
    }

    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //以世界坐标系为参考系进行旋转
        transform.Rotate(Vector3.up, Space.World);
        //以自身为坐标系来进行旋转
        transform.Rotate(new Vector3(1, 0, 0), Space.Self);

        //以target的坐标系来移动
        transform.Translate(new Vector3(0.001f, 0, 0), target.transform);

        //看向某个位置
        transform.LookAt(transform.position, target.transform.position);
        //在两个点之间画一条线
        Debug.DrawLine(transform.position, target.transform.position);

        //gameObject.SetActive(false);
        //gameObject.SetActive(true);

        //RaycastHit


    }

    //递归查找
    Transform FindChild(string childName, Transform parent)
    {
        Transform obj = null;
        if (childName == parent.name) return parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            obj = FindChild(childName, parent.GetChild(i));
            if (obj != null)
            {
                return obj;
            }
        }

        return obj;
    }



}
