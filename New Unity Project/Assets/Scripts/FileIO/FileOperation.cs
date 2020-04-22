using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileOperation
{
    //public static string BaseAndroidPath = Path.Combine(Application.temporaryCachePath, UnityEngine.Application.productName);
    public static string BaseAndroidPath = Application.temporaryCachePath;
    public static string FileRead(string path)
    {
        string data = string.Empty;
        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            return string.Empty;
        }
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            data = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }
        catch (IOException e)
        {
            Debug.LogError("FileRead: " + e.Message);
        }
        return data;
    }
    public static void FileWrite(string path, string data)
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(data);
            sw.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
