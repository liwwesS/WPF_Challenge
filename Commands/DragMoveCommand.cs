﻿using System.Windows.Input;
using System.Windows;

namespace WPF_Challenge.Commands;

public class DragMoveCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        if (parameter is not MouseButtonEventArgs args || args.OriginalSource is not FrameworkElement element) return;

        if (args.ChangedButton != MouseButton.Left)
            return;

        var window = Window.GetWindow(element);

        window?.DragMove();
    }
}
