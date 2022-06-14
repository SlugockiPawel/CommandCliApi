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

        public IEnumerable<Command> GetAllCommands()
        {
            return  _context.Commands.ToList();
        }

        public  Command GetCommandById(int id)
        {
            return  _context.Commands.FirstOrDefault(c => c.Id.Equals(id));
        }

        public void CreateCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Add(command);
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

        public void SaveChanges()
        {
           _context.SaveChanges();
        }
    }
}
