using AutoMapper;
using CommandCliApi.DTOs;
using CommandCliApi.Models;
using CommandCliApi.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandCliApi.Controllers
{
    // api/commands
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IMapper _mapper;

        public CommandsController(ICommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        // GET api/commands/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands()
        {
            var commandItems = await _commandService.GetAllCommandsAsync();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }


        // GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            var commandItem = await _commandService.GetCommandByIdAsync(id);

            return commandItem is not null ? Ok(_mapper.Map<CommandReadDto>(commandItem)) : NotFound();
        }

        // POST api/commands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            await _commandService.CreateCommandAsync(commandModel);
            await _commandService.SaveChangesAsync();

            // we need to return commandReadDto with 201 Created status and URI (as per REST api best practices)
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromDb = await _commandService.GetCommandByIdAsync(id);
            if (commandModelFromDb is null)
            {
                return NotFound();
            }

            // the below will update and track changes to the entity by EF core- we need to save changes
            _mapper.Map(commandUpdateDto, commandModelFromDb);

            // but it is good practice to still call Update method, in case the implementation changes in the future
            _commandService.UpdateCommand(commandModelFromDb);

            await _commandService.SaveChangesAsync();

            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromDb = await _commandService.GetCommandByIdAsync(id);
            if (commandModelFromDb is null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromDb);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // if we are here, it means validation is correct, so we can update db
            _mapper.Map(commandToPatch, commandModelFromDb);

            // but it is good practice to still call Update method, in case the implementation changes in the future
            _commandService.UpdateCommand(commandModelFromDb);

            await _commandService.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandModelFromDb = await _commandService.GetCommandByIdAsync(id);
            if (commandModelFromDb is null)
            {
                return NotFound();
            }

            _commandService.DeleteCommand(commandModelFromDb);
            await _commandService.SaveChangesAsync();

            return NoContent();
        }
    }
}
