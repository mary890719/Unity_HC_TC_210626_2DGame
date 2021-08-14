using UnityEngine;
using System.Linq;

/// <summary>
/// �ĤH�����O
/// �\��G�H�����ʡB���ݡB�l�ܪ��a�B���˻P���`
/// ���A���G�C�| Enum�B�P�_�� switch (��¦�y�k)
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region ���G���}
    [Header("�򥻯�O")]
    [Range(50, 5000)]
    public float hp = 100;
    [Range(5, 1000)]
    public float attack = 20;
    [Range(1, 500)]
    public float speed = 1.5f;

    /// <summary>
    /// �H�����ݽd��
    /// </summary>
    public Vector2 v2RandomIdle = new Vector2(1, 5);
    /// <summary>
    /// �H�������d��
    /// </summary>
    public Vector2 v2RandomWalk = new Vector2(3, 6);
    [Header("�ˬd�e��O�_����ê���Φa�O�y��")]
    public Vector3 checkForwardOffset;
    [Range(0, 1)]
    public float checkForwardRadius = 0.3f;

    // �N�p�H�����ܦb�ݩʭ��O�W
    [SerializeField]
    protected StateEnemy state;
    #endregion

    #region ���G�p�H
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    /// <summary>
    /// ���ݮɶ��G�H��
    /// </summary>
    private float timeIdle;
    /// <summary>
    /// ���ݥέp�ɾ�
    /// </summary>
    private float timerIdle;
    /// <summary>
    /// �����ɶ��G�H��
    /// </summary>
    private float timeWalk;
    /// <summary>
    /// �����έp�ɾ�
    /// </summary>
    private float timerWalk;

    // �{�Ѱ}�C
    // �y�k�G�������[�W���A���A�Ҧp�Gint[]�Bfloat[]�Bstring[]�BVector2[]
    private Collider2D[] hits;
    /// <summary>
    /// �s��e��O�_�����]�t�a�O�B���x������
    /// </summary>
    private Collider2D[] hitResult;
    #endregion

    #region �ƥ�
    private void Start()
    {
        #region ���o����
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        #endregion

        #region ��l�ȳ]�w
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    private void Update()
    {
        CheckForward();
        CheckState();
    }

    private void FixedUpdate()
    {
        WalkInFixedUpdate();
    }

    // �����O�������p�G�Ʊ�l���O���g������`�G
    // 1. �׹��������O public �� protrcted - �O�@ ���\�l���O�s��
    // 2. �K�[����r virtual ���� - ���\�l���O�Ƽg
    // 3. �l���O�ϥ� override �Ƽg
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        // transform.right ��e���󪺥k�� (2D �Ҧ����e��A����b�Y)
        // transform.up ��e���󪺤W�� (���b�Y)
        Gizmos.DrawSphere(
            transform.position +
            transform.right * checkForwardOffset.x +
            transform.up * checkForwardOffset.y,
            checkForwardRadius);
    }
    #endregion

    #region ��k
    /// <summary>
    /// �ˬd�e��G�O�_���a�O�λ�ê��
    /// </summary>
    private void CheckForward()
    {
        hits = Physics2D.OverlapCircleAll(
            transform.position +
            transform.right * checkForwardOffset.x +
            transform.up * checkForwardOffset.y,
            checkForwardRadius);

        // ��ر��p���n��V�A�קK�����ê���H�α���
        // 1. �}�C�������O �a�O �� ���O ���x������ - ����ê��
        // 2. �}�C���O�Ū� - �S���a�诸�߷|����
        // �d�߻y�� LinQ�G�i�H�d�߰}�C��ơA�Ҧp�G�O�_�]�t�a�O�B�O�_����Ƶ���...

        hitResult = hits.Where(x => x.name != "BG" && x.name != "platform" && x.name !="�D��" && x.name !="�i��z���x").ToArray();

        // �}�C���ŭȡG�}�C�ƶq���s
        // �p�G �I���ƶq���s (�e��S���a�诸��) �Ϊ� �I�����G�j��s (�e�観��ê��) ���n��V
        if (hits.Length == 0 || hitResult.Length > 0)
        {
            TurnDirection();
        }
    }

    /// <summary>
    /// ��V
    /// </summary>
    private void TurnDirection()
    {
        float y = transform.eulerAngles.y;
        if (y == 0) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.zero;
    }

    /// <summary>
    /// �ˬd���A
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case StateEnemy.idle:
                Idle();
                break;
            case StateEnemy.walk:
                Walk();
                break;
            case StateEnemy.track:
                break;
            case StateEnemy.attack:
                Attack();
                break;
            case StateEnemy.dead:
                break;
        }
    }

    [Range(0.5f, 5)]
    /// <summary>
    /// �����N�o�ɶ�
    /// </summary>
    public float cdAttack = 3;
    private float timerAttack;

    /// <summary>
    /// �������A�G��������òK�[�N�o
    /// </summary>
    private void Attack()
    {
        if (timerAttack < cdAttack)
        {
            timerAttack += Time.deltaTime;
        }
        else
        {
            AttackMethod();
        }
    }

    /// <summary>
    /// �l���O�i�H�M�w�Ӧp���������k
    /// </summary>
    protected virtual void AttackMethod()
    {
        timerAttack = 0;
        ani.SetTrigger("attack trigger");
        print("����");
    }

    /// <summary>
    /// ���ݡG�H����ƫ�i�J�쨫�����A
    /// �P�w������ܨ������A
    /// </summary>
    private void Idle()
    {
        if(timerIdle < timeIdle)                                        // �p�G �p�ɾ� < ���ݮɶ�
        {
            timerIdle += Time.deltaTime;                                // �֥[�ɶ�
            ani.SetBool("walk switch", false);                              // ���������}���G���ݰʵe
        }
        else                                                            // �_�h
        {
            RandomDirection();                                          // �H����V
            state = StateEnemy.walk;                                    // �������A
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);    // ���o�H�������ɶ�
            timerIdle = 0;                                              // �p�ɾ��k�s
        }
    }

    /// <summary>
    /// �H������
    /// </summary>
    private void Walk()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
            ani.SetBool("walk switch", true);                              // �}�Ҩ����}���G�����ʵe
        }
        else
        {
            state = StateEnemy.idle;
            rig.velocity = Vector2.zero;
            timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
            timerWalk = 0;
        }
    }

    /// <summary>
    /// �N���z�欰��W�B�z�æb FixedUpdate �I�s
    /// </summary>
    private void WalkInFixedUpdate()
    {
        // �p�G �ثe���A �O���� �N ����.�[�t�� = �k�� * �t�� * 1/50 + �W�� * �a�ߤޤO
        if (state == StateEnemy.walk) rig.velocity = transform.right * speed * Time.fixedDeltaTime + Vector3.up * rig.velocity.y;
    }

    /// <summary>
    /// �H����V�G�H�����V�k��Υ���
    /// �Ȭ� 0 �ɡA����G0�A180�A0
    /// �Ȭ� 1 �ɡA�k��G0�A0�A0
    /// </summary>
    private void RandomDirection()
    {
        // �H��.�d��(�̤p�A�̤j) - ��Ʈɤ��]�t�̤j�� (0�A2) - �H�����o 0 �� 1
        int random = Random.Range(0, 2);

        if (random == 0) transform.eulerAngles = Vector2.up * 180;
        else transform.eulerAngles = Vector2.zero;
    }
    #endregion
}

// �w�q�C�|
// 1. �ϥ�����r enum �w�q�C�|�H�Υ]�t���ﶵ�A�i�H�b���O�~�w�q�C
// 2. �ݭn���@�����w�q�����C�|�����C
// �y�k�G�׹��� enum �C�|�W�� { �ﶵ1�A�ﶵ2�A...�A�ﶵN }
public enum StateEnemy
{
    idle, walk, track, attack, dead
}