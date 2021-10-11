using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosText : MonoBehaviour
{
    //=======================
    // 変数
    //=======================
    //変数設定
    float mX;
    float mY;
    float mZ;
    float mX2;
    float mY2;
    float mZ2;
    //知りたい座標のGaeObjectの設定
    public GameObject target;

    private GameObject drawLine;
    private DrawLine dlScript;

    // Start is called before the first frame update
    void Start()
    {
        drawLine = GameObject.Find("Line");
        dlScript = drawLine.GetComponent<DrawLine>();

    }

    // Update is called once per frame
    void Update()
    {
        //それぞれに座標を挿入
        //mX = target.transform.position.x;
        //mY = target.transform.position.y;
        //mZ = target.transform.position.z;

        // Lineの座標
        // 始点
        mX = dlScript.startPos.x;
        mY = dlScript.startPos.y;
        mZ = dlScript.startPos.z;
        // 終点
        mX2 = dlScript.endPos.x;
        mY2 = dlScript.endPos.y;
        mZ2 = dlScript.endPos.z;

        //テキストに表示
        this.GetComponent<Text>().text = 
            "【始点座標】" + 
            "\nX座標は" + mX.ToString() + 
            "\nY座標は" + mY.ToString() + 
            "\nZ座標は" + mZ.ToString() +
            "\n【終点座標】" +
            "\nX座標は" + mX2.ToString() +
            "\nY座標は" + mY2.ToString() +
            "\nZ座標は" + mZ2.ToString();

    }
}
