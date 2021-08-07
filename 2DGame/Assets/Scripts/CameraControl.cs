using UnityEngine;

/// <summary>
/// ��v���l�ܪ��a
/// </summary>
public class CameraControl : MonoBehaviour
{
    #region ���
    [Header("�l�ܳt��"),Range(0,100)]
    public float speed = 10;
    [Header("�n�l�ܪ�����W��")]
    public string nameTarger;
    [Header("���k����")]
    public Vector2 limitHorizontal;

    /// <summary>
    /// �n�l�ܪ��ؼ�
    /// </summary>
    private Transform target;
    #endregion

    #region �ƥ�
    private void Start()
    {
        // �� �ܦY�į�A�ҥH��ĳ�b Start ���ϥ�
        // �ؼ��ܧΤ��� = �C������.�M��(����W��).�ܧΤ���
        target = GameObject.Find(nameTarger).transform;
    }

    // ���C��s�G�b Update �����A��ĳ�ΨӳB�z��v��
    private void LateUpdate()
    {
        Track();
    }
    #endregion

    #region ��k
    /// <summary>
    /// �l�ܥؼ�
    /// </summary>
    private void Track()
    {
        Vector3 posCamera = transform.position;     // A �I�G��v���y��
        Vector3 posTarget = target.position;        // B �I�G�ؼЪ��y��

        // ���o A �I ��v�� �P B �I �ؼЪ� �������y��
        Vector3 posResult = Vector3.Lerp(posCamera, posTarget, speed * Time.deltaTime);
        // ��v�� Z �b��^�w�] -10 �קK�ݤ��� 2D ����
        posResult.z = -10;

        // �ϥΧ��� API ���� ��v�� �� ���k�d��
        posResult.x = Mathf.Clamp(posResult.x, limitHorizontal.x, limitHorizontal.y);

        // ������y�� ���w�� �B��᪺���G�y��
        transform.position = posResult;
    }
    #endregion
}
