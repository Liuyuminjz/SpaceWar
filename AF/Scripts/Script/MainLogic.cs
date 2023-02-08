using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using static MainLogic;

public class MainLogic : MonoBehaviour
{
    private LuaEnv luaEnv;
    private SetFrameRate setFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'Main'");
        setFrameRate = luaEnv.Global.Get<SetFrameRate>("setFrameRate");
        setFrameRate();

    }
    private void Update()
    {
        Debug.Log(Application.targetFrameRate);
    }
    
    [CSharpCallLua]
    public delegate void SetFrameRate();
}
