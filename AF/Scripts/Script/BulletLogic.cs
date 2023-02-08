using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using Unity.VisualScripting;
using static BulletLogic;

public class BulletLogic : MonoBehaviour
{
    [Tooltip("子弹飞行速度")]
    public float speed = 30;
    [Tooltip("子弹生命时间")]
    public float lifetime = 7;

    private LuaEnv luaEnv;
    private InvokeDestroy invokeDestroy;
    private SelfDestroy selfDestroy;
    private BulletShot bulletShot;
    private TouchMonster touchMonster;

    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'BulletLogic'");
        invokeDestroy = luaEnv.Global.Get<InvokeDestroy>("invokeDestroy");
        selfDestroy = luaEnv.Global.Get<SelfDestroy>("selfDestroy");
        bulletShot = luaEnv.Global.Get<BulletShot>("bulletShot");
        touchMonster = luaEnv.Global.Get<TouchMonster>("touchMonster");
        invokeDestroy(this);

    }

    // Update is called once per frame
    void Update()
    {

        bulletShot(this.gameObject, this);
    }
    private void OnTriggerEnter(Collider other)
    {
        touchMonster(this.gameObject, other.gameObject);
    
    }
    private void OnDestroy()
    {
        invokeDestroy = null;
        selfDestroy = null;
        bulletShot = null;
        touchMonster = null;
        luaEnv.Dispose();
    //    UnityEngine.GameObject.
    }
 
    private void SDestroy()
    {
        this.selfDestroy(this.gameObject);
    }
    
    [CSharpCallLua]
    public delegate void InvokeDestroy(BulletLogic bulletLogic);
    public delegate void SelfDestroy(GameObject bullet);
    public delegate void BulletShot(GameObject bullet, BulletLogic bulletLogic);
    public delegate void TouchMonster(GameObject bullet, GameObject monster);

}