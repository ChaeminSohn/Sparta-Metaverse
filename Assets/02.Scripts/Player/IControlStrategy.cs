using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControlStrategy
{
    void Enter(PlayerCtrl player); // 전략 시작 시 호출
    void Exit();                   // 전략 종료 시 호출
    void ProcessMovement(InputAction.CallbackContext context); // 이동 입력 처리
    void ProcessTurn(InputAction.CallbackContext context); //회전 입력 처리
    void ProcessJump(InputAction.CallbackContext context); // 점프 입력 처리 (Context 필요시)
    // 필요에 따라 다른 액션 처리 메서드 추가 (예: ProcessAttack)
    void UpdateStrategy();         // 매 프레임 로직 (Update)
    void FixedUpdateStrategy();    // 물리 관련 로직 (FixedUpdate)
}
