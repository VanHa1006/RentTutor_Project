using Microsoft.AspNetCore.SignalR;

public class TutorHub : Hub
{
	// Phương thức này có thể gọi từ phía client để thông báo admin
	public async Task NotifyAdminAboutNewRegistration()
	{
		await Clients.All.SendAsync("ReceiveNewTutorRegistration");
	}
}