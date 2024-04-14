namespace RxTelegram.Bot.Examples.Conversation.StateMachine.States;

/// <summary>
/// This state is used when the user has not entered the year yet.
/// </summary>
public class AwaitingYearState : IState
{
    private readonly int _day;
    private readonly int _month;

    public AwaitingYearState(int day, int month)
    {
        _day = day;
        _month = month;
    }

    public IState ProcessInput(string input, out string response)
    {
        if (int.TryParse(input, out var year))
        {
            response = $"You entered the date: {_day}/{_month}/{year}.";
            
            // The next state is the completed state
            return new CompletedState();
        }
        response = "Invalid year, please try again.";
        return this;
    }

    public string GetPrompt()
    {
        return "Please enter the year:";
    }
    
    public bool IsCompleted() => false;
}