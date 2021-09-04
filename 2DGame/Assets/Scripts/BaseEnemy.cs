using UnityEngine;
using System.Linq;

/// <summary>
/// �ĤH�����O
/// �\��G�H�����ʡB���ݡB�l�ܪ��a�B���˻P���` - ���A�ˬd��
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
    /// �����N�o�ɶ�
    /// </summary>
    public float cdAttack = 3;
    [Tooltip("�H�����ݮɶ��d��")]
    /// <summary>
    /// �H�����ݽd��
    /// </summary>
    public Vector2 v2RandomIdle = new Vector2(1, 5);
    [Tooltip("�H�������ɶ��d��")]
    /// <summary>
    /// �H�������d��
    /// </summary>
    public Vector2 v2RandomWalk = new Vector2(3, 6);
    [Header("�ˬd�e��O�_����ê���Φa�O�y��")]
    public Vector3 checkForwardOffset;
    [Range(0, 1)]
    public float checkForwardRadius = 0.3f;
    [Range(0.5f, 5)]
    // �}�C�G�O�s�ۦP��������ƪ��A�֦��s���P�Ȩ�����
    // �}�C�y�k�G����[]
    // �Ҧp�Gint[]�Bstring[]�BGameObject[]�BVector3[]
    [Header("��������A�i�ۦ�]�w�ƶq")]
    public float[] attacksDelay;
    [Header("����������j�h�[��_�쥻���A"), Range(0, 5)]
    public float afterAttackRestoreOriginal = 1;
    [Header("�����D���ơG�D��B���v")]
    public GameObject goProp;
    [Range(0, 1)]
    public float propProbability = 0.3f;
    #endregion

    #region ���G�p�H
    private float timerAttack;
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

    /// <summary>
    /// ���a���O
    /// </summary>
    protected Player player;
    /// <summary>
    /// �����ϰ쪺�I���G�O�s���a�O�_�i�J�H�Ϊ��a�I����T
    /// </summary>
    protected Collider2D hit;
    // �N�p�H�����ܦb�ݩʭ��O�W
    // [SerializeField]
    protected StateEnemy state;
    #endregion

    #region �ƥ�
    private void Start()
    {
        #region ���o����P���a���O
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        player = GameObject.Find("�D��").GetComponent<Player>();
        #endregion

        #region ��l�ȳ]�w
        timeIdle = Random.Range(v2RandomIdle.x, v2RandomIdle.y);
        #endregion
    }

    private void FixedUpdate()
    {
        WalkInFixedUpdate();
    }

    protected virtual void Update()
    {
        CheckForward();
        CheckState();
    }

    // �����O�������p�G�Ʊ�l���O�Ƽg������`�G
    // 1. �׹��������O public �� protected - �O�@ ���\�l���O�s��
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

    #region ��k�G�p�H
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
        // 1. �}�C���O�Ū� - �S���a�诸�߷|����
        // 2. �}�C���� ���O �a�O �åB ���O ���x ������ - ����ê��
        // �d�߻y�� LinQ�G�i�H�d�߰}�C��ơA�Ҧp�G�O�_�]�t�a�O�B�O�_����Ƶ���..

        // �I��a�O�B���x�B�D���Ҥ��|��V
        hitResult = hits.Where(x => x.name != "�a�O" && x.name != "���x" && x.name != "�D��" && x.name != "�i��z���x").ToArray();

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

    /// <summary>
    /// �������A�G��������òK�[�N�o
    /// </summary>
    private void Attack()
    {
        if (timerAttack < cdAttack)
        {
            timerAttack += Time.deltaTime;
            ani.SetBool("�����}��", false);
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
        ani.SetTrigger("����Ĳ�o");
    }

    /// <summary>
    /// ���ݡG�H����ƫ�i�J�쨫�����A
    /// �P�w������ܨ������A
    /// </summary>
    private void Idle()
    {
        if (timerIdle < timeIdle)                                           // �p�G �p�ɾ� < ���ݮɶ�
        {
            #region ���ݭn�B�z���{��
            timerIdle += Time.deltaTime;                                    // �֥[�ɶ�
            ani.SetBool("�����}��", false);                                  // ���������}���G���ݰʵe
            #endregion
        }
        else                                                                // �_�h
        {
            #region ���ݫ�e�������n�B�z���{��
            RandomDirection();                                              // �H����V
            state = StateEnemy.walk;                                        // �������A
            timeWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);        // ���o�H�������ɶ�
            timerIdle = 0;                                                  // �p�ɾ��k�s
            #endregion
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
            ani.SetBool("�����}��", true);      // �}�Ҩ����}���G�����ʵe
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
        // �p�G �ثe���A �O���� �N ����.�[�t�� = �k�� * �t�� *  1/50 + �W�� * �a�ߤޤO
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

    /// <summary>
    /// ���`�G���`�ʵe�B���A�B�����}���B�I�����B�[�t�ץH�έ���ᵲ
    /// </summary>
    private void Dead()
    {
        hp = 0;
        ani.SetBool("���`�}��", true);
        state = StateEnemy.dead;
        GetComponent<CapsuleCollider2D>().enabled = false;      // �����I����
        rig.velocity = Vector3.zero;                            // �[�t���k�s
        rig.constraints = RigidbodyConstraints2D.FreezeAll;     // ����ᵲ����
        DropProp();

        // �q���ǰe�޲z�N�ƶq - 1
        TeleportManager.countAllEnemy--;

        enabled = false;
    }

    /// <summary>
    /// ���`��I�s�����D���k�A���v�ʱ���
    /// </summary>
    private void DropProp()
    {
        if (Random.value <= propProbability)
        {
            // �ͦ�(����A�y�СA����)
            // Quaternion.identity �s���� = Vector3.zero
            Instantiate(goProp, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
    }
    #endregion

    #region ��k�G���}
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�����쪺�ˮ`��</param>
    public void Hurt(float damage)
    {
        hp -= damage;
        ani.SetTrigger("����Ĳ�o");

        if (hp <= 0) Dead();
    }
    #endregion
}

// �w�q�C�|
// 1. �ϥ�����r enum �w�q�C�|�H�Υ]�t���ﶵ�A�i�H�b���O�~�w�q
// 2. �ݭn���@�����w�q�����C�|����
// �y�k�G�׹��� enum �C�|�W�� { �ﶵ1�A�ﶵ2�A....�A�ﶵN }
public enum StateEnemy
{
    idle, walk, track, attack, dead
}