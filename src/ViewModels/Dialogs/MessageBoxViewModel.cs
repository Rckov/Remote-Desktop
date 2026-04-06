using CommunityToolkit.Mvvm.ComponentModel;

using RemoteDesktop.Common.Attributes;
using RemoteDesktop.Views.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktop.ViewModels.Dialogs;

[Window(typeof(MessageBoxDialog))]
internal class MessageBoxViewModel : ObservableObject
{
}
