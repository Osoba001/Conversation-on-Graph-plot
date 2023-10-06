using GroupChatDemo.Services;
using GroupChatDemo.Services.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GroupChatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IChatServices _chatServices;

        public ChartsController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] string name)=> ActionResponse(await _chatServices.CreateUser(name));

        [HttpGet("user")]
        public async Task<IActionResult> FetchUsers()=>ActionResponse(await _chatServices.FetchUsers());

        [HttpPost("plot")]
        public async Task<IActionResult> CreatePlot([FromBody] CreatePlotCommand command)=> ActionResponse( await _chatServices.CreatePlote(command));

        [HttpGet("plot")]
        public async Task<IActionResult> FetchPlot()=> ActionResponse( await _chatServices.FetchPlots());

        [HttpPost("conversation")]
        public async Task<IActionResult> StarConversation([FromBody] CreateConversationCommand command)=>ActionResponse( await _chatServices.StartConversation(command));

        [HttpGet("conversation/{plotId}")]
        public async Task<IActionResult> FetchConversation(Guid plotId)=> ActionResponse(await _chatServices.FetchConversations(plotId));

        [HttpPut("add-user-to-conversation")]
        public async Task<IActionResult> AddUserToCoversation([FromBody] AddUserToCoversationCommand command)=>ActionResponse(await _chatServices.AddUsersToConversation(command));

        [HttpPut("remove-user-from-conversation")]
        public async Task<IActionResult> RemoveUserFromCoversation([FromBody] RemoveUsersFromCoversationCommand command) => ActionResponse(await _chatServices.RemoveUsersFromConversation(command));

        [HttpGet("message/{conversationId}")]
        public async Task<IActionResult> FetchMessage(Guid conversationId) => ActionResponse(await _chatServices.FetchMessages(conversationId));

        [HttpPut("delete-message")]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageCommand command)=> ActionResponse(await _chatServices.DeleteMessages(command.Ids));



        private IActionResult ActionResponse(ActionResponse response)
        {
            if (response.ResponseType == ResponseType.Success)
                return Ok(response.PayLoad);
            if(response.ResponseType==ResponseType.NotFound) 
                return NotFound(response.ReasonPhrase);
            if (response.ResponseType == ResponseType.ServerError)
                return Problem(response.ReasonPhrase);
            return BadRequest(response.ReasonPhrase);
        }
    }
}
