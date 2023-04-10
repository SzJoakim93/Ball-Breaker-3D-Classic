using UnityEngine;

public class AppInitialize : MonoBehaviour {

    void Start()
    {
        if (Global.AppStarted)
        {
            return;
        }

        AdManager.Initialize();

        Global.AppStarted = true;
    }
}