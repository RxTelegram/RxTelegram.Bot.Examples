namespace RxTelegram.Bot.Examples.Conversation.StateMachine.States;

/// <summary>
/// This interface is used to define the state machine. Each state should have its own ProcessInput method.
/// </summary>
public interface IState
{
    IState ProcessInput(string input, out string response);

    string GetPrompt();

    bool IsCompleted();
}