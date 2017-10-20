using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppFile
{
    //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
    public static readonly string PathURL =
#if UNITY_IPHONE
    Application.streamingAssetsPath + "/";
#elif UNITY_EDITOR
    //Application.dataPath + "/StreamingAssets/";
    Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID   //安卓
	"jar:file://" + Application.dataPath + "!/assets/";
#else
    Application.streamingAssetsPath + "/";
#endif

    public static readonly string PersistentPathUrl =
#if UNITY_EDITOR_OSX
    Application.persistentDataPath;
#elif UNITY_IOS
    Application.persistentDataPath + "/";
#elif UNITY_ANDROID
    Application.persistentDataPath + "/";
#else
    Application.dataPath + "/cache/";
#endif

    public static string GetStreamingAssetsFullPath(string subPath)
    {
        return PathURL + subPath;
    }

    public static string GetPersistentFullpath(string subPath)
    {
        return PersistentPathUrl + subPath;
    }

    public static string LoadFile2Str(string fullPath)
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(fullPath);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return string.Empty;
        }
        string str = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        return str;
    }

    public static ArrayList LoadFile2Array(string subPath, string name)
    {
        StreamReader sr = null;
        string path = PathURL + subPath;
        try
        {
            sr = File.OpenText(path + name);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            arrlist.Add(line);
        }
        sr.Close();
        sr.Dispose();
        return arrlist;
    }

    public static void CreateFolder(string folderPath)
    {
        if (folderPath.Equals(PathURL) || folderPath.Equals(PersistentPathUrl)) return;
        if (File.Exists(folderPath)) return;
        Directory.CreateDirectory(folderPath);
    }
    
    public static void SavePersistentFile(string path, string info, bool isCover=true)
    {
        try
        {
            CreateFolder(path.Substring(0, path.LastIndexOf('/')));
            StreamWriter sw;
            FileInfo t = new FileInfo(path);
            if(isCover || !t.Exists)
            {
                sw = t.CreateText();
            }
            else
            {
                sw = t.AppendText();
            }
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static long GetDirectoryLength(string dirPath)
    {
        //判断给定的路径是否存在,如果不存在则退出
        if (!Directory.Exists(dirPath))
            return 0;
        long len = 0;

        //定义一个DirectoryInfo对象
        DirectoryInfo di = new DirectoryInfo(dirPath);

        //通过GetFiles方法,获取di目录中的所有文件的大小
        foreach (FileInfo fi in di.GetFiles())
        {
            len += fi.Length;
        }
        //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
        DirectoryInfo[] dis = di.GetDirectories();
        if (dis.Length > 0)
        {
            for (int i = 0; i < dis.Length; i++)
            {
                len += GetDirectoryLength(dis[i].FullName);
            }
        }
        return len;
    }

    public static bool ClearDirectory(string dirPath, string[] excepts=null)
    {
        if (!Directory.Exists(dirPath)) return false;
        DirectoryInfo dir = new DirectoryInfo(dirPath);
        FileInfo[] files = dir.GetFiles();
        try
        {
            foreach (var item in files)
            {
                File.Delete(item.FullName);
            }
            if (dir.GetDirectories().Length != 0)
            {
                foreach (var item in dir.GetDirectories())
                {
                    if (!item.ToString().Contains("$") && (!item.ToString().Contains("Boot")))
                    {
                        if (!ClearDirectory(dir.ToString() + "\\" + item.ToString(), excepts)) return false;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
        return true;
    }

}
