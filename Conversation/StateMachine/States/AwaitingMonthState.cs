namespace RxTelegram.Bot.Examples.Conversation.StateMachine.States;

/// <summary>
/// This state is used when the user has not entered the month yet.
/// </summary>
public class AwaitingMonthState : IState
{
    private readonly int _day;

    public AwaitingMonthState(int day)
    {
        _day = day;
    }

    public IState ProcessInput(string input, out string response)
    {
        if (int.TryParse(input, out var month) && month is > 0 and <= 12)
        {
            response = "";
            
            // The next state is the awaiting year state
            return new AwaitingYearState(_day, month);
        }
        response = "Invalid month, please try again.";
        return this;
    }

    public string GetPrompt()
    {
        return "Please enter the month:";
    }
    
    public bool IsCompleted() => false;
}