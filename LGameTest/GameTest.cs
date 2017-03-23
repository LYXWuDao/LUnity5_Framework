using LUnity.Game.Util;
using UnityEngine;

public class GameTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        SLUnityLog.ListenerLogStack();

        for (int i = 0, len = 10; i < len; i++)
            SLUnityLog.Log("this is log");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
