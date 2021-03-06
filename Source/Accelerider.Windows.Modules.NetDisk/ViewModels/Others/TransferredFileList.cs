﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Infrastructure.Interfaces;

namespace Accelerider.Windows.Modules.NetDisk.ViewModels.Others
{
    public class TransferredFileList : ObservableCollection<ITransferredFile>
    {
        public TransferredFileList(IEnumerable<ITransferredFile> files)
        {
            foreach (var file in files)
            {
                Add(file);
            }
        }

        protected override void InsertItem(int index, ITransferredFile item)
        {
            item.FileChecked += OnChecked;
            base.InsertItem(GetAppropriateIndex(item), item);
        }

        protected override void RemoveItem(int index)
        {
            Items[index].FileChecked -= OnChecked;
            base.RemoveItem(index);
        }

        private void OnChecked(object sender, FileCheckStatusEnum e)
        {
            var file = (ITransferredFile)sender;
            SetItem(IndexOf(file), file);
        }

        private int GetAppropriateIndex(ITransferredFile other)
        {
            var i = 0;
            for (; i < Items.Count; i++)
            {
                var item = Items[i];
                if (other.CompletedTime > item.CompletedTime) break;
            }

            return i;
        }
    }
}
