using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //*******************************************
    // 変数
    //*******************************************
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度
    [SerializeField] private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用
    private Rigidbody mRigidbody;
    private Transform mTrans;
    private Vector3 mMoveVector;
    private Vector3 mMoveRote;
    float mMoveSpeed;    // 移動速度
    float mMoveMaxSpeed; // 最大速度
    float mRoteSpeed;    // 回転速度
    float mRoteMaxSpeed; // 最大回転速度
    float mRoteDeceleration; // 回転減速値


    //*******************************************
    // 初期化処理
    //*******************************************
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mTrans = GetComponent<Transform>();
        mMoveVector = new Vector3(0, 0, 0);
        mMoveRote = new Vector3(0, 0, 0);
        mMoveSpeed = 0;
        mMoveMaxSpeed = 50.0f;
        mRoteSpeed = 0;
        mRoteMaxSpeed = 1000.0f;
        mRoteDeceleration = 1.0f;

    }


    //*******************************************
    // １フレームに更新される処理
    //*******************************************
    void Update()
    {
        PlayerMove2();
        PhysicsUpdate();

    }

    //*******************************************
    // アクション
    //*******************************************
    private void PlayerMove()
    {
        //=============================
        // 移動
        //=============================
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        bool SpeedOn = false;
        mMoveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            mMoveVector.z -= 1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mMoveVector.x += 1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mMoveVector.z += 1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mMoveVector.x -= 1.0f;
            SpeedOn = true;
        }
        if (SpeedOn == true)
        {
            mMoveSpeed += 50.0f;
        }

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        mMoveVector = mMoveVector.normalized * mMoveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (mMoveVector.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(refCamera.hRotation * -mMoveVector), applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
            mRigidbody.AddForce(refCamera.hRotation * -mMoveVector);

        }
    }

    private void PlayerMove2()
    {
        //=============================
        // 移動
        //=============================
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        bool SpeedOn = false;
        mMoveVector = Vector3.zero;

        // transformを取得
        //Transform myTransform = this.transform;
        //mMoveRote = myTransform.eulerAngles;
        //mMoveRote = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            mMoveRote.x  += 10.0f;
            mMoveVector.z = -1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mMoveRote.x  -= 10.0f;
            mMoveVector.z = 1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mMoveRote.z  += 10.0f;
            mMoveVector.x = 1.0f;
            SpeedOn = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mMoveRote.z  -= 10.0f;
            mMoveVector.x = -1.0f;
            SpeedOn = true;
        }
        if (SpeedOn)
        {
            mMoveSpeed += 10.1f;
           // mRoteSpeed += 10.0f;
        }


        // 回転
        //myTransform.Rotate(refCamera.hRotation * mMoveRote * mMoveSpeed * Time.deltaTime);
        //mRigidbody.rotation = Quaternion.AngleAxis(mMoveRote.z, mMoveVector);
        //mMoveRote = mMoveRote.normalized * mRoteSpeed * Time.deltaTime;
        mRigidbody.angularVelocity = refCamera.hRotation * mMoveRote * Time.deltaTime;
        // 移動
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        mMoveVector = mMoveVector.normalized * mMoveSpeed * Time.deltaTime;
        mRigidbody.AddForce(refCamera.hRotation * -mMoveVector);


    }


    //*******************************************
    // 物理処理
    //*******************************************
    private void PhysicsUpdate()
    {
        //=======================================
        // 自然減速
        //=======================================
        mMoveSpeed -= 10.0f;
        if (mMoveSpeed < 0)
        {
            mMoveSpeed = 0;
        }
        //=======================================
        // 速度制限
        //=======================================
        if (mMoveSpeed > mMoveMaxSpeed)
        {
            mMoveSpeed = mMoveMaxSpeed;
        }
        //=======================================
        // 回転減速
        //=======================================
        //mRoteSpeed -= 1.0f;
        //if(mRoteSpeed < 0)
        //{
        //    mRoteSpeed = 0;
        //}
        if(mMoveRote.x < 0)
        {
            mMoveRote.x += mRoteDeceleration;
        }
        if (mMoveRote.x > 0)
        {
            mMoveRote.x -= mRoteDeceleration;
        }
        if (mMoveRote.z < 0)
        {
            mMoveRote.z += mRoteDeceleration;
        }
        if (mMoveRote.z > 0)
        {
            mMoveRote.z -= mRoteDeceleration;
        }

        //=======================================
        // 回転速度制限
        //=======================================
        //if (mRoteSpeed > mRoteMaxSpeed)
        //{
        //    mRoteSpeed = mRoteMaxSpeed;
        //}
        if (mMoveRote.x > mRoteMaxSpeed)
        {
            mMoveRote.x = mRoteMaxSpeed;
        }
        if (mMoveRote.x < -mRoteMaxSpeed)
        {
            mMoveRote.x = -mRoteMaxSpeed;
        }
        if (mMoveRote.z > mRoteMaxSpeed)
        {
            mMoveRote.z = mRoteMaxSpeed;
        }
        if (mMoveRote.z < -mRoteMaxSpeed)
        {
            mMoveRote.z = -mRoteMaxSpeed;
        }

    }


    //*******************************************
    // 当たり判定処理
    //*******************************************
    private void OnCollisionEnter(Collision target)
    {
        

        if(target.gameObject.tag == "Object")
        {
            Debug.Log(target.gameObject.name); // ぶつかった相手の名前を取得
            mTrans.localScale = mTrans.localScale * 1.2f;
        }
    }
    


}
