using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension 
{
    #region GetOrAddComponent
    static public T GetOrAddComponent<T>(this GameObject go, string path = "") where T : Component
    {
        Transform t;
        if (string.IsNullOrEmpty(path))
            t = go.transform;
        else
            t = go.transform.Find(path);

        if (null == t)
        {
            //Foundation.LogUtility.LogError("GetOrAddComponent not Find GameObject at Path: " + path);
            Debug.LogError($"GetOrAddComponent not Find GameObject at Path:{path}");
            return null;
        }

        T ret = t.GetComponent<T>();
        if (null == ret)
            ret = t.gameObject.AddComponent<T>();
        return ret;
    }

    static public T GetOrAddComponent<T>(this Transform t, string path = "") where T : Component
    {
        return t.gameObject.GetOrAddComponent<T>(path);
    }

    static public T GetOrAddComponent<T>(this MonoBehaviour mono, string path = "") where T : Component
    {
        return mono.gameObject.GetOrAddComponent<T>(path);
    }

    #endregion
}
