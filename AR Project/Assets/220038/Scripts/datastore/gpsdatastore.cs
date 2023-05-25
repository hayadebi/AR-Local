using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gpsdatastore : MonoBehaviour
{
    private string class_name = "gpsdatastore_class";//データストア内から参照・新規作成するクラスネーム名
    private string latitude_name = "latitude";//クラス内から参照・新規作成する緯度変数名
    private string longitude_name = "longitude";//クラス内から参照・新規作成する経度変数名
    private string comment_name = "comment_name";//クラス内から参照・新規作成するコメント内容変数名
    private string savegps_name = "get_gps";//クラス内から参照・新規作成するGPS取得文変数名
    private string savegps_id = "gpsid";//クラス内から参照・新規作成するランダムなID変数名
    public int query_limit = 999;//表示制限数。表示データの肥大化を防ぐ。超えたら新たなデータストアを開設。
    public InputField set_commentfield;//コメント書き込む用のInputField
    public Text get_commentthis;//書き込んだコメントと、書き込んだコメント座標を表示する用のTextコンポーネント
    public Text get_commentall;//取得した近辺の全コメントを表示する用のTextコンポーネント
    private NCMBObject CreateClass = null;//データストアクラス
    private string tmp_savetext;//一時的な文字格納場所
    public GPSManager gps_manager;//シーン内のGPSManager格納
    public double max_viewarea = 1.0;
    public GameObject comment_obj;
    private GameObject instantiate_obj;
    // Start is called before the first frame update
    void Start()
    {
        //get_commentthis.text = "(この地に書き込みでコメントを残してみませんか？)";
        ;
    }

    public void ClickDataSet()
    {
        //ワード規制サンプル
        //bool tmp_notword = false;
        //if (get_namefield.text == "") get_namefield.text = "No name";
        //for (int w = 0; w < ゲームマネージャー内の禁止ワード一覧.Length;)
        //{
        //    if (get_namefield.text.Contains(ゲームマネージャー内の禁止ワード一覧[w]))
        //    {
        //        tmp_notword = true;
        //        break;
        //    }
        //    w++;
        //}
        //bool notname = false;
        //if (!tmp_notword)
        //{
        //    ;
        //}
        //else if (tmp_notword)
        //{
        //    if (ゲームマネージャー内の言語管理 == 0)
        //        get_namefield.text = "※規制されたコメントです";
        //    else
        //        get_namefield.text = "※Regulated name.";
        //    notname = true;
        //    ゲームマネージャー内の効果音管理 = 1;
        //}
        //if (!notname)
        //{
        gps_manager.GetGPS();
        CreateClass = new NCMBObject(class_name);//参照するクラスが入る。無かったら勝手に新規作成される。
        CreateClass[comment_name] = set_commentfield.text;//新規でコメント内容を、データストア内の参照クラスに反映させる準備
        CreateClass[latitude_name] = gps_manager.get_latitude;//新規で緯度を、データストア内の参照クラスに反映させる準備
        CreateClass[longitude_name] = gps_manager.get_longitude;//新規で経度を、データストア内の参照クラスに反映させる準備
        tmp_savetext = "緯度：" + gps_manager.get_latitude.ToString() + ",経度：" + gps_manager.get_longitude.ToString();//以下のために一時的格納
        CreateClass[savegps_name] = tmp_savetext;//新規で緯度経度をまとめた文字列を、データストア内の参照クラスに反映させる準備
        CreateClass[savegps_id] = UnityEngine.Random.Range((int)0, (int)9999);//GPSIDをランダムに反映させる準備
        CreateClass.SaveAsync();//準備したデータ達を、参照クラス内に"まとめて"追加する。
        get_commentthis.text = "<size=16>【" + tmp_savetext + "】</size>\n「" + set_commentfield.text + "」\n\n";
        set_commentfield.text = "";

        FetchStage();
        //}
    }
    public void FetchStage()
    {
        get_commentall.text = "";
        //取得から配置
        NCMBQuery<NCMBObject> query = null;
        query = new NCMBQuery<NCMBObject>(class_name);//クラス名から参照するクラスを入れる
        //ソートしてデータを取得
        query.OrderByDescending(savegps_id);
        //検索件数を設定
        query.Limit = query_limit;
        int i = 1;

        GameObject[] tmp_arobjs = GameObject.FindGameObjectsWithTag("arobj");
        foreach (GameObject obj in tmp_arobjs)
        {
            Destroy(obj.gameObject);
        }
        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                get_commentall.text = "(周辺にはコメントがまだ無いようだ…)";
            }
            else
            {
                gps_manager.GetGPS();
                double tmp_la = gps_manager.get_latitude *= 10000;
                double tmp_lo = gps_manager.get_longitude *= 10000;
                //Vectorに変換する際はfloatが必須なので、上記でdouble変数に*10000して値をなるべく崩さないようにする
                Vector3 tmp_this = new Vector3((float)tmp_la,0, (float)tmp_lo);
                //検索成功時の処理
                foreach (NCMBObject obj in objList)
                {
                    tmp_la = (double)obj[latitude_name] * 10000;
                    tmp_lo = (double)obj[longitude_name] * 10000;
                    Vector3 tmp_check = new Vector3((float)tmp_la,0, (float)tmp_lo);
                    if ((double)(tmp_this - tmp_check).magnitude <= max_viewarea)//もし周辺のコメントなら表示
                    {
                        instantiate_obj = Instantiate(comment_obj, new Vector3((float)tmp_la / 10000, 0, (float)tmp_lo / 10000), transform.rotation);
                    }
                    i += 1;
                }
                if (get_commentall.text == "") get_commentall.text = "(周辺にはコメントがまだ無いようだ…)";
            }
        });

    }
}
