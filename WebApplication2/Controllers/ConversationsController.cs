using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroupChatDemo.Models;
using GroupChatDemo.DTOs;

namespace GroupChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly GroupChatDbContext _context;

        public ConversationsController(GroupChatDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
          if (_context.Conversations == null)
          {
              return NotFound();
          }
            return await _context.Conversations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
          if (_context.Conversations == null)
          {
              return NotFound();
          }
            var conversation = await _context.Conversations.FindAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddMembersToConversation(int id, IEnumerable<int> members)
        {
            var conversation = await _context.Conversations.Where(x=>x.Id==id).Include(x=>x.Members).FirstOrDefaultAsync();
            if(conversation == null)
                return NotFound();

            var users= await _context.Users.Where(x=>members.Contains(id)).ToListAsync();
            users= users.ExceptBy(conversation.Members,x=>x).ToList();
            conversation.Members.AddRange(users);
            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Conversation>> PostConversation(CreateConversationCommand command)
        {
          if (_context.Conversations == null)
          {
              return Problem("Entity set 'GroupChatDbContext.Conversations'  is null.");
          }
          var members= await _context.Users.Where(x=>command.MembersId.Contains(x.Id)).ToListAsync();
          Conversation conversation = new Conversation { GraphPlotId= command.GraphPlotId, XCoordinate=command.XCoordinate, YCoordinate=command.YCoordinate, Members=members };
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversation", new { id = conversation.Id }, conversation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            if (_context.Conversations == null)
            {
                return NotFound();
            }
            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversationExists(int id)
        {
            return (_context.Conversations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
