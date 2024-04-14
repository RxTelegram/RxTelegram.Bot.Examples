namespace RxTelegram.Bot.Examples.Conversation.StateMachine.States;

/// <summary>
/// This state is used when the user has not entered the day yet.
/// </summary>
public class AwaitingDayState : IState
{
    public IState ProcessInput(string input, out string response)
    {
        if (int.TryParse(input, out var day) && day is > 0 and <= 31)
        {
            response = "";
            
            // The next state is the awaiting month state
            return new AwaitingMonthState(day);
        }

        response = "Invalid day, please try again.";
        return this;
    }

    public string GetPrompt()
    {
        return "Please enter the day:";
    }

    public bool IsCompleted() => false;
}