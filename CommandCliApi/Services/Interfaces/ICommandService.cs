using CommandCliApi.Models;

namespace CommandCliApi.Services.Interfaces
{
    public interface ICommandService
    {
        void SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}
