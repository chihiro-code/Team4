using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    // X軸の角度を制限するための変数
    float angleUp = 30.0f;
    float angleDown = 30.0f;

    // playerをInspectorで入れる
    [SerializeField] GameObject mPlayer;
    // Main CameraをInspectorで入れる
    [SerializeField] GameObject mCamera;

    //Cameraが回転するスピード
    [SerializeField] float mRotateSpeed = 3;
    //Axisの位置を指定する変数
    [SerializeField] Vector3 mAxisPos;

    //マウススクロールの値を入れる
    [SerializeField] float mScroll;
    //マウスホイールの値を保存
    [SerializeField] float mScrollLog;

    // Start is called before the first frame update
    void Start()
    {
        //CameraのAxisに相対的な位置をlocalPositionで指定
        mCamera.transform.localPosition = new Vector3(0, 0, -3);
        //CameraとAxisの向きを最初だけそろえる
        mCamera.transform.localRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Axisの位置をユニティちゃんの位置＋axisPosで決める
        transform.position = mPlayer.transform.position + mAxisPos;
        //三人称の時のCameraの位置にマウススクロールの値を足して位置を調整
        //thirdPosAdd = thirdPos + new Vector3(0, 0, scrollLog);

        //マウススクロールの値を入れる
        mScroll = Input.GetAxis("Mouse ScrollWheel");
        //scrollAdd += Input.GetAxis("Mouse ScrollWheel") * -10;
        //マウススクロールの値は動かさないと0になるのでここで保存する
        mScrollLog += Input.GetAxis("Mouse ScrollWheel");

        //Cameraの位置、Z軸にスクロール分を加える
        mCamera.transform.localPosition = new Vector3(
            mCamera.transform.localPosition.x,
            mCamera.transform.localPosition.y,
            mCamera.transform.localPosition.z + mScroll);

        //Cameraの角度にマウスからとった値を入れる
        transform.eulerAngles += new Vector3(
            Input.GetAxis("Mouse Y") * mRotateSpeed,
            Input.GetAxis("Mouse X") * mRotateSpeed,
            0);

        //X軸の角度
        float angleX = transform.eulerAngles.x;
        //X軸の値を180度超えたら360引くことで制限しやすくする
        if (angleX >= 180)
        {
            angleX = angleX - 360;
        }
        //Mathf.Clamp(値、最小値、最大値）でX軸の値を制限する
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(
            angleX, angleDown, angleUp),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

    }
}
