public interface IState //각각의 상태가 상속하게 될 인터페이스
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

public abstract class StateMachine //각 스탯머신이 상속할 기본형
{
    //현재 상태 - 인터페이스를 통해 각 필요한 부분을 제현
    protected IState currentState;

    //스탯 변경
    public void ChangeState(IState newState)
    {
        //지금 상태의 탈출 로직을 실행 후 새 상태를 적용하고 상태 입장
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    //입력 처리
    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    //상태 업데이트
    public void Update()
    {
        currentState?.Update();
    }

    //물리 업데이트
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
