using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0,1000)]
    public float Movespeed = 10.5f;
    [Header("���D����"), Range(0, 3000)]
    public int JumpHeight = 100;
    [Header("��q"), Range(0, 200)]
    public float HP = 100;
    [Header("�O�_�b�a�O�W"), Tooltip("�O�_�b�a�O�W")]
    public bool isGround;

    public AudioSource Aud;
    public Rigidbody2D rig;
    public Animator ani;
    #endregion

    #region �ƥ�

    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="horizontal">���k�ƭ�</param>
    private void Move(float horizontal)
    {

    }

    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {

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
