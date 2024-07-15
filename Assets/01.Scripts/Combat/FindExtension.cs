using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class FindExtension
{
    public static T[] FindInterfaces<T>()
	{
        var saveables = new List<T>();
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
            foreach (var root in rootObjs)
            {
                saveables.AddRange(root.GetComponentsInChildren<T>(true));
            }
        }

        return saveables.ToArray();
    }
}
