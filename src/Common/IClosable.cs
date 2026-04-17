using System.Windows;

namespace RemoteDesktop.Common;

public interface IClosable
{
	WindowState WindowState { get; set; }

	void Close();
}