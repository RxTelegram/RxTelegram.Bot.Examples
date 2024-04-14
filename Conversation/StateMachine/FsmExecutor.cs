using RxTelegram.Bot.Examples.Conversation.StateMachine.States;

namespace RxTelegram.Bot.Examples.Conversation.StateMachine;

public class FsmExecutor
{
    private IState _currentState = new AwaitingDayState(); // Initial state

    public string HandleInput(string input)
    {
        _currentState = _currentState.ProcessInput(input, out var response);
        return response;
    }

    public string GetCurrentPrompt()
    {
        return _currentState.GetPrompt();
    }
    
    public bool IsCompleted()
    {
        return _currentState.IsCompleted();
    }
}