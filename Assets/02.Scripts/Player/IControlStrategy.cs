using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControlStrategy
{
    void Enter(PlayerCtrl player); // ���� ���� �� ȣ��
    void Exit();                   // ���� ���� �� ȣ��
    void ProcessMovement(Vector2 input); // �̵� �Է� ó��
    void ProcessJump(InputAction.CallbackContext context); // ���� �Է� ó�� (Context �ʿ��)
    // �ʿ信 ���� �ٸ� �׼� ó�� �޼��� �߰� (��: ProcessAttack)
    void UpdateStrategy();         // �� ������ ���� (Update)
    void FixedUpdateStrategy();    // ���� ���� ���� (FixedUpdate)
}
