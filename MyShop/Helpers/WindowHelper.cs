using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace MyShop.Helpers;

public class WindowHelper
{
    public static Window CreateWindow
    {
        get
        {
            var newWindow = new Window();
            TrackWindow(newWindow);
            return newWindow;
        }
    }

    public static void TrackWindow(Window window)
    {
        window.Closed += (sender, args) =>
        {
            _activeWindows.Remove(window);
        };
        _activeWindows.Add(window);
    }

    public static AppWindow GetAppWindow(Window window)
    {
        var hWnd = WindowNative.GetWindowHandle(window);
        var wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(wndId);
    }

    public static Window? GetWindowForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (var window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return window;
                }
            }
        }
        return null;
    }

    public static UIElement? FindElementByName(UIElement element, string name)
    {
        if (element.XamlRoot != null && element.XamlRoot.Content != null)
        {
            var ele = ((FrameworkElement)element.XamlRoot.Content).FindName(name);
            if (ele != null)
            {
                return ele as UIElement;
            }
        }
        return null;
    }

    public static List<Window> ActiveWindows => _activeWindows;

    private static readonly List<Window> _activeWindows = new();
}