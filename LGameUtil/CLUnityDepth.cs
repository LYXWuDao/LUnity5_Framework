using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CLUnityDepth : MonoBehaviour
{
    public int depth = 0;

    void Awake()
    {
        if (Application.isPlaying) enabled = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.SetSiblingIndex(depth);
    }

}
