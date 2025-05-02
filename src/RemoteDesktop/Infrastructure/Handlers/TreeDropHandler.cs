using GongSolutions.Wpf.DragDrop;

using RemoteDesktop.Models;
using RemoteDesktop.ViewModels;

using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace RemoteDesktop.Infrastructure.Handlers;

internal class TreeDropHandler : IDropTarget
{
    public IList<TreeItemViewModel> RootGroups { get; }

    public TreeDropHandler(IList<TreeItemViewModel> rootGroups)
    {
        RootGroups = rootGroups;
    }

    public void DragOver(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel viewModel)
        {
            info.Effects = DragDropEffects.None;
            return;
        }

        var target = info.TargetItem as TreeItemViewModel;

        var canServer = viewModel.Model is Server
            && target.Model is ServerGroup;

        var canGroup = viewModel.Model is ServerGroup
            && info.TargetCollection == RootGroups;

        if (canServer || canGroup)
        {
            info.Effects = DragDropEffects.Move;
            info.DropTargetAdorner = canServer ? DropTargetAdorners.Highlight : DropTargetAdorners.Insert;
        }
        else
        {
            info.Effects = DragDropEffects.None;
        }
    }

    public void Drop(IDropInfo info)
    {
        if (info.Data is not TreeItemViewModel src)
        {
            return;
        }

        if (info.TargetCollection is not IList dstList ||
            info.DragInfo.SourceCollection is not IList srcList)
        {
            return;
        }

        if (src.Model is Server && info.TargetCollection == RootGroups)
        {
            return;
        }

        srcList.Remove(src);

        var idx = info.InsertIndex;
        if (idx < 0 || idx > dstList.Count)
        {
            idx = dstList.Count;
        }

        dstList.Insert(idx, src);
    }

    public void DropHint(IDropHintInfo hint)
    { }

    public void DragLeave(IDropInfo info)
    { }

    public void DragEnter(IDropInfo info) => DragOver(info);
}