using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    //=======================
    // 変数
    //=======================
    private LineRenderer lineRenderer;
    private int positionCount;
    private Camera mainCamera;
    public Vector3 startPos;
    public Vector3 endPos;
    public GameObject centerObj; // 中心オブジェクト


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // ラインの座標指定を、このラインオブジェクトのローカル座標系を基準にするよう設定を変更
        // この状態でラインオブジェクトを移動・回転させると、描かれたラインもワールド空間に取り残されることなく、一緒に移動・回転
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        // このラインオブジェクトを、位置はカメラ前方10m、回転はカメラと同じになるようキープさせる
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
        Vector3 pos = Input.mousePosition;
        pos.z = 10.0f;

        // マウススクリーン座標をワールド座標に直す
        pos = mainCamera.ScreenToWorldPoint(pos);

        // さらにそれをローカル座標に直す。
        pos = transform.InverseTransformPoint(pos);

        if (Input.GetMouseButton(0))
        {
            // 得られたローカル座標をラインレンダラーに追加する
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);
        }
        //リセットする
        if (!(Input.GetMouseButton(0)))
        {
            positionCount = 0;
            endPos = pos;
        }

        // 書き出し位置を記憶
        if(positionCount == 1)
        {
            startPos = pos;
        }

    }
}
