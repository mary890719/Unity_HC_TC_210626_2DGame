using UnityEngine;

/// <summary>
/// �ĤH�����O
/// �\��G�H�����ʡB���ݡB�l�ܪ��a�B���˻P���`
/// ���A���G�C�| Enum�B�P�_�� switch (��¦�y�k)
/// </summary>
public class BaseEnemy : MonoBehaviour
{
    #region ���
    [Header("�򥻯�O")]
    [Range(50, 5000)]
    public float hp = 100;
    [Range(5, 1000)]
    public float attack = 20;
    [Range(1, 500)]
    public float speed = 1.5f;

    // �N�p�H�����ܦb�ݩʭ��O�W
    [SerializeField]
    public StateEnemy state;

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
    #endregion

    #region �ƥ�
    private void Start()
    {
        #region ��l�ȳ]�w
        timeIdle = Random.Range(1f, 5f);
        #endregion
    }
    private void Update()
    {
        CheckState();
    }
    #endregion

    #region ��k
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
                break;
            case StateEnemy.dead:
                break;
        }
    }

    /// <summary>
    /// ���ݡG�H����ƫ�i�J�쨫�����A
    /// �P�w������ܨ������A
    /// </summary>
    private void Idle()
    {
        if(timerIdle < timeIdle)                  // �p�G �p�ɾ� < ���ݮɶ�
        {
            timerIdle += Time.deltaTime;        // �֥[�ɶ�
            print("����");
        }
        else                                    // �_�h
        {
            state = StateEnemy.walk;            //�������A
            timerIdle = 0;                      //�p�ɾ��k�s
        }
    }

    /// <summary>
    /// �H������
    /// </summary>
    private void Walk()
    {
        print("����");
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