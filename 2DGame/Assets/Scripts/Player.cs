using UnityEngine;
using UnityEngine.UI;                   // �ޥ� ���� API
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("���D����"), Range(0, 30000)]
    public int jump = 100;
    [Header("��q"), Range(0, 200)]
    public float hp = 100;
    [Header("�O�_�b�a�O�W"), Tooltip("�Ψ��x�s����O�_�b�a�O�W����T�A�b�a�O�W true�B���b�a�O�W false")]
    public bool isGround;
    [Header("���O"), Range(0.01f, 1)]
    public float gravity = 1;
    [Header("�ˬd�a�O�ϰ�G�첾�P�b�|")]
    public Vector3 groundOffset;
    [Range(0, 2)]
    public float groundRadius = 0.5f;
    [Header("�����N�o"), Range(0, 5)]
    public float cd = 2;
    [Header("�����O"), Range(0, 1000)]
    public float attack = 20;
    [Header("�����ϰ쪺�첾�P�j�p")]
    public Vector2 checkAttackOffset;
    public Vector3 checkAttackSize;
    [Header("���`�ƥ�")]
    public UnityEvent onDead;
    [Header("���İϰ�")]
    public AudioClip soundJump;
    public AudioClip soundAttack;

    // �p�H��줣���
    // �}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;
    /// <summary>
    /// ���a������J��
    /// </summary>
    private float hValue;
    /// <summary>
    /// �����p�ɾ�
    /// </summary>
    private float timer;
    /// <summary>
    /// �O�_����
    /// </summary>
    private bool isAttack;
    /// <summary>
    /// ��r��q
    /// </summary>
    private Text textHp;
    /// <summary>
    /// ���
    /// </summary>
    private Image imgHp;
    /// <summary>
    /// ��q�̤j�ȡG�O�s��q�̤j�ƭ�
    /// </summary>
    private float hpMax;
    /// <summary>
    /// ��v���������O
    /// </summary>
    private CameraControl cameraControl;
    /// <summary>
    /// �x�s�I�������T
    /// </summary>
    private GameObject goPropHit;
    #endregion

    #region �ƥ�
    private void Start()
    {
        // GetComponent<����>() �x����k�A�i�H���w��������
        // �@�ΡG���o������ 2D ���餸��
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        hpMax = hp;

        textHp = GameObject.Find("��r��q").GetComponent<Text>();
        imgHp = GameObject.Find("���").GetComponent<Image>();

        cameraControl = GameObject.Find("��v��").GetComponent<CameraControl>();
    }

    // �@������� 60 ��
    private void Update()
    {
        GetPlayerInputHorizontal();
        TurnDirection();
        Jump();
        Attack();
    }

    // �T�w��s�ƥ�
    // �@��T�w���� 50 ���A�x���ĳ���ϥΨ쪫�z API �n�b���ƥ󤺰���
    private void FixedUpdate()
    {
        Move(hValue);
    }

    // ø�s�ϥܨƥ�G���U�}�o�̥ΡA�ȷ|��ܦb�s�边 Unity ��
    private void OnDrawGizmos()
    {
        // ���M�w�C��bø�s�ϥ�
        Gizmos.color = new Color(1, 0, 0, 0.3f);    // �b�z������
        // ø�s�y��(�����I�A�b�|)
        Gizmos.DrawSphere(transform.position + groundOffset, groundRadius);

        Gizmos.color = new Color(0.5f, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(
            transform.position +
            transform.right * checkAttackOffset.x +
            transform.up * checkAttackOffset.y,
            checkAttackSize);
    }

    // �I���ƥ�GCollision
    // 1. ��ӸI�����󳣭n�� Collider 
    // 2. �åB�䤤�@�ӭn�� Rigidbody
    // 3. ��ӳ��S���Ŀ� Is Trigger
    // Enter �ƥ�G�I���}�l�ɰ���@��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        goPropHit = collision.gameObject;
        EatProp(collision.gameObject.tag);
    }
    #endregion

    #region ��k
    /// <summary>
    /// ���o���a��J�����b�V�ȡGA�BD�B���B�k
    /// </summary>
    private void GetPlayerInputHorizontal()
    {
        // ������ = ��J.���o�b�V(�b�V�W��)
        // �@�ΡG���o���a���U�������䪺�ȡA���k�� 1�A������ -1�A�S���� 0
        hValue = Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="horizontal">���k�ƭ�</param>
    private void Move(float horizontal)
    {
        /** �Ĥ@�ز��ʤ覡�G�ۭq���O
        // �ϰ��ܼơG�b��k�������A���ϰ�ʡA�ȭ��󦹤�k���s��
        // ²�g�Gtransform ������ Transform �ܧΤ���
        // posMove = �����e�y�� + ���a��J��������
        // Time.fixedDeltaTime �� 1/50 ��
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        // ����.���ʮy��(�n�e�����y��)
        rig.MovePosition(posMove);
        */

        /** �ĤG�ز��ʤ覡�G�ϥαM�פ������O - ���w�C */
        rig.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rig.velocity.y);
        // ������ʵe�G������s�� �Ŀ�A����s�� ����
        ani.SetBool("�����}��", horizontal != 0);
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

        // �]�w�ʵe�Ѽ� �P �O�_�b�a�O�W �ۤ�
        ani.SetBool("���D�}��", !isGround);

        // �p�G �b�a�O�W �åB ���a ���U �ť��� ����N���W���D
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, jump));
            aud.PlayOneShot(soundJump, Random.Range(0.7f, 1.1f));
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        // �p�G ���O������ �åB ���U ���� �~�i�H���� �Ұ�Ĳ�o�Ѽ�
        if (!isAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            ani.SetTrigger("����Ĳ�o");
            aud.PlayOneShot(soundAttack, Random.Range(0.7f, 1.1f));

            // �P�w�����ϰ�O�_������ 8 ���ĤH�ϼh����
            Collider2D hit = Physics2D.OverlapBox(transform.position +
            transform.right * checkAttackOffset.x +
            transform.up * checkAttackOffset.y,
            checkAttackSize, 0, 1 << 8);

            // ��������s�b�� ���y���ˮ`
            if (hit)
            {
                hit.GetComponent<BaseEnemy>().Hurt(attack);     // �ĤH ����
                StartCoroutine(cameraControl.ShakeEffect());    // ��v�� �̰�
            }
        }

        // �p�G���U����������N�}�l�֥[�ɶ�
        if (isAttack)
        {
            if (timer < cd)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                isAttack = false;
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�y�����ˮ`</param>
    public void Hurt(float damage)
    {
        hp -= damage;                       // ��q�����ˮ`��

        if (hp <= 0) Dead();                // �p�G ��q <= 0 �N ���`

        textHp.text = "HP " + hp;           // ��r��q.��r���e = "HP " + ��q
        imgHp.fillAmount = hp / hpMax;      // ���.�񺡼ƭ� = hp / hpMax
    }

    /// <summary>
    /// ���`
    /// </summary>
    private void Dead()
    {
        hp = 0;                             // ��q�k�s
        ani.SetBool("���`�}��", true);       // ���`�ʵe
        onDead.Invoke();                    // �I�s���`�ƥ�
        enabled = false;                    // �������}��
    }

    /// <summary>
    /// �Y�D��
    /// </summary>
    /// <param name="propName">�Y�쪺�D��W��</param>
    private void EatProp(string propName)
    {
        switch (propName)
        {
            case "ī�G":
                Destroy(goPropHit);             // �R��(����A����ɶ�)
                hp += 10;
                hp = Mathf.Clamp(hp, 0, hpMax); // ���� HP �b�d��
                textHp.text = "HP " + hp;       // ��s����
                imgHp.fillAmount = hp / hpMax;
                break;
            default:
                break;
        }
    }
    #endregion
}
