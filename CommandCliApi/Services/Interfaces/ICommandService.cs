using CommandCliApi.Models;

namespace CommandCliApi.Services.Interfaces
{
    public interface ICommandService
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Command>> GetAllCommandsAsync();
        Task<Command> GetCommandByIdAsync(int id);
        Task CreateCommandAsync(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}
