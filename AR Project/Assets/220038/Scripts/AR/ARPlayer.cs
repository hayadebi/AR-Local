using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlayer : MonoBehaviour
{
    public GPSManager gpsm;//いつでも取得できるように
    private Vector3 latestPos=Vector3.zero;
    public float rot_speed;//カメラ回転速度；
    private float tmp_time = 0f;
    private bool movetrg = false;//移動タイミングかどうか
    public Transform rot_cm;
    private bool starttrg = false;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GManager.instance.walktrg != starttrg)
        {
            gpsm.GetGPS();
            startPos = new Vector3((float)gpsm.get_latitude, this.transform.position.y, (float)gpsm.get_longitude);
            latestPos = new Vector3((float)gpsm.get_latitude-startPos.x, this.transform.position.y, (float)gpsm.get_longitude-startPos.z);
            this.transform.position = latestPos;
            starttrg = GManager.instance.walktrg;
        }
        else if (latestPos != Vector3.zero && GManager.instance.walktrg && starttrg)
        {
            movetrg = false;
            //移動
            if(tmp_time>=0.1f)//常にやると負荷やメモリリークが怖いので心なしか対策
            {
                tmp_time = 0f;
                Resources.UnloadUnusedAssets();//用心
                gpsm.GetGPS();
                latestPos = new Vector3((float)gpsm.get_latitude - startPos.x, this.transform.position.y, (float)gpsm.get_longitude - startPos.z);
                this.transform.position = latestPos;
                movetrg = true;
            }
            tmp_time += Time.deltaTime;
            //カメラ回転検知
            Vector3 targetPositon = latestPos;
            if (this.transform.position.y != latestPos.y)// 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
                targetPositon = new Vector3(latestPos.x, this.transform.position.y, latestPos.z);
            Vector3 diff = this.transform.position - targetPositon;
            if (diff != Vector3.zero || movetrg)
            {
                Quaternion targetRotation = Quaternion.LookRotation(diff);
                rot_cm.rotation = Quaternion.Slerp(rot_cm.rotation, targetRotation, Time.deltaTime * rot_speed);
                var tmprot = rot_cm.eulerAngles;
                tmprot.x = gpsm.get_xrot;
                rot_cm.eulerAngles = tmprot;
                latestPos = this.transform.position;
            } 
        }
    }
}
