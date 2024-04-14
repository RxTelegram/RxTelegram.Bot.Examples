# About the Finite State Machine (FSM)

This folder demonstrates the implementation of a Finite State Machine (FSM) in C# to manage a chatbot's user interaction for collecting a date piece-by-piece—first the day, then the month, and finally the year.

## Overview

The FSM is implemented using object-oriented programming to encapsulate each state's behavior in its own class. This approach avoids using a switch statement for state transitions and makes the system flexible and easy to maintain or extend.

## Key Components

### 1. State Interface (`IState`)

- **Purpose**: Defines a common interface for all state classes.
- **Methods**:
    - `ProcessInput(string input, out string response)`: Handles user input and transitions to the next state.
    - `GetPrompt()`: Returns the prompt message for the current state.
    - `IsCompleted()`: Returns `true` if the state is in a completed state.

### 2. State Classes

- `AwaitingDayState`, `AwaitingMonthState`, `AwaitingYearState`, `CompletedState`
- **Behavior**: Each state class implements the `IState` interface and manages the logic for that particular part of the date collection process.
- **Transitions**: Each state returns the next appropriate state based on user input, or remains in the current state if the input is invalid.

### 3. Fsm Executor (`FsmExecutor`)

- **Purpose**: Manages the current state and transitions based on user input.
- **Core Methods**:
    - `HandleInput(string input)`: Passes input to the current state and manages state transitions.
    - `GetCurrentPrompt()`: Retrieves the current prompt from the state to guide the user.
    - `IsCompleted()`: Returns `true` if the FSM is in a completed state.

## Usage

The FSM can be used in any C# application where a date needs to be collected from a user input sequentially. To do so, the following steps need to be followed:
- Initialize the `DateCollectorFSM`.
- Use a loop to continuously accept user input until the date collection is completed and `IsCompleted()` returns `true`.

## Future Enhancements

- **Validation Improvements**: Add more sophisticated validation for dates (e.g., checking for valid dates within each month, leap year considerations).
- **Localization Support**: Adapt prompts and validations to support different cultural formats for dates.
- **Error Handling Enhancements**: Implement better user feedback mechanisms for error states.
- **Serialization Support**: Support serialization of the FSM state to a file or database.
- **Logging Support**: Support logging of state transitions and errors.
- **User Interface Enhancements**: Add more user-friendly prompts and error messages.
- **Testing Support**: Support unit testing of the FSM and its state classes.