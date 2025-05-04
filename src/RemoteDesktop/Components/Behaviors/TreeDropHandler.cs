using GongSolutions.Wpf.DragDrop;

using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System;
using System.Collections;
using System.Windows;

namespace RemoteDesktop.Components.Behaviors;

internal class TreeDropHandler : ITreeDropHandler
{
    public event Action ItemMoved;

    public Point DragMouseAnchorPoint { get; } = new Point(.4, 0);

    public void DragOver(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel dragged)
        {
            info.Effects = DragDropEffects.None;
            return;
        }

        var targetVm = info.TargetItem as TreeItemViewModel;

        if (info.DragInfo.SourceCollection == info.TargetCollection)
        {
            info.Effects = DragDropEffects.Move;
            info.DropTargetAdorner = DropTargetAdorners.Insert;
            return;
        }

        if (dragged.Model is Server && targetVm?.Model is ServerGroup)
        {
            info.Effects = DragDropEffects.Move;
            info.DropTargetAdorner = DropTargetAdorners.Highlight;
            return;
        }

        info.Effects = DragDropEffects.None;
    }

    public void Drop(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel dragged)
        {
            return;
        }

        if (info.DragInfo.SourceCollection is IList sourceList)
        {
            sourceList.Remove(dragged);
        }

        if (info.TargetCollection is IList targetList && info.DragInfo.SourceCollection == targetList)
        {
            var insertIndex = info.InsertIndex;
            if (insertIndex > targetList.Count)
            {
                insertIndex = targetList.Count;
            }

            targetList.Insert(insertIndex, dragged);
        }
        else if (info.TargetItem is TreeItemViewModel { Model: ServerGroup } groupVm)
        {
            groupVm.Children.Add(dragged);
        }

        ItemMoved?.Invoke();
    }

    public void DropHint(IDropHintInfo hint)
    {
    }

    public void DragLeave(IDropInfo info)
    {
    }

    public void DragEnter(IDropInfo info)
    {
        DragOver(info);
    }
}

public interface ITreeDropHandler : IDropTarget
{
    event Action ItemMoved;

    Point DragMouseAnchorPoint { get; }
}