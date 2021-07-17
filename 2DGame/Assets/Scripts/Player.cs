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
}
