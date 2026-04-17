namespace RemoteDesktop.Models.Themes;

public class ThemeDescriptor(ThemeType type, string url)
{
	public string Url { get; } = url;
	public ThemeType ThemeType { get; } = type;
}