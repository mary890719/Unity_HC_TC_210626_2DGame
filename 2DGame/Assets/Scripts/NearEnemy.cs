using UnityEngine;
using System.Collections;   // �ޥ� �t��.���X - ��P�{��

/// <summary>
/// ��Z�������ĤH�����G��Z������
/// </summary>
// ���O �G �����O
// �G �_���᭱�Ĥ@�ӥN��O�n�~�Ӫ����O
public class NearEnemy : BaseEnemy
{
    #region ���
    [Header("�����ϰ쪺�첾�P�j�p")]
    public Vector2 checkAttackOffset;
    public Vector3 checkAttackSize;
    #endregion

    #region �ƥ�
    protected override void OnDrawGizmos()
    {
        //�����O�쥻���{�����e
        base.OnDrawGizmos();

        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(
            transform.position +
            transform.right * checkAttackOffset.x +
            transform.up * checkAttackOffset.y,
            checkAttackSize);
    }
    #endregion

    #region ��k
    /// <summary>
    /// �ˬd���a�O�_�i�J�����ϰ�
    /// </summary>
    private void CheckPlayerInAttackArea()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position + 
            transform.right * checkAttackOffset.x +
            transform.up * checkAttackOffset.y,
            checkAttackSize, 0, 1 << 7);

        // �p�G �I�쪫�󬰪��a �N�N���A�אּ ����
        if (hit) state = StateEnemy.attack;
    }

    protected override void AttackMethod()
    {
        base.AttackMethod();

        StartCoroutine(DelaySendDamageToPlayer());          // �Ұʨ�P�{��
    }

    // ��P�{�ǥΪk�G
    // 1. �ޥ� System.Collections API
    // 2. �Ǧ^��k�A�Ǧ^������ IEnumerator
    // 3. �ϥ� StartCoroutine() �ҥΨ�P�{��
    /// <summary>
    /// ����N�ˮ`�ǵ����a
    /// </summary>
    private IEnumerator DelaySendDamageToPlayer()
    {
        yield return new WaitForSeconds(attackDelayFirst);
        print("�Ĥ@������");
        player.Hurt(attack);
    }
    #endregion
}
