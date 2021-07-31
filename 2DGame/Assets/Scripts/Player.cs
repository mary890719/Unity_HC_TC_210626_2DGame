using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0,50)]
    public float speed = 10.5f;
    [Header("���D����"), Range(0, 3000)]
    public float jump = 100;
    [Header("��q"), Range(0, 200)]
    public float HP = 100;
    [Header("�O�_�b�a�O�W"), Tooltip("�O�_�b�a�O�W")]
    public bool isGround;

    //�p�H��쬰�����
    //�}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���
    private AudioSource Aud;
    private Rigidbody2D rig;
    private Animator ani;
    #endregion

    #region �ƥ�
    private void Start()
    {
        //GetComponet<����>() �x����k�A�i�H���w��������
        //�@�ΡG���o������2D���餸��
        rig = GetComponent<Rigidbody2D>();
    }
    
    //�@������� 60 ��
    private void Update()
    {
        GetPlayerInputHorizontal();
        TurnDirection();
        Jump();
    }

    //�T�w��s�ƥ�
    //�@��T�w���� 50 ���A�x���ĳ���ϥιD���z API �n�b���ƥ󤺰���
    private void FixedUpdate()
    {
        Move(hValue);
    }
    #endregion

    #region ��k
    /// <summary>
    /// ���a������J��
    /// </summary>
    private float hValue;
        
    /// <summary>
    /// ���o���a��J�����b�V�ȡG���P�k A�BD�B���B�k
    /// </summary>
    private void GetPlayerInputHorizontal() 
    {
        //������ = ��J.���o�b�V(�b�V�W��)
        //�@�ΡG���o���a���U�������䪺�ȡA���k��1�A������-1�A�S����0
        hValue = Input.GetAxis("Horizontal");
        //print("���a������" + hValue);

    }

    [Header("���O"), Range(0.1f, 10)]
    public float gravity = 1;

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="horizontal">���k�ƭ�</param>
    private void Move(float horizontal)
    {
        // �ϰ��ܼ�:�b��k�������A���ϰ�ʡA�ȭ��󦹤�k���s��
        //transform ������ Transform �ܧΤ���
        //posMove = �����e�y��+ ���a��J��������
        //Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        // ����.���ʮy��(�n�e�����y��)
        rig.MovePosition(posMove);
    }

    /// <summary>
    /// �����V�G�B�z���⭱�V���D�A���k���� 0�A�������� 180
    /// </summary>
    private void TurnDirection()
    {

        // �p�G ���a�� D �N�N���׳]�w�� 0, 0, 0
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.zero;
        }
        // �p�G ���a�� A �N�N���׳]�w�� 0, 180, 0
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {
        // �p�G ���a ���U �ťի� ����N���W���D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, jump));
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attake()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�y�����ˮ`</param>
    public void Damage(float damage)
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    private void Dead()
    {

    }

    /// <summary>
    /// �Y�D��
    /// </summary>
    /// <param name="propName">�Y�쪺�D��W��</param>
    private void EatProp(string propName)
    {

    }
    #endregion
}
