# About States

This folder contains all the states used in the State Machine. The states are used to represent the different states of the conversation. In this example, the states are:
* `DaySate` - Represents the the first input: Day
* `MonthState` - Represents the second input: Month
* `YearState` - Represents the third input: Year
* `CompletedState` - Represents the state after all the inputs are entered.

## Remarks
* This States do not check for the date to be valid. So 31/2/2022 will be accepted.
  * Basically, it is a simple state machine. That only asks for three inputs and places them in a string.

