using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using static EnemyCreator;
using static EnemyLogic;

public class EnemyLogic : MonoBehaviour
{
    [Tooltip("前进速度")]
    public float zSpeed = 10;

    private LuaEnv luaEnv;
    private LateralMove lateralMove;
    private SnakeMove snakeMove;
    private InvokeChange invokeChange;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'EnemyLogic'");
        lateralMove = luaEnv.Global.Get<LateralMove>("lateralMove");
        snakeMove = luaEnv.Global.Get<SnakeMove>("snakeMove");
        invokeChange = luaEnv.Global.Get<InvokeChange>("invokeChange");

        // 每秒改变一次横移速度
        invokeChange(this);
    }

    // Update is called once per frame
    void Update()
    {
        lateralMove(this, this.gameObject);
    }
    private void OnDestroy()
    {
        lateralMove = null;
        snakeMove = null;
        invokeChange = null;
        luaEnv.Dispose();
    }
    private void SMove()
    {
        snakeMove();
    }
    [CSharpCallLua]
    public delegate void LateralMove(EnemyLogic enemyLogic, GameObject gameObject);
    public delegate void SnakeMove();
    public delegate void InvokeChange(EnemyLogic enemyLogic);


}
