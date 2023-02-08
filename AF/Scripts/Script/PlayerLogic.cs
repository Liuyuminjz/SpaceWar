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

    [Tooltip("�ӵ��ڵ�Ĺ���")]
    public Transform bulletFolder;

    [Tooltip("�ӵ�������")]
    public Transform firePoint;

    [Tooltip("�ӵ�������")]
    public float bulletInterval = 0.5f;

    [Tooltip("����ƶ��ٶ�")]
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
