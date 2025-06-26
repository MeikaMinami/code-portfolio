using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using static TansakuModeManager;  //探索シーンの中でのモードマネージャ

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5.0F;  //移動速度
    public float rotateSpeed = 15.0F;  //回転速度
    public float gravity = 0.05f;  //重力
    public float jumpPower = 3;  //ジャンプの力

    private Vector3 moveDirection;  //動いた距離
    private CharacterController controller;  //プレイヤーのキャラクターコントローラー

    [SerializeField]
    private Transform rayPosition;  //レイを飛ばす位置5

    [SerializeField]
    private float rayRange = 1f;  //レイの長さ
    private Vector2 moveInput;  //移動入力
    private bool jumpInput = false;  //ジャンプ入力

    //ステートを管理するフラグ
    private bool isLanding = false;  //着地中かのフラグ

    [SerializeField] public Transform verRot;  //縦の視点移動の変数(カメラに合わせる)
    [SerializeField] public Transform horRot;  //横の視点移動の変数(プレイヤーに合わせる)

    Vector3 cameraForward;

    [SerializeField] private GameObject Camera;  //プレイヤーが操作するメインカメラ

    private Transform _transform;

    [SerializeField] private Animator animator;  //プレイヤーのアニメーター

    /// <summary>
    /// キャラクターのベクトルの動きを入れる
    /// </summary>
    private Vector3 velocity;

    bool isStanding;  //現在の接地判定

    [SerializeField] private float landingThreshold = 3f;  //着地判定の距離

    /// <summary>
    /// 0=StopState、1=WalkState、2=JumpStartState
    /// </summary>
    int ControllerState = 0;  //最初はStopState

    bool isJumpAnimation = false;  //ジャンプアニメーションをまだしているか

    bool Ground;  //着地中かどうかのbool

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;  //フレームレートを60に固定

        Screen.SetResolution(1920, 1080, true);  //解像度を1920*1080に変更

        // コンポーネントの取得
        controller = GetComponent<CharacterController>();

        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  //Rキーで駅前へ（デバッグ用）
        {
            this.transform.position = new Vector3(-21, 0.7f, 20.01f);
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))  //Rキーで駅前へ（デバッグ用）
        {
            this.transform.position = new Vector3(-21, 0.7f, 20.01f);
            Tansaku_Mode();
            return;
        }

        if (ModeAccess.NowMode != AllMode.Tansaku_Mode)  //探索モードでない場合は移動させない
        {
            return;
        }

        if (isJumpAnimation)  //ジャンプアニメーションをまだしていたら入力を受け付けない
        {
            moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
            controller.Move(moveDirection);

            //レイヤーマスクを設定）
            LayerMask groundLayer = LayerMask.GetMask("Ground");

            //Ray を飛ばす
            RaycastHit hit;  //← ここで UnityEngine の RaycastHit を明示
            bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

            //デバッグ用の Ray を可視化
            Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

            if(raycastHit)
            {
                isJumpAnimation = false;
            }

            return;
        }

        velocity = controller.velocity; //CharacterControllerの速度を取得

        if (Ground)  //着地中
        {

            //レイヤーマスクを設定
            LayerMask groundLayer = LayerMask.GetMask("Ground");

            //Ray を飛ばす
            RaycastHit hit;  //← ここで UnityEngine の RaycastHit を明示
            bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

            //デバッグ用の Ray を可視化
            Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

            if (raycastHit)
            {
                //Debug.Log($"Rayが {hit.collider.gameObject.name} にヒットしました (レイヤー: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                isStanding = true;  //レイがヒットした場合は接地していると判定
            }
            else
            {
                isStanding = false;  //レイがヒットしなかった場合は非接地と判定
            }

            if (!isStanding)  //足元が空いていたら
            {
                ControllerState = 3;  //FallingStateに変更
                Ground = false;
                return;
            }

            switch (ControllerState)  //StopStatとWalkStateとJumpStartStateの切り替え
            {
                case 0:  //StopState
                    moveDirection.y = 0;  //重力を初期化
                    animator.SetFloat("MoveSpeed", 0f);  //移動アニメーションを停止
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    animator.SetTrigger("FallToIdle");  //着地アニメーションを再生
                    if (JumpCheck())  //ジャンプの入力があった場合
                    {
                        break;
                    }
                    else  //ジャンプ入力が無かった場合
                    {
                        if (MoveCheck())   //移動入力があった場合
                        {
                            ControllerState = 1;  //WalkStateに変更
                            break;
                        }
                        else  //移動入力が無かった場合
                        {
                            break;
                        }
                    }

                case 1:  //WalkState
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    if (JumpCheck())  //ジャンプの入力があった場合
                    {
                        break;
                    }
                    else
                    {
                        //カメラ前方のベクトルのｘｚ成分を取得して、単位ベクトル化
                        cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //カメラの前方向
                        cameraForward.y = 0;  //水平部分のみを使用
                        cameraForward = cameraForward.normalized;

                        Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // カメラの右方向
                        cameraRight.y = 0; //水平成分のみを使用
                        cameraRight = cameraRight.normalized;

                        //入力に基づいて移動方向を決定
                        Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                        moveDirection = desiredMoveDirection;  //地上での移動方向を更新

                        //入力がある場合、目標方向に滑らかに回転
                        if (desiredMoveDirection.sqrMagnitude > 0.1f)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                        }

                        moveDirection.x *= speed;
                        moveDirection.z *= speed;

                        moveDirection.x *= Time.deltaTime;
                        moveDirection.z *= Time.deltaTime;

                        //移動速度に応じてアニメーション
                        animator.SetFloat("MoveSpeed", desiredMoveDirection.magnitude);
                        animator.SetBool("IsJumping", false);
                        animator.SetBool("IsFalling", false);
                        animator.SetBool("IsLanding", false);

                        //移動適用
                        controller.Move(moveDirection);

                        if (MoveCheck())   //移動入力があった場合
                        {
                            ControllerState = 1;  //WalkStateに変更
                            break;
                        }
                        else  //移動入力が無かった場合
                        {
                            ControllerState = 0;  //StopStateに変更
                            break;
                        }
                    }

                case 2:  //JumpStartState
                    Ground = false;
                    break;
            }

        }
        else  //空中
        {
            Debug.Log("空中になりました");
            if (LandingCheck())  //着地までの距離になっていたら
            {
                moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
                if (velocity.y <= -1.0f)  //下向きのベクトルが-1.0以下の時
                {
                    Debug.Log("着地アニメーションの距離に到達しました");
                    isLanding = true;

                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    animator.SetBool("IsLanding", true);
                    Debug.Log("IsLandingをtrueにしました");

                    //一定時間後に着地アニメーションを解除
                    Invoke(nameof(ResetLanding), 0.3f);

                    //レイヤーマスクを設定
                    LayerMask groundLayer = LayerMask.GetMask("Ground");

                    //Ray を飛ばす
                    RaycastHit hit;  //← ここで UnityEngine の RaycastHit を明示
                    bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                    //デバッグ用の Ray を可視化
                    Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                    if (raycastHit)
                    {
                        Debug.Log($"Rayが {hit.collider.gameObject.name} にヒットしました (レイヤー: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                        isStanding = true;  //レイがヒットした場合は接地していると判定
                        Ground = true;  //着地モードに切り替え
                        Debug.Log("着地アニメーションの中で着地しました");
                    }

                    //移動適用
                    controller.Move(moveDirection);

                    ControllerState = 0;  //StopStateに変更

                    return;
                }
                else
                {
                    if(MoveCheck())  //移動の入力があったら
                    {
                        //カメラ前方のベクトルのｘｚ成分を取得して、単位ベクトル化
                        cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //カメラの前方向
                        cameraForward.y = 0;  //水平部分のみを使用
                        cameraForward = cameraForward.normalized;

                        Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // カメラの右方向
                        cameraRight.y = 0; //水平成分のみを使用
                        cameraRight = cameraRight.normalized;

                        //入力に基づいて移動方向を決定
                        Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                        //y 成分を保持
                        float originalY = moveDirection.y;

                        //x および z 成分のみを更新
                        moveDirection = desiredMoveDirection;

                        //y 成分を再設定
                        moveDirection.y = originalY;

                        //入力がある場合、目標方向に滑らかに回転
                        if (desiredMoveDirection.sqrMagnitude > 0.1f)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                        }

                        //空中でも移動可能にする
                        Vector3 airControl = desiredMoveDirection * speed * 0.3f;  // 空中時は移動速度を 30% にする
                        moveDirection.x = airControl.x * Time.deltaTime;
                        moveDirection.z = airControl.z * Time.deltaTime;

                        //レイヤーマスクを設定（例: "Ground" レイヤーを対象にする）
                        LayerMask groundLayer = LayerMask.GetMask("Ground");

                        //Ray を飛ばす
                        RaycastHit hit;  //← ここで UnityEngine の RaycastHit を明示
                        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                        //デバッグ用の Ray を可視化
                        Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                        if (raycastHit)
                        {
                            Debug.Log($"Rayが {hit.collider.gameObject.name} にヒットしました (レイヤー: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                            isStanding = true;  //レイがヒットした場合は接地していると判定
                            Ground = true;  //着地モードに切り替え
                            ControllerState = 1;  //WalkStateに変更
                        }

                        //移動適用
                        controller.Move(moveDirection);

                        return;
                    }
                    else
                    {
                        //レイヤーマスクを設定（例: "Ground" レイヤーを対象にする）
                        LayerMask groundLayer = LayerMask.GetMask("Ground");

                        //Ray を飛ばす
                        RaycastHit hit;  //← ここで UnityEngine の RaycastHit を明示
                        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                        //デバッグ用の Ray を可視化
                        Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                        if (raycastHit)
                        {
                            Debug.Log($"Rayが {hit.collider.gameObject.name} にヒットしました (レイヤー: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                            isStanding = true;  // レイがヒットした場合は接地していると判定
                            Ground = true;  //着地モードに切り替え
                            ControllerState = 0;  //StopStateに変更
                        }


                        moveDirection.x = 0;
                        moveDirection.z = 0;
                        //移動適用
                        controller.Move(moveDirection);
                        return;
                    }
                }
            }

            else
            {
                moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
                if (MoveCheck())  //移動の入力があれば
                {
                    //カメラ前方のベクトルのｘｚ成分を取得して、単位ベクトル化
                    cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //カメラの前方向
                    cameraForward.y = 0;  //水平部分のみを使用
                    cameraForward = cameraForward.normalized;

                    Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // カメラの右方向
                    cameraRight.y = 0; //水平成分のみを使用
                    cameraRight = cameraRight.normalized;

                    //入力に基づいて移動方向を決定
                    Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                    //y 成分を保持
                    float originalY = moveDirection.y;

                    //x および z 成分のみを更新
                    moveDirection = desiredMoveDirection;

                    //y 成分を再設定
                    moveDirection.y = originalY;

                    //入力がある場合、目標方向に滑らかに回転
                    if (desiredMoveDirection.sqrMagnitude > 0.1f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                    }

                    //空中でも移動可能にする
                    Vector3 airControl = desiredMoveDirection * speed * 0.3f;  //空中時は移動速度を 30% にする
                    moveDirection.x = airControl.x * Time.deltaTime;
                    moveDirection.z = airControl.z * Time.deltaTime;

                    //移動適用
                    controller.Move(moveDirection);

                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsJumping", false);  //ジャンプアニメーションを停止

                    return;
                }
                else
                {
                    //移動適用
                    controller.Move(moveDirection);

                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsJumping", false);  //ジャンプアニメーションを停止

                    return;
                }
            }

        }
    }

    void ResetLanding()
    {
        isLanding = false;
        animator.SetBool("IsLanding", false);
    }

    bool JumpCheck()
    {
        if (jumpInput)  //ジャンプ中でなければジャンプ可能
        {
            moveDirection.y += jumpPower;  //上方向の速度を変更（横方向は維持）
            jumpInput = false;  //ジャンプ処理を一度だけ行う

            animator.SetBool("IsJumping", true);  // ジャンプアニメーション開始
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsLanding", false);
            Debug.Log("IsJumpingをtrueにしました");

            isJumpAnimation = true;  //ジャンプスタートアニメーション中に切り替え
            ControllerState = 2;  //JumpStartStateに変更

            moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);

            controller.Move(moveDirection);

            return true;
        }
        else
        {
            return false;
        }
    }

    bool MoveCheck()
    {
        if (moveInput.x != 0)  //移動入力があった場合
        {
            return true;
        }
        else  //移動入力が無かった場合
        {
            return false;
        }
    }

    bool LandingCheck()  //着地までの距離になっているか
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");  //Groundレイヤーを取得
        RaycastHit LandAnimationHit;

        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out LandAnimationHit, landingThreshold, groundLayer);  //着地までの距離になっているか

        return raycastHit;
    }
}