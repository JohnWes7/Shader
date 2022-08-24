using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;

public class TestSocket : MonoBehaviour
{
    Socket socket;
    public string Host; //域名
    public int Port;    //端口

    private byte[] _Buffer = new byte[1024 * 1024]; //缓冲容量是1MB
    public Queue<byte[]> ReceiveMessageQueue = new Queue<byte[]>();

    void Start()  
    {
        try
        {
            //using System.Net.Sockets;
            //网络类型 基于数据流方式 基于TCP协议
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //异步
            socket.BeginConnect(Host, Port, _EndConnect, null);
            //同步
            //socket.Connect("hxsd.tcp.honorzhao.com", 8282);

            Debug.Log("connect success");
        }
        catch (Exception)
        {
            Debug.Log("connect fail");
        }
        
    }

    /// <summary>
    /// 连接成功后回调
    /// </summary>
    /// <param name="asyncResult">连接的返回值</param>
    private void _EndConnect(IAsyncResult asyncResult)
    {
        //异步结束的时候把异步操作挂起
        socket.EndConnect(asyncResult);

        Debug.Log("[TCP]异步连接成功，等待执行连接回调函数");
    }

    /// <summary>
    /// 断开连接回调
    /// </summary>
    /// <param name="asyncResult">断开连接的返回值</param>
    private void _EndDisConnect(IAsyncResult asyncResult)
    {
        //结束异步断开
        socket.EndDisconnect(asyncResult);
        //关闭socket并释放所有资源
        socket.Close();
        //socket资源置空
        socket = null;

        Debug.Log("断开连接成功");
    }

    private void OnDestroy()
    {
        //DisConnect();
    }

    /// <summary>
    /// 同步断开连接
    /// </summary>
    private void DisConnect()
    {
        if (!socket.Connected)
        {
            return;
        }

        //下次使用，会创建全新的套接字
        socket.Disconnect(false);
        //关闭套接字连接，释放资源
        socket.Close();
    }

    /// <summary>
    /// 异步断开连接
    /// </summary>
    private void AsyncDisConnect()
    {
        if (socket == null)
        {
            return;
        }
        if (!socket.Connected)
        {
            return;
        }

        //异步开始断开连接
        socket.BeginDisconnect(false, _EndDisConnect, null);

        Debug.Log("发起断开连接");

    }

    /// <summary>
    /// 同步接收数据
    /// </summary>
    /// <returns>接收数据的长度0表示服务器断开</returns>
    private int Receive()
    {
        if (socket == null || !socket.Connected)
        {
            return 0;
        }

        //接收缓冲区
        //数据再网卡中的接收起始偏移量
        //当次接收的数据长度（缓冲区是1mb，所以接收数据的长度也是1MB）
        //不对接受数据的来源进行指定

        //返回值是从服务器接收到的数据的长度
        //代码会阻塞主线程，直到服务器给客户端发送了数据，接收的阻塞才会结束
        int length = socket.Receive(_Buffer, 0, _Buffer.Length, SocketFlags.None);

        return length;
    }

    /// <summary>
    /// 异步接收数据
    /// </summary>
    private void BeginReceive()
    {
        socket.BeginReceive(_Buffer, 0, _Buffer.Length, SocketFlags.None, EndReceive, null);
    }

    /// <summary>
    /// 异步数据接收回调
    /// </summary>
    /// <param name="asyncResult">异步接收起始返回值</param>
    private void EndReceive(IAsyncResult asyncResult)
    {
        //异步接收end 把线程挂起
        int length = socket.EndReceive(asyncResult);

        //服务器发起断开连接，会接收到0字节的数据
        if (length == 0)
        {
            Debug.Log("服务器断开连接，接收到0字节，关闭连接");

            //断开连接
            socket.Disconnect(false);
            socket.Close();
            socket = null;
        }
        //正常连接
        else
        {
            Debug.Log("正常接收");

            try
            {
                //分包
                //当前读取的起始下标
                int startIndex = 0;

                //还没有读取完接收到的所有数据
                while (startIndex < length)
                {
                    //获得单个包的大小
                    byte[] plBytes = new byte[4];
                    Array.Copy(_Buffer, startIndex, plBytes, 0, 4);
                    int packageLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(plBytes, 0));

                    //创建单个包的字节数组
                    byte[] package = new byte[packageLength];
                    //从缓存区中读取
                    Array.Copy(_Buffer, startIndex, package, 0, packageLength);

                    //向消息接收列队中追加消息
                    //主线程。分线程因为共享列队，所以需要加锁
                    lock (ReceiveMessageQueue)
                    {
                        //ReceiveMessageQueue.Enqueue(TcpPackage)
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

    /// <summary>
    /// 同步发送数据
    /// </summary>
    /// <param name="data">数据</param>
    private void Send(string data)
    {
        //字符串转字节数组
        //Encoding类，根据字符串的字符集，自动将其转换为字节数组
        byte[] dataByte = System.Text.Encoding.UTF8.GetBytes(data);

        //返回值是发送到网卡的数据字节长度
        //Send是阻塞的，在主线程中运行，如果数据过大游戏会假死
        int length = socket.Send(dataByte, 0, dataByte.Length, SocketFlags.None);


    }

    private void BeginSend(string data)
    {
        byte[] dataByte = System.Text.Encoding.UTF8.GetBytes(data);

        socket.BeginSend(dataByte, 0, dataByte.Length, SocketFlags.None, EndSend, null);
    }

    private void EndSend(IAsyncResult asyncResult)
    {
        
    }


}
