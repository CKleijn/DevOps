using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Tools;

namespace Domain.Services;

public class NotificationService
{
    private Notification _context { get; init; }
    
    public NotificationService(Notification context)
    {
        _context = context;
    }

    public void SendNotification()
    {
        var instances = AssemblyScanner.GetInstancesOfType<INotificationAdapter>();
        
        AssemblyScanner.GetInstancesOfType<INotificationAdapter>()
            .ToList()
            .ForEach(adapter =>
            {
                if (_context.Recipient!.DestinationTypes.Contains(adapter.Type))
                {
                    adapter.SendMessage(_context);
                }
            });
        
        Logger.DisplayCustomAlert(nameof(NotificationService), nameof(SendNotification), "All notifications sent successfully!");
    }
}