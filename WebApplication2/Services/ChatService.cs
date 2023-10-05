using GroupChatDemo.Database;
using GroupChatDemo.Database.Entities;
using GroupChatDemo.Hubs;
using GroupChatDemo.Services.Commands;
using Microsoft.EntityFrameworkCore;

namespace GroupChatDemo.Services
{
    internal class ChatServices : IChatServices
    {
        private readonly GroupChatDbContext _dbContext;
        private readonly ChatHub _ChatHub;
        public ChatServices(GroupChatDbContext dbContext, ChatHub chatHub)
        {
            _dbContext = dbContext;
            _ChatHub = chatHub;
        }
        public async Task<ActionResponse> CreatePlote(CreatePlotCommand command)
        {
            var plot = new GraphPlot
            {
                Name = command.Name,
                PlotInitiatorId = command.PlotInitiatorId
            };
            _dbContext.Plots.Add(plot);
            return await Complete();
        }
        public async Task<ActionResponse> CreateUser(string name)
        {
            name=name.ToLowerInvariant();   
            var user= await _dbContext.Users.SingleOrDefaultAsync(x => x.Name == name);
            if (user != null)
                return new ActionResponse("user name is in used alread.");
            user =new User { Name = name };
            _dbContext.Users.Add(user);
            return await Complete();
        }

        public async Task<ActionResponse> FetchUsers()
        {
            return new ActionResponse
            {
                PayLoad = await _dbContext.Users.Select(u => new
                {
                    u.Name,
                    u.Id,
                }).ToListAsync(),
            };
        }

        public async Task<ActionResponse> DeleteMessages(List<Guid> ids)
        {
            var msgs = await _dbContext.Messages.Where(m => ids.Contains(m.Id)).ToListAsync();
            _dbContext.Messages.RemoveRange(msgs);
            return await Complete();
        }

        public async Task<ActionResponse> FetchConversations(Guid plotId)
        {
            var conversations = await _dbContext.Conversation.Where(x => x.GraphPlotId == plotId)
                .Select(p => new
                {
                    p.GraphPlotId,
                    p.XCoordinate,
                    p.YCoordinate,
                }).ToListAsync();
            return new ActionResponse { PayLoad = conversations };

        }

        public async Task<ActionResponse> FetchMessages(Guid conversationId)
        {
            var message = await _dbContext.Messages.Where(m => m.Conversation.Id == conversationId)
                .Include(m => m.Conversation)
                .Include(m => m.Sender)
                .Select(m => new
                {
                    m.Id,
                    m.CreatedTime,
                    m.IsEdited,
                    m.Text,
                    ConversationId = m.Conversation.Id,
                    SenderName = m.Sender.Name
                })
                .ToListAsync();

            return new ActionResponse { PayLoad = message };
        }

        public async Task<ActionResponse> FetchPlots()
        {
            var plots = await _dbContext.Plots.
                Select(p => new { p.Name, p.PlotInitiatorId, p.Id }).ToListAsync();
            return new ActionResponse { PayLoad = plots };
        }

        public async Task<ActionResponse> SendMessage(CreateMessageCommand command)
        {
            var message = new Message
            {
                ConversationId = command.ConversationId,
                CreatedTime = DateTime.Now,
                UserId = command.SendId,
                Text = command.Text,
            };
            _dbContext.Messages.Add(message);
            await Complete();

            var msg = new
            {
                message.Id,
                message.CreatedTime,
                message.IsEdited,
                message.Text,
                command.ConversationId,
                command.SenderName,
            };
            await _ChatHub.SendMessageToGroup(command.ConversationId.ToString(), msg);
            return new ActionResponse { PayLoad = message.Id };
        }

        public async Task<ActionResponse> StartConversation(CreateConversationCommand command)
        {
            var members = await _dbContext.Users.Where(u => command.MembersId.Contains(u.Id)).ToListAsync();
            var conversation = new Conversation
            {
                GraphPlotId = command.GraphPlotId,
                XCoordinate = command.XCoordinate,
                YCoordinate = command.YCoordinate,
            };

            _dbContext.Conversation.Add(conversation);
            return await Complete();

        }

       

        public async Task<ActionResponse> AddUsersToConversation(AddUserToCoversationCommand command)
        {
            var conversation = await _dbContext.Conversation.FindAsync(command.ConversationId);
            if (conversation == null)
                return new ActionResponse("No conversation found with the given id.", ResponseType.NotFound);

            var users= await _dbContext.Users.Where(u=>command.UserIds.Contains(u.Id)).ToListAsync();
            foreach (var id in command.UserIds)
            {
                var member=new ConversationMember { ConversationId = command.ConversationId, MemberId=id };
                _dbContext.ConversationMembers.Add(member);
            }
            return await Complete();
        }

        public async Task<ActionResponse> RemoveUsersFromConversation(RemoveUsersFromCoversationCommand command)
        {
            var conversation = await _dbContext.Conversation.FindAsync(command.ConversationId);
            if (conversation == null)
                return new ActionResponse("No conversation found with the given id.", ResponseType.NotFound);

            var users = await _dbContext.ConversationMembers.Where(u => command.UserIds.Contains(u.MemberId) && u.ConversationId==command.ConversationId).ToListAsync();
            foreach (var user in users)
            {
                _dbContext.ConversationMembers.Remove(user);
            }
            return await Complete();
        }

        private async Task<ActionResponse> Complete()
        {
            int affectedRow;
            try
            {
                affectedRow = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return new ActionResponse { Exception = ex };
            }
            return new ActionResponse { PayLoad = affectedRow };
        }
    }
}
