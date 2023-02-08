using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using static PlayerLogic;
using static EnemyCreator;

public class EnemyCreator : MonoBehaviour
{
    [Tooltip("怪兽的 Prefab")]
    public GameObject enemyPrefab;

    [Tooltip("定时创建新的怪兽")]
    public float interval = 1;
    private LuaEnv luaEnv;
    private CreateEnemy createEnemy;
    private RepeatCreateEnemy repeatCreateEnemy;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'EnemyCreator'");
        createEnemy = luaEnv.Global.Get<CreateEnemy>("createEnemy");
        repeatCreateEnemy = luaEnv.Global.Get<RepeatCreateEnemy>("repeatCreateEnemy");

        repeatCreateEnemy(this);
    }

    private void OnDestroy()
    {
        createEnemy = null;
        repeatCreateEnemy = null;
        luaEnv.Dispose();
    }
    private void createMonster()
    {
        
        createEnemy(this, this.gameObject);
    }
    [CSharpCallLua]
    public delegate void CreateEnemy(EnemyCreator enemyCreator,GameObject gameObject);
    public delegate void RepeatCreateEnemy(EnemyCreator enemyCreator);

}
