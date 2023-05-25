using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
    public double get_latitude;//緯度を表す。他のスクリプトから取得しやすいように
    public double get_longitude;//経度を表す。他のスクリプトから取得しやすいように
    [DllImport("__Internal")]
    
    private static extern void GetCurrentPosition();//Plugins内のGPSTestから取得してる
    private void Start()
    {
        Invoke(nameof(GetGPS), 1f);//1秒後に取得テスト実行
    }
    public void GetGPS()
    {
        GetCurrentPosition();//GPSManager内のを呼び出す
    }
    public void ShowLocation(string location)//取得してからの処理。この場合は変数get_に格納(緯度経度)
    {
        string[] locations = location.Split(',');//取得結果を,の部分で区切り、配列化する
        get_latitude = double.Parse(locations[0]);//緯度
        get_longitude = double.Parse(locations[1]);//経度
    }
}
