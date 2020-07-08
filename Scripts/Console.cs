using Godot;
using System;
using System.Linq;

public class Console : Control
{
    //The output text field for the consol
    TextEdit outputField;
    //The text input field for the console
    LineEdit inputField;
    //Refference to the command handler script in the console
    CommandHandler commandHandler;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        outputField = GetNode<TextEdit>("Output");
        inputField = GetNode<LineEdit>("Input");
        commandHandler = GetNode<CommandHandler>("CommandHandler");
        //Set the focus on the input field
        inputField.GrabFocus();
    }

    public void OnInputTextEntered(String newText)
    {
        //Clear the input field when enter is pressed
        inputField.Clear();
        //Send the input to the command method 
        ProcessCommand(newText);
    }

    private void OutputText(String text)
    {
        //Outputs all the old text from the output field wit hte new command added on the bottom of the list
        outputField.Text = outputField.Text + "\n" + text;
    }

    private void ProcessCommand(String text)
    {
        //Spilt all the words that are sent for precessing and strip any empty strings
        string[] words = text.Split(" ", false);

        if (words.Length <= 0) return;

        String commandWord = words[0];

        foreach (var command in commandHandler.ValidCommands)
        {
            if (command.Key == commandWord)
            {
                if (command.Value.Count == words.Length -1)
                {
                    OutputText("Number of command arguments correct:");
                }
                else
                {
                    OutputText("Number of command arguments not correct, looking for:");
                    foreach (var argument in command.Value)
                    {
                        OutputText(argument.ToString());
                    }
                }
                break;
            }
            else if(command.Equals(commandHandler.ValidCommands.Last()))
            {
                OutputText("Command not recognised: " + commandWord);
            }
        }
        //Output the commands to the output text box
        OutputText(text);
    }

    private bool CheckType(CommandHandler.Arguments commandType, String type)
    {
        switch (commandType)
        {
            case CommandHandler.Arguments.ARG_BOOL:
                //type.isValidBool();
                return true;
            case CommandHandler.Arguments.ARG_FLOAT:
                type.IsValidFloat();
                return true;
            case CommandHandler.Arguments.ARG_INT:
                type.IsValidInteger();
                return true;
            case CommandHandler.Arguments.ARG_STRING:
                return true;
            default:
                return false;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
