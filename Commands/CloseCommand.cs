﻿namespace WPF_Challenge.Commands;

public class CloseCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        System.Windows.Application.Current.Shutdown();
    }
}
