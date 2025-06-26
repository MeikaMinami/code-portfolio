using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using static TansakuModeManager;  //�T���V�[���̒��ł̃��[�h�}�l�[�W��

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5.0F;  //�ړ����x
    public float rotateSpeed = 15.0F;  //��]���x
    public float gravity = 0.05f;  //�d��
    public float jumpPower = 3;  //�W�����v�̗�

    private Vector3 moveDirection;  //����������
    private CharacterController controller;  //�v���C���[�̃L�����N�^�[�R���g���[���[

    [SerializeField]
    private Transform rayPosition;  //���C���΂��ʒu5

    [SerializeField]
    private float rayRange = 1f;  //���C�̒���
    private Vector2 moveInput;  //�ړ�����
    private bool jumpInput = false;  //�W�����v����

    //�X�e�[�g���Ǘ�����t���O
    private bool isLanding = false;  //���n�����̃t���O

    [SerializeField] public Transform verRot;  //�c�̎��_�ړ��̕ϐ�(�J�����ɍ��킹��)
    [SerializeField] public Transform horRot;  //���̎��_�ړ��̕ϐ�(�v���C���[�ɍ��킹��)

    Vector3 cameraForward;

    [SerializeField] private GameObject Camera;  //�v���C���[�����삷�郁�C���J����

    private Transform _transform;

    [SerializeField] private Animator animator;  //�v���C���[�̃A�j���[�^�[

    /// <summary>
    /// �L�����N�^�[�̃x�N�g���̓���������
    /// </summary>
    private Vector3 velocity;

    bool isStanding;  //���݂̐ڒn����

    [SerializeField] private float landingThreshold = 3f;  //���n����̋���

    /// <summary>
    /// 0=StopState�A1=WalkState�A2=JumpStartState
    /// </summary>
    int ControllerState = 0;  //�ŏ���StopState

    bool isJumpAnimation = false;  //�W�����v�A�j���[�V�������܂����Ă��邩

    bool Ground;  //���n�����ǂ�����bool

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;  //�t���[�����[�g��60�ɌŒ�

        Screen.SetResolution(1920, 1080, true);  //�𑜓x��1920*1080�ɕύX

        // �R���|�[�l���g�̎擾
        controller = GetComponent<CharacterController>();

        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  //R�L�[�ŉw�O�ցi�f�o�b�O�p�j
        {
            this.transform.position = new Vector3(-21, 0.7f, 20.01f);
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))  //R�L�[�ŉw�O�ցi�f�o�b�O�p�j
        {
            this.transform.position = new Vector3(-21, 0.7f, 20.01f);
            Tansaku_Mode();
            return;
        }

        if (ModeAccess.NowMode != AllMode.Tansaku_Mode)  //�T�����[�h�łȂ��ꍇ�͈ړ������Ȃ�
        {
            return;
        }

        if (isJumpAnimation)  //�W�����v�A�j���[�V�������܂����Ă�������͂��󂯕t���Ȃ�
        {
            moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
            controller.Move(moveDirection);

            //���C���[�}�X�N��ݒ�j
            LayerMask groundLayer = LayerMask.GetMask("Ground");

            //Ray ���΂�
            RaycastHit hit;  //�� ������ UnityEngine �� RaycastHit �𖾎�
            bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

            //�f�o�b�O�p�� Ray ������
            Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

            if(raycastHit)
            {
                isJumpAnimation = false;
            }

            return;
        }

        velocity = controller.velocity; //CharacterController�̑��x���擾

        if (Ground)  //���n��
        {

            //���C���[�}�X�N��ݒ�
            LayerMask groundLayer = LayerMask.GetMask("Ground");

            //Ray ���΂�
            RaycastHit hit;  //�� ������ UnityEngine �� RaycastHit �𖾎�
            bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

            //�f�o�b�O�p�� Ray ������
            Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

            if (raycastHit)
            {
                //Debug.Log($"Ray�� {hit.collider.gameObject.name} �Ƀq�b�g���܂��� (���C���[: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                isStanding = true;  //���C���q�b�g�����ꍇ�͐ڒn���Ă���Ɣ���
            }
            else
            {
                isStanding = false;  //���C���q�b�g���Ȃ������ꍇ�͔�ڒn�Ɣ���
            }

            if (!isStanding)  //�������󂢂Ă�����
            {
                ControllerState = 3;  //FallingState�ɕύX
                Ground = false;
                return;
            }

            switch (ControllerState)  //StopStat��WalkState��JumpStartState�̐؂�ւ�
            {
                case 0:  //StopState
                    moveDirection.y = 0;  //�d�͂�������
                    animator.SetFloat("MoveSpeed", 0f);  //�ړ��A�j���[�V�������~
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    animator.SetTrigger("FallToIdle");  //���n�A�j���[�V�������Đ�
                    if (JumpCheck())  //�W�����v�̓��͂��������ꍇ
                    {
                        break;
                    }
                    else  //�W�����v���͂����������ꍇ
                    {
                        if (MoveCheck())   //�ړ����͂��������ꍇ
                        {
                            ControllerState = 1;  //WalkState�ɕύX
                            break;
                        }
                        else  //�ړ����͂����������ꍇ
                        {
                            break;
                        }
                    }

                case 1:  //WalkState
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    if (JumpCheck())  //�W�����v�̓��͂��������ꍇ
                    {
                        break;
                    }
                    else
                    {
                        //�J�����O���̃x�N�g���̂����������擾���āA�P�ʃx�N�g����
                        cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //�J�����̑O����
                        cameraForward.y = 0;  //���������݂̂��g�p
                        cameraForward = cameraForward.normalized;

                        Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // �J�����̉E����
                        cameraRight.y = 0; //���������݂̂��g�p
                        cameraRight = cameraRight.normalized;

                        //���͂Ɋ�Â��Ĉړ�����������
                        Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                        moveDirection = desiredMoveDirection;  //�n��ł̈ړ��������X�V

                        //���͂�����ꍇ�A�ڕW�����Ɋ��炩�ɉ�]
                        if (desiredMoveDirection.sqrMagnitude > 0.1f)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                        }

                        moveDirection.x *= speed;
                        moveDirection.z *= speed;

                        moveDirection.x *= Time.deltaTime;
                        moveDirection.z *= Time.deltaTime;

                        //�ړ����x�ɉ����ăA�j���[�V����
                        animator.SetFloat("MoveSpeed", desiredMoveDirection.magnitude);
                        animator.SetBool("IsJumping", false);
                        animator.SetBool("IsFalling", false);
                        animator.SetBool("IsLanding", false);

                        //�ړ��K�p
                        controller.Move(moveDirection);

                        if (MoveCheck())   //�ړ����͂��������ꍇ
                        {
                            ControllerState = 1;  //WalkState�ɕύX
                            break;
                        }
                        else  //�ړ����͂����������ꍇ
                        {
                            ControllerState = 0;  //StopState�ɕύX
                            break;
                        }
                    }

                case 2:  //JumpStartState
                    Ground = false;
                    break;
            }

        }
        else  //��
        {
            Debug.Log("�󒆂ɂȂ�܂���");
            if (LandingCheck())  //���n�܂ł̋����ɂȂ��Ă�����
            {
                moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
                if (velocity.y <= -1.0f)  //�������̃x�N�g����-1.0�ȉ��̎�
                {
                    Debug.Log("���n�A�j���[�V�����̋����ɓ��B���܂���");
                    isLanding = true;

                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", false);
                    animator.SetBool("IsLanding", true);
                    Debug.Log("IsLanding��true�ɂ��܂���");

                    //��莞�Ԍ�ɒ��n�A�j���[�V����������
                    Invoke(nameof(ResetLanding), 0.3f);

                    //���C���[�}�X�N��ݒ�
                    LayerMask groundLayer = LayerMask.GetMask("Ground");

                    //Ray ���΂�
                    RaycastHit hit;  //�� ������ UnityEngine �� RaycastHit �𖾎�
                    bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                    //�f�o�b�O�p�� Ray ������
                    Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                    if (raycastHit)
                    {
                        Debug.Log($"Ray�� {hit.collider.gameObject.name} �Ƀq�b�g���܂��� (���C���[: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                        isStanding = true;  //���C���q�b�g�����ꍇ�͐ڒn���Ă���Ɣ���
                        Ground = true;  //���n���[�h�ɐ؂�ւ�
                        Debug.Log("���n�A�j���[�V�����̒��Œ��n���܂���");
                    }

                    //�ړ��K�p
                    controller.Move(moveDirection);

                    ControllerState = 0;  //StopState�ɕύX

                    return;
                }
                else
                {
                    if(MoveCheck())  //�ړ��̓��͂���������
                    {
                        //�J�����O���̃x�N�g���̂����������擾���āA�P�ʃx�N�g����
                        cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //�J�����̑O����
                        cameraForward.y = 0;  //���������݂̂��g�p
                        cameraForward = cameraForward.normalized;

                        Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // �J�����̉E����
                        cameraRight.y = 0; //���������݂̂��g�p
                        cameraRight = cameraRight.normalized;

                        //���͂Ɋ�Â��Ĉړ�����������
                        Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                        //y ������ێ�
                        float originalY = moveDirection.y;

                        //x ����� z �����݂̂��X�V
                        moveDirection = desiredMoveDirection;

                        //y �������Đݒ�
                        moveDirection.y = originalY;

                        //���͂�����ꍇ�A�ڕW�����Ɋ��炩�ɉ�]
                        if (desiredMoveDirection.sqrMagnitude > 0.1f)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                        }

                        //�󒆂ł��ړ��\�ɂ���
                        Vector3 airControl = desiredMoveDirection * speed * 0.3f;  // �󒆎��͈ړ����x�� 30% �ɂ���
                        moveDirection.x = airControl.x * Time.deltaTime;
                        moveDirection.z = airControl.z * Time.deltaTime;

                        //���C���[�}�X�N��ݒ�i��: "Ground" ���C���[��Ώۂɂ���j
                        LayerMask groundLayer = LayerMask.GetMask("Ground");

                        //Ray ���΂�
                        RaycastHit hit;  //�� ������ UnityEngine �� RaycastHit �𖾎�
                        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                        //�f�o�b�O�p�� Ray ������
                        Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                        if (raycastHit)
                        {
                            Debug.Log($"Ray�� {hit.collider.gameObject.name} �Ƀq�b�g���܂��� (���C���[: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                            isStanding = true;  //���C���q�b�g�����ꍇ�͐ڒn���Ă���Ɣ���
                            Ground = true;  //���n���[�h�ɐ؂�ւ�
                            ControllerState = 1;  //WalkState�ɕύX
                        }

                        //�ړ��K�p
                        controller.Move(moveDirection);

                        return;
                    }
                    else
                    {
                        //���C���[�}�X�N��ݒ�i��: "Ground" ���C���[��Ώۂɂ���j
                        LayerMask groundLayer = LayerMask.GetMask("Ground");

                        //Ray ���΂�
                        RaycastHit hit;  //�� ������ UnityEngine �� RaycastHit �𖾎�
                        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out hit, rayRange, groundLayer);

                        //�f�o�b�O�p�� Ray ������
                        Debug.DrawRay(rayPosition.position, Vector3.down * rayRange, raycastHit ? Color.green : Color.red);

                        if (raycastHit)
                        {
                            Debug.Log($"Ray�� {hit.collider.gameObject.name} �Ƀq�b�g���܂��� (���C���[: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");
                            isStanding = true;  // ���C���q�b�g�����ꍇ�͐ڒn���Ă���Ɣ���
                            Ground = true;  //���n���[�h�ɐ؂�ւ�
                            ControllerState = 0;  //StopState�ɕύX
                        }


                        moveDirection.x = 0;
                        moveDirection.z = 0;
                        //�ړ��K�p
                        controller.Move(moveDirection);
                        return;
                    }
                }
            }

            else
            {
                moveDirection -= new Vector3(0.0f, gravity * Time.deltaTime, 0.0f);
                if (MoveCheck())  //�ړ��̓��͂������
                {
                    //�J�����O���̃x�N�g���̂����������擾���āA�P�ʃx�N�g����
                    cameraForward = Camera.transform.TransformDirection(Vector3.forward);  //�J�����̑O����
                    cameraForward.y = 0;  //���������݂̂��g�p
                    cameraForward = cameraForward.normalized;

                    Vector3 cameraRight = Camera.transform.TransformDirection(Vector3.right); // �J�����̉E����
                    cameraRight.y = 0; //���������݂̂��g�p
                    cameraRight = cameraRight.normalized;

                    //���͂Ɋ�Â��Ĉړ�����������
                    Vector3 desiredMoveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

                    //y ������ێ�
                    float originalY = moveDirection.y;

                    //x ����� z �����݂̂��X�V
                    moveDirection = desiredMoveDirection;

                    //y �������Đݒ�
                    moveDirection.y = originalY;

                    //���͂�����ꍇ�A�ڕW�����Ɋ��炩�ɉ�]
                    if (desiredMoveDirection.sqrMagnitude > 0.1f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                    }

                    //�󒆂ł��ړ��\�ɂ���
                    Vector3 airControl = desiredMoveDirection * speed * 0.3f;  //�󒆎��͈ړ����x�� 30% �ɂ���
                    moveDirection.x = airControl.x * Time.deltaTime;
                    moveDirection.z = airControl.z * Time.deltaTime;

                    //�ړ��K�p
                    controller.Move(moveDirection);

                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsJumping", false);  //�W�����v�A�j���[�V�������~

                    return;
                }
                else
                {
                    //�ړ��K�p
                    controller.Move(moveDirection);

                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsJumping", false);  //�W�����v�A�j���[�V�������~

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
        if (jumpInput)  //�W�����v���łȂ���΃W�����v�\
        {
            moveDirection.y += jumpPower;  //������̑��x��ύX�i�������͈ێ��j
            jumpInput = false;  //�W�����v��������x�����s��

            animator.SetBool("IsJumping", true);  // �W�����v�A�j���[�V�����J�n
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsLanding", false);
            Debug.Log("IsJumping��true�ɂ��܂���");

            isJumpAnimation = true;  //�W�����v�X�^�[�g�A�j���[�V�������ɐ؂�ւ�
            ControllerState = 2;  //JumpStartState�ɕύX

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
        if (moveInput.x != 0)  //�ړ����͂��������ꍇ
        {
            return true;
        }
        else  //�ړ����͂����������ꍇ
        {
            return false;
        }
    }

    bool LandingCheck()  //���n�܂ł̋����ɂȂ��Ă��邩
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");  //Ground���C���[���擾
        RaycastHit LandAnimationHit;

        bool raycastHit = Physics.Raycast(rayPosition.position, Vector3.down, out LandAnimationHit, landingThreshold, groundLayer);  //���n�܂ł̋����ɂȂ��Ă��邩

        return raycastHit;
    }
}