using CommandCliApi.Data;
using CommandCliApi.Models;
using CommandCliApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommandCliApi.Services
{
    public class CommandService : ICommandService
    {
        private readonly ApplicationDbContext _context;

        // TODO make calls async
        // TODO wrap in try/catch
        public CommandService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Command>> GetAllCommandsAsync()
        {
            return await _context.Commands.ToListAsync();
        }

        public async  Task<Command> GetCommandByIdAsync(int id)
        {
            return await  _context.Commands.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task CreateCommandAsync(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await _context.Commands.AddAsync(command);
        }

        public void UpdateCommand(Command command)
        {
            // EF will handle update by default
        }

        public void DeleteCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Remove(command);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
