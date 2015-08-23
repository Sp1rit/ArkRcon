namespace Rcon.Commands
{
    interface ICommand
    {
        CommandType Type { get; }

        bool ValidateResponse(string responseBody);

        string ToString();
    }
}
