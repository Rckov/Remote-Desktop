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

    }

    public void ErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void SuccessMessage(string message)
    {
        throw new NotImplementedException();
    }
}
