using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktop.Services.Interfaces;
internal interface INotificationService
{
    void InfoMessage(string message);
    void SuccessMessage(string message);
    void ErrorMessage(string message);
}
