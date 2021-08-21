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

    protected override void Update()
    {
        base.Update();

        CheckPlayerInAttackArea();
    }
    #endregion

    #region ��k
    /// <summary>
    /// �ˬd���a�O�_�i�J�����ϰ�
    /// </summary>
    private void CheckPlayerInAttackArea()
    {
        hit = Physics2D.OverlapBox(transform.position + 
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
    /// ������P�_�P�M�w���A���� - �����Ψ���
    /// </summary>
    private IEnumerator DelaySendDamageToPlayer()
    {
        // �h���{���ֱ���GAlt + �W�ΤU
        // �榡�Ʊƪ��ֱ���GCtrl + K D

        // ���o�}�C�ƶq�y�k�G�}�C.Length
        for (int i = 0; i < attackDelay.Length; i++)
        {
            // ���o�}�C��ƻy�k�G�}�C���W��[�s��]
            yield return new WaitForSeconds(attackDelay[i]);      // ����ɶ�

            if (hit) player.Hurt(attack);                           // �p�G�I����T�s�b�A�N�缾�a�y���ˮ`

        }

        // ���ݧ������_�쥻���A�ɶ� - �����ʵe�̫᪺�ɶ�
        yield return new WaitForSeconds(afterAttackRestoreOriginal);
        // �p�G ���a�٦b�����ϰ줺 �N���� �_�h �N����
        if (hit) state = StateEnemy.attack;
        else state = StateEnemy.walk;

    }
    #endregion
}
