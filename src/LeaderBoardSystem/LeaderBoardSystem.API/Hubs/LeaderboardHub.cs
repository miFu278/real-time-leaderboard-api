using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LeaderboardSystem.API.Hubs
{
    [Authorize]
    public class LeaderboardHub : Hub
    {
        public async Task JoinGlobalLeaderboard()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "global");
            await Clients.Caller.SendAsync("JoinedGlobalLeaderboard", "Successfully joined global leaderboard");
        }

        public async Task LeaveGlobalLeaderboard()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "global");
        }

        public async Task JoinGameLeaderboard(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"game:{gameId}");
            await Clients.Caller.SendAsync("JoinedGameLeaderboard", $"Successfully joined game {gameId} leaderboard");
        }

        public async Task LeaveGameLeaderboard(string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"game:{gameId}");
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.User?.Identity?.Name ?? "Anonymous";
            await Clients.Caller.SendAsync("Connected", $"Welcome {username}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
