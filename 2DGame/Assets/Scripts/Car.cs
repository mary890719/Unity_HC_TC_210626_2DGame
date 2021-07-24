using UnityEngine;      //�ޥ�Unity���� ���Ѫ�API(Unity Engine �R�W�Ŷ�)

//���O
//�y�k�G�׹��� ���O����r �}���W��
public class Car : MonoBehaviour
{
    #region ����
    //�����ѡG�K�[�����B½Ķ�B���������K�|�Q�{������
    /*
     * �h�����
     */
    #endregion

    #region �{�����P�`�Υ|�j����
    //���G�x�s²�檺���
    //�y�k�G
    //�׹��� ������� ���W�� ���w�Ÿ� �w�]�� ����
    //���w�Ÿ� =
    //�׹����G
    //1.�p�H private �w�]   - �����
    //2.���} public         - ���

    //Unity ���`�Ϊ��|�j����
    //���    int     �ҡG1, 99, 0, -33
    //�B�I��  float   �ҡG0.1, 0.005, -0.33, �ϥίB�I�Ʈɶ��[�W"f"
    //�r��    string  �ҡGBMW, ���h, ��ܤ��e@#...�A�Ѽg�r��ɶ��[�W"(���e)"
    //���L��  bool    �ҡGtrue, false

    //�w�q���
    //Unity �H�ݩ� Inspector���O�W���Ȭ��D
    public float weight = 3.5f;
    public int CC = 2000;
    public string brand = "���h";
    public bool windowSky = true;

    //�i�H�ϥΤ���W�r�A����ĳ - �s�X���D�P�ഫ�į���D
    //�W�߶}�o�B�ζ��\�i
    public int ���L�ƶq = 4;

    //����ݩʡG���U���K�[�B�~�\��
    //�y�k�G[�ݩʦW��(�ݩʭ�)]
    //���D�G[Header(�r��)]
    [Header("���L���D")]
    public int wheelCount = 4;
    //���ܡG[Tooltip(�r��)]
    [Tooltip("�o����쪺�@�άO�]�w�T��������...")]
    public float height = 1.5f;
    //�d��G[Range(�̤p�ƭȡA�̤j�ƭ�)] - �ȭ��ƭ����� float �P int
    [Range(2, 10)]
    public int doorCount;
    #endregion

    #region ��L����
    //�C��GColor
    public Color color1;                                            //�����w���¦�z��
    public Color red = Color.red;                                   //�ϥιw�]�C��
    public Color colorCustom1 = new Color(0.5f, 0.5f, 0);           //�ۭq�C��(R�AG�AB)
    public Color colorCustom22 = new Color(0.5f, 0, 0.5f, 0.5f);    //�ۭq�C��(R�AG�AB�AA)

    //�y�� 2 - 4 �� Vector2 - 4
    //�O�s�ƭȸ�T�A�B�I��
    public Vector2 v2;
    public Vector2 v2Zero = Vector2.zero;
    public Vector2 v2One = Vector2.one;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2Right = Vector2.right;
    public Vector2 v2Custom = new Vector2(-99.5f, 100.5f);

    public Vector3 v3;
    public Vector4 v4;

    //�ץ�����
    public KeyCode kc;
    public KeyCode forward = KeyCode.D;
    public KeyCode attack = KeyCode.Mouse0; // ����0�A�k��1�A�u��2

    //�C������P����
    public GameObject goCamera;           //�C������]�t�����W�H�αM�פ����w�s���A�Y�g��go��obj
    //����ȭ���s���ݩʭ��O�������󪺪���
    public Transform traCar;              //�Y�gtra
    public SpriteRenderer sprPicture;     //�Y�gspr

    #endregion

    #region �ƥ�
    //�}�l�ƥ�G����C���ɰ���@���A�B�z��l��
    private void Start()
    {
        #region �m�����
        //��X(�����������);
        print("���o�AWorld");

        //�m�ߨ��o���Get
        print(brand);
        //�m�߳]�w���Set
        windowSky = true;
        CC = 5000;
        weight = 9.9f;
        #endregion

        //�I�s��k�y�k�G��k�W��()�F
        Drive50();
        Drive100();
        Drive(150, "������");                              //�I�s�ɤp�A�������٬��޼�
        Drive(200, "�F�F�F");
        Drive(300);                                       //���w�]�Ȫ��Ѽƥi�H���ε��޼�
        //Drive(80, "�H��");                              //�ɳt 80�A���� �������A�S�� �H�� - ���~
        Drive(80, effect: "�H��");                        //�ϥΦh�ӹw�]�ȰѼƮɥi�H�ϥ� �ѼƦW��: ��
        Drive(999, "�N�N�N", "�z��");

        float kg = KG();                                 //�ϰ��ܼơA�Ȧb���A�����ϥ�
        print("�ର���窺��T�G" + kg);                    //�����N�Ǧ^��k���Ȩϥ�

        print("KID �� BMI�G" + BMI(60, 1.68f));
    }

    //��s�ƥ�G�j���@��60���A60FPS�A�B�z���󲾰ʩΪ̺�ť���a����J
    private void Update()
    {
        print("�ڦbUpdte��");
    }
    #endregion

    #region ��k(�\��B�禡) Method
    //��k�G��@����������欰�A�Ҧp�G�T�����e�}�B�}�ҨT�����T�ü��񭵼�......
    //���y�k�G�׹��� ��    �� �W�� ���w �w�]�ȡF
    //��k�y�k�G�׹��� �Ǧ^���� �W�� (...) { �{���϶� }
    //�����Gviod - �L�Ǧ^
    //�w�q��k�A���|���檺�����I�s�A�I�s���覡�G�b�ƥ󤺩I�s����k
    //���@�ʡB�X�R��
    private void Drive50()
    {
        print("�}�����A�ɳt�G50");    
    }

    private void Drive100()
    {
        print("�}�����A�ɳt�G100");
    }
    //�Ѽƻy�k�G���� �ѼƦW�� - �g�b�p�A����
    //�Ѽ�1�B�Ѽ�2�B�Ѽ�3......�Ѽ�N
    //�Ѽƹw�]�ȡG���� �ѼƦW�� ���w �� (��񦡰Ѽ�)
    //���w�]�ȥu���b�̥k��

    /// <summary>
    /// �o�O�}������k�A�i�H�Ψӱ���l���t�סB���ĻP�S�ġC
    /// </summary>
    /// <param name="speed">���l���}�ʳt��</param>
    /// <param name="sound">�}���ɪ�����</param>
    /// <param name="effect">�}���ɭn���񪺯S��</param>
    private void Drive(int speed, string sound = "������", string effect = "�ǹ�")
    {
        print("�}�����A�ɳt�G" + speed);
        print("�}�����ġG" + sound);
        print("�}���S�ġG" + effect);
    }

    /// <summary>
    /// �����ഫ������
    /// </summary>
    /// <returns>�ର���窺���q��T</returns>
    private float KG()
    {
        return weight * 1000;
    }

    /// <summary>
    /// �p��BMI
    /// </summary>
    /// <param name="weight">�魫(����)</param>
    /// <param name="height">����(����)</param>
    /// <returns>BMI��</returns>
    private float BMI(float weight, float height)
    {
        return weight / (height * height);
    }
    #endregion

}

