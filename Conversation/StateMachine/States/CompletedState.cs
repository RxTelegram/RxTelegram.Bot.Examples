namespace RxTelegram.Bot.Examples.Conversation.StateMachine.States;

/// <summary>
/// This state is used when the conversation is completed so the state machine should stop.
/// </summary>
public class CompletedState : IState
{
    public IState ProcessInput(string input, out string response)
    {
        response = "";
        return this;
    }

    public string GetPrompt()
    {
        return "";
    }
    
    public bool IsCompleted() => true;
}