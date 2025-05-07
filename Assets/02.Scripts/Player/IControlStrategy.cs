using UnityEngine.InputSystem;

public interface IControlStrategy
{
    void Enter(PlayerCtrl player); // ���� ���� �� ȣ��
    void Exit();                   // ���� ���� �� ȣ��
    void ProcessMovement(InputAction.CallbackContext context); // �̵� �Է� ó��
    void ProcessTurn(InputAction.CallbackContext context); //ȸ�� �Է� ó��
    void ProcessJump(InputAction.CallbackContext context); // ���� �Է� ó�� 
    void UpdateStrategy();         // �� ������ ���� (Update)
    void FixedUpdateStrategy();    // ���� ���� ���� (FixedUpdate)
}



