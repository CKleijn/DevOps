using System.Collections;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Services;

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
        if (_context.DestinationTypes.Count == 0)
        {
            Logger.DisplayCustomAlert(nameof(NotificationService), nameof(SendNotification), "No destinations types provided.");
            return;    
        }
        
        if (_context.TargetUsers.Count == 0)
        {
            Logger.DisplayCustomAlert(nameof(NotificationService), nameof(SendNotification), "No recipients have been provided.");
            return;    
        }
        
        var instances = AssemblyScanner.GetInstancesOfType<INotificationAdapter>();
        
        AssemblyScanner.GetInstancesOfType<INotificationAdapter>().ToList().ForEach(adapter =>
        {
            if (_context.DestinationTypes.Contains(adapter.Type))
                adapter.SendMessage(_context);
        });
        
        Logger.DisplayCustomAlert(nameof(NotificationService), nameof(SendNotification), "All notifications sent successfully.");
    }
}