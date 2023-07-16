using UnityEngine;

public class AppInitialize : MonoBehaviour {

    void Start()
    {
        if (Global.AppStarted)
        {
            return;
        }
    
        Global.AppStarted = true;
    }
}