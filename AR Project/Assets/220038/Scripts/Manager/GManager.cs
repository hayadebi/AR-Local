using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public bool walktrg = false; //動ける状態か
    public int setmenu = 0; //UIの表示状態、0はUIが無い時を示す
    public int[] EventNumber; //各イベント状態、0はそのイベントが進行していないことを示す
    public int setrg = -1;
    //設定
    public float audioMax = 0.16f; //音量設定に使用
    public float seMax = 0.16f;//効果音設定に使用
    public int isEnglish = 0; //言語設定に使用

    public int[] Triggers; //各トリガーの状態。イベントとは違い、この宝箱は一度取ってあるのか、この敵は討伐した奴かどうかなどを格納
    public AudioClip ase; //一時的な効果音を格納する用

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete ))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public struct SaveEvent
    {
        public void DataSave()
        {
            ;
            PlayerPrefs.Save();
        }
        public void DataLoad()
        {
            ;
        }
        public void DataReset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public SaveEvent saveevent;
}