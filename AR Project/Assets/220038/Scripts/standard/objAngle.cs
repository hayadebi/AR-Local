using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objAngle : MonoBehaviour
{
    public bool allangle = false;
    public bool startangle = false;
    public float limitangle = -1;
    GameObject P;
    bool movetrg = true;
    public bool ptrg = false;
    public GameObject[] targetobj = null;
    public int indextarget = -1;
    public string objname;
    private Vector3 vec;
    public float RotSpeed = 2;
    private bool startTrg = false;
    // Start is called before the first frame update
    void Start()
    {
        RotSpeed = 3;
        P = GameObject.Find("Player");
        if(targetobj != null && objname != "")
        {
            targetobj[0] = GameObject.Find(objname);
        }
        if (P != null && startangle == true)
        {
            if (indextarget == -1)
            {
                var rotation = Quaternion.LookRotation(P.transform.position - this.transform.position);

                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
            if (indextarget != -1 && targetobj != null)
            {
                var rotation = Quaternion.LookRotation(targetobj[indextarget].transform.position - this.transform.position);

                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
        }
        Invoke("startAngle", 0.1f);
    }
    void startAngle()
    {
        startTrg = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (P != null && startangle == false && movetrg == true && ptrg == false)
        {
            _Rot();
        }
        if(!startTrg && startangle )
        {
            _Rot();
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "area" && movetrg == false)// && es != null)
        {
            // 表示されている場合の処理
            movetrg = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "area"&& movetrg == false)// && es != null)
        {
            // 表示されている場合の処理
            movetrg = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "area" && movetrg == true)// && es != null)
        {
            // 表示されている場合の処理
            movetrg = false;
        }
    }

    private void _Rot()
    {
        if (indextarget == -1)
        {
            if (!allangle)
            {
                Vector3 tDir = new Vector3(P.transform.position.x, this.transform.position.y, P.transform.position.z) - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, tDir, RotSpeed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            else if (allangle)
            {
                Vector3 tDir = new Vector3(P.transform.position.x, P.transform.position.y, P.transform.position.z) - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, tDir, RotSpeed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            //変換
            if (limitangle != -1)
            {
                float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                if (rotateY > limitangle)
                {
                    Vector3 er = this.transform.localEulerAngles;
                    er.y = limitangle;
                    this.transform.localEulerAngles = er;
                }
                else if (rotateY < -limitangle)
                {
                    Vector3 er = this.transform.localEulerAngles;
                    er.y = -limitangle;
                    this.transform.localEulerAngles = er;
                }
            }
        }
        else if (indextarget != -1 && targetobj != null)
        {
            if (targetobj[indextarget] != null)
            {
                if (!allangle)
                {
                    Vector3 tDir = new Vector3(targetobj[indextarget].transform.position.x, this.transform.position.y, targetobj[indextarget].transform.position.z) - transform.position;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, tDir, RotSpeed * Time.deltaTime, 0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
                else if (allangle)
                {
                    Vector3 tDir = new Vector3(targetobj[indextarget].transform.position.x, targetobj[indextarget].transform.position.y, targetobj[indextarget].transform.position.z) - transform.position;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, tDir, RotSpeed * Time.deltaTime, 0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
        }
    }
}
