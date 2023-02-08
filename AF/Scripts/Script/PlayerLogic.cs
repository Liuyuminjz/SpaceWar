using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using static BulletLogic;
using static PlayerLogic;
using UnityEngine.SocialPlatforms;

public class PlayerLogic : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Tooltip("子弹节点的管理")]
    public Transform bulletFolder;

    [Tooltip("子弹出生点")]
    public Transform firePoint;

    [Tooltip("子弹发射间隔")]
    public float bulletInterval = 0.5f;

    [Tooltip("玩家移动速度")]
    public float playerSpeed = 0.3f;

    private LuaEnv luaEnv;
    private Fire fire;
    private KeepFire keepFire;
    private ControlKey controlKey;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'PlayerLogic'");
        fire = luaEnv.Global.Get<Fire>("fire");
        keepFire = luaEnv.Global.Get<KeepFire>("keepFire");
        controlKey = luaEnv.Global.Get<ControlKey>("controlKey");

        keepFire(this);
    }

    // Update is called once per frame
    void Update()
    {
        controlKey(this.gameObject,this);
    }
    private void OnDestroy()
    {
        fire = null;
        keepFire = null;
        controlKey = null;
        luaEnv.Dispose();
    }
    private void Shot()
    {
        fire(this);
    }
    [CSharpCallLua]
    public delegate void Fire(PlayerLogic playerLogic);
    public delegate void KeepFire(PlayerLogic playerLogic);
    public delegate void ControlKey(GameObject gameObject, PlayerLogic playerLogic);

}
