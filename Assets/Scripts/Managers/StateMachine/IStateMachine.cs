public interface IStateMachine {
    void Initialize(IState entityState);
    void ChangeState(IState entityState);
}