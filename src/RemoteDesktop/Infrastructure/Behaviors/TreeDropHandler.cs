using GongSolutions.Wpf.DragDrop;

using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections;
using System.Windows;

namespace RemoteDesktop.Infrastructure.Behaviors;

internal class TreeDropHandler : ITreeDropHandler
{
    public event Action ItemMoved;
    public Point DragMouseAnchorPoint { get; }

    public TreeDropHandler()
    {
        DragMouseAnchorPoint = new Point(.4, 0);
    }

    public void DragOver(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel viewModel)
        {
            info.Effects = DragDropEffects.None;
            return;
        }

        var target = info.TargetItem as TreeItemViewModel;

        switch (viewModel.Model)
        {
            case Server when target.Model is ServerGroup:
                {
                    info.Effects = DragDropEffects.Move;
                    info.DropTargetAdorner = DropTargetAdorners.Highlight;

                    break;
                }
            default:
                info.Effects = DragDropEffects.None;
                break;
        }
    }

    public void Drop(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel viewModel)
        {
            return;
        }

        if (info.DragInfo.SourceCollection is IList source)
        {
            source.Remove(viewModel);
        }

        if (info.TargetItem is TreeItemViewModel { Model: ServerGroup } target)
        {
            target.Children.Add(viewModel);
        }

        ItemMoved?.Invoke();
    }

    public void DropHint(IDropHintInfo hint)
    { }

    public void DragLeave(IDropInfo info)
    { }

    public void DragEnter(IDropInfo info) => DragOver(info);
}

public interface ITreeDropHandler : IDropTarget
{
    event Action ItemMoved;

    Point DragMouseAnchorPoint { get; }
}