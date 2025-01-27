﻿using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

namespace uNvEncoder.Examples
{

public class OutputEncodedDataToFile : MonoBehaviour
{
    [SerializeField]
    string filePath = "test.h264";

    FileStream fileStream_;
    BinaryWriter binaryWriter_;

    void Start()
    {
        fileStream_ = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        binaryWriter_ = new BinaryWriter(fileStream_);
    }

    void OnApplicationQuit()
    {
        if (fileStream_ != null) 
        {
            fileStream_.Close();
        }

        if (binaryWriter_ != null) 
        {
            binaryWriter_.Close();
        }
    }

    public void OnEncoded(System.IntPtr ptr, int size)
    {
        var bytes = new byte[size];
        Marshal.Copy(ptr, bytes, 0, size);
        binaryWriter_.Write(bytes);
    }
}

}
