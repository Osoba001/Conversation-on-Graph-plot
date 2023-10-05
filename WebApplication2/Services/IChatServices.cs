using GroupChatDemo.Services.Commands;

namespace GroupChatDemo.Services
{
    public interface IChatServices
    {
        Task<ActionResponse> CreatePlote(CreatePlotCommand command);
        Task<ActionResponse> DeleteMessages(List<Guid> ids);
        Task<ActionResponse> FetchConversations(Guid plotId);
        Task<ActionResponse> FetchMessages(Guid conversationId);
        Task<ActionResponse> FetchPlots();
        Task<ActionResponse> SendMessage(CreateMessageCommand command);
        Task<ActionResponse> StartConversation(CreateConversationCommand command);
        Task <ActionResponse> AddUsersToConversation(AddUserToCoversationCommand command);
        Task <ActionResponse> RemoveUsersFromConversation(RemoveUsersFromCoversationCommand command);
        Task<ActionResponse> FetchUsers();
        Task<ActionResponse> CreateUser(string name);
    }
}