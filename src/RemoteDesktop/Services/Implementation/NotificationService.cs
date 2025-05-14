using HandyControl.Controls;
using HandyControl.Data;

using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktop.Services.Implementation;
internal class NotificationService : INotificationService
{
    public void InfoMessage(string message)
    {
        Growl.Info(GetGrowInfo(message));
    }

    public void ErrorMessage(string message)
    {
        Growl.Error(GetGrowInfo(message));
    }

    public void SuccessMessage(string message)
    {
        Growl.Success(GetGrowInfo(message));
    }

    private GrowlInfo GetGrowInfo(string message)
    {
        return new GrowlInfo
        {
            Message = message,
            ShowDateTime = false,
            StaysOpen = false,
            ShowCloseButton = true,
            WaitTime = 1
        };
    }
}
