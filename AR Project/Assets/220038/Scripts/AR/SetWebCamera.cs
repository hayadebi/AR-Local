using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWebCamera : MonoBehaviour
{
    public RawImage webRaw;
    public Animator anim;
    public gpsdatastore gds;
    public void CheckButton()//クリックされたらカメラ起動する用
    {
        StartCoroutine(CheckCm());
    }
    IEnumerator CheckCm()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            anim.SetBool("trg", true);
            gds.FetchStage();
            GManager.instance.walktrg = true;
            StartCamera();
        }
        else Application.Quit();
    }
    // カメラを起動させる
    public void StartCamera()
    {
        // 処理はコルーチンで実行する
        StartCoroutine(_startCamera());
    }

    IEnumerator _startCamera()
    {
        // 指定カメラを起動させる
        var webCam = new WebCamTexture(WebCamTexture.devices[0].name, 900, 1800);

        // RawImageのテクスチャにWebCamTextureのインスタンスを設定
        webRaw.texture = webCam;
        // カメラ起動
        webCam.Play();
        while (webCam.width != webCam.requestedWidth)
        {
            // widthが指定したものになっていない場合は処理を抜けて次のフレームで再開
            yield return null;
        }
        yield break;
    }
}
