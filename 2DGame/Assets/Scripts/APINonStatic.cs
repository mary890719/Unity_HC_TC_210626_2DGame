using UnityEngine;

public class APINonStatic : MonoBehaviour
{
    //API ���A������j��
    //1. �R  �A�G������r static
    //2. �D�R�A�G�L����r static

    // �ϥΫD�R�A�ݩ� 1. ���w�q���D�R�A�ݩʪ����O���
    // �ϥΫD�R�A�ݩ� 3. ��쥲����J�n���o����T������A�� ���ର�ŭ�
    public Transform traA;
    public Camera cam;
    public Transform traB;
    public Light lightA;

    public Camera camA;
    public SpriteRenderer srA;
    public Transform traC;
    public Rigidbody2D rigA;

    private void Start()
    {
        #region �{�ѫD�R�A�ݩʻP��k
        // 1. ���o�D�R�A�ݩ�

        // print("���o�y�СG" + Transform.position); // ���~�G�ݭn������Ѧ�

        // �ϥΫD�R�A�ݩ� 2. ��J���o�y�k �� �y�k�G���.�D�R�A�ݩ�
        print("���o�ߤ���y�СG" + traA.position);
        print("���o��v�����I���C��G" + cam.backgroundColor);

        // 2. �]�w�D�R�A�ݩ�
        //�� �y�k�G���.�D�R�A�ݩ� ���w ��;
        cam.backgroundColor = new Color(0.8f, 0.5f, 0.6f);

        // 3. �I�s�D�R�A��k
        //�� �y�k�G���.�D�R�A��k(�������޼�);
        traB.Translate(1, 0, 0);
        lightA.Reset();
        #endregion

        #region �m���R�A�ݩʻP��k
        //���o
        print("���o��v�����`��" + camA.depth);
        print("���o�Ϥ� 1 ���C��" + srA.color);
        //�]�w
        camA.backgroundColor = Random.ColorHSV();
        srA.flipY = true;
        #endregion
    }
    private void Update()
    {
        //�ϥ�
        traC.Rotate(0, 0, 1);
        rigA.AddForce(new Vector2(0, 10));
    }

}
