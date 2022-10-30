using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneData
{
    public static string previousScene = "Null";
    public static string currentScene = "Null";
    public static Dictionary<string, Transform> spacialPostions = new Dictionary<string, Transform>();
    // spacialPostions["Earth"] = GameObject.Find("Earth").transform;
    // spacialPostions.Add("Moon", GameObject.Find("Moon").transform);
    // spacialPostions.Add("Sun", GameObject.Find("Sun").transform);
    // spacialPostions.Add("Planet1", GameObject.Find("Planet1").transform);
    // spacialPostions.Add("Planet2", GameObject.Find("Planet2").transform);
    // spacialPostions.Add("Planet3", GameObject.Find("Planet3").transform);
    // spacialPostions.Add("Planet4", GameObject.Find("Planet4").transform);
    // spacialPostions.Add("Planet5", GameObject.Find("Planet5").transform);

}