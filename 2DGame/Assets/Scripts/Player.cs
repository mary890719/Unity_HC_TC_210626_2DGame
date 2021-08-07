using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 1000)]
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
        ani = GetComponent<Animator>();
    }

    // �@������� 60 ��
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

    [Header("�ˬd�a�O�ϰ�G�첾�P�b�|")]
    public Vector3 groundOffset;
    [Range(0, 2)]
    public float groundRadius = 0.5f;

    // ø�s�ϥܨƥ�G���U�}�o�̥ΡA�ȷ|��ܦb�s�边 Unity ��
    private void OnDrawGizmos()
    {
        // ���M�w�C��Aø�s�ϥ�
        Gizmos.color = new Color(1, 0, 0, 0.3f);    // �b�z������
        // ø�s�y��(�����I�A�b�|)
        Gizmos.DrawSphere(transform.position + groundOffset, groundRadius);   
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
        /** �Ĥ@�ز��ʤ覡�G�ۭq���O
        // �ϰ��ܼ�:�b��k�������A���ϰ�ʡA�ȭ��󦹤�k���s��
        //transform ������ Transform �ܧΤ���
        //posMove = �����e�y��+ ���a��J��������
        //Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        // ����.���ʮy��(�n�e�����y��)
        rig.MovePosition(posMove);
        */

        /** �ĤG�ز��ʤ覡�G�ϥαM�פ������O - ���w�C*/
        rig.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rig.velocity.y);
        // ������ʵe�G������0�� �Ŀ�A����0�� ����
        ani.SetBool("walk switch", horizontal != 0);
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
        // Vector2 �Ѽƥi�H�ϥ� Vector3 �N�J�A�{���|�۰ʧ� Z �b����
        // << �첾�B��l
        // ���w�ϼh�y�k�G1 << �ϼh�s��
        Collider2D hit = Physics2D.OverlapCircle(transform.position + groundOffset, groundRadius, 1 << 6);

        // �p�G �I�쪫��s�b �N�N��b�a���W �_�h �N�N���b�a���W
        // �P�_���p�G�u�� �@�ӵ����Ÿ��F �i�H�ٲ��j�A��
        if (hit) isGround = true;
        else isGround = false;

        //�]�w�ʵe�Ѽ� �P �O�_�b�a���W �ۤ�
        ani.SetBool("jump switch", !isGround);

        print("�I�쪺����G" + hit.name);

        // �p�G �b�a�O�W �åB ���a ���U �ťի� ����N���W���D
        if (isGround && Input.GetKeyDown(KeyCode.Space))
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
