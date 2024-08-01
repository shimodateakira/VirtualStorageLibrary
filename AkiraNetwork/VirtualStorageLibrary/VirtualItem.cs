﻿using AkiraNetwork.VirtualStorageLibrary.Localization;

namespace AkiraNetwork.VirtualStorageLibrary
{
    /// <summary>
    /// Represents an item with data in the virtual storage.
    /// </summary>
    public abstract class VirtualItem : VirtualNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        protected VirtualItem(VirtualNodeName name) : base(name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="createdDate">The creation date of the item.</param>
        protected VirtualItem(VirtualNodeName name, DateTime createdDate) : base(name, createdDate) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="createdDate">The creation date of the item.</param>
        /// <param name="updatedDate">The last update date of the item.</param>
        protected VirtualItem(VirtualNodeName name, DateTime createdDate, DateTime updatedDate) : base(name, createdDate, updatedDate) { }

        /// <summary>
        /// Creates a deep clone of this item. If the data of type T held by the item 
        /// implements the <see cref="IVirtualDeepCloneable{T}"/> interface, the 
        /// DeepClone method of the data type is called.
        /// </summary>
        /// <param name="recursive">In the case of items, this parameter is ignored.</param>
        /// <returns>A deep clone of the current item as a <see cref="VirtualNode"/>.</returns>
        public override abstract VirtualNode DeepClone(bool recursive = false);
    }

    /// <summary>
    /// Represents an item with data of a user-defined type T in the virtual storage.
    /// </summary>
    /// <typeparam name="T">
    /// The user-defined type that can be freely specified by the user of this library.
    /// </typeparam>
    public class VirtualItem<T> : VirtualItem, IDisposable
    {
        /// <summary>
        /// The data associated with this item.
        /// It is user-defined and can hold any type of data.
        /// </summary>
        private T? _itemData;

        /// <summary>
        /// Gets or sets the data associated with this item. This value can be null.
        /// </summary>
        public T? ItemData
        {
            get => _itemData;
            set => _itemData = value;
        }

        /// <summary>
        /// Indicates whether the object has been disposed.
        /// Initially set to false in the constructor and set to true when Dispose is called.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        public override VirtualNodeType NodeType => VirtualNodeType.Item;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        public VirtualItem()
            : base(VirtualNodeName.GenerateNodeName(VirtualStorageState.State.PrefixItem))
        {
            ItemData = default;
            _disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        public VirtualItem(VirtualNodeName name) : base(name)
        {
            ItemData = default;
            _disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="itemData">
        /// The data associated with this item. It is user-defined and can hold any type of data.
        /// </param>
        public VirtualItem(VirtualNodeName name, T? itemData) : base(name)
        {
            ItemData = itemData;
            _disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="itemData">
        /// The data associated with this item. It is user-defined and can hold any type of data.
        /// </param>
        /// <param name="createdDate">The creation date of the item.</param>
        public VirtualItem(VirtualNodeName name, T? itemData, DateTime createdDate) : base(name, createdDate)
        {
            ItemData = itemData;
            _disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="itemData">
        /// The data associated with this item. It is user-defined and can hold any type of data.
        /// </param>
        /// <param name="createdDate">The creation date of the item.</param>
        /// <param name="updatedDate">The last update date of the item.</param>
        public VirtualItem(VirtualNodeName name, T? itemData, DateTime createdDate, DateTime updatedDate) : base(name, createdDate, updatedDate)
        {
            ItemData = itemData;
            _disposed = false;
        }

        /// <summary>
        /// Converts a tuple (nodeName, itemData) to a <see cref="VirtualItem{T}"/>.
        /// </summary>
        /// <param name="tuple">A tuple containing the node name and item data.</param>
        /// <returns>A <see cref="VirtualItem{T}"/> initialized with the specified values.</returns>
        public static implicit operator VirtualItem<T>((VirtualNodeName nodeName, T? itemData) tuple)
        {
            return new VirtualItem<T>(tuple.nodeName, tuple.itemData);
        }

        /// <summary>
        /// Implicitly converts the specified data of type T to a <see cref="VirtualItem{T}"/>.
        /// </summary>
        /// <param name="itemData">
        /// The data associated with this item. It is user-defined and can hold any type of data.
        /// </param>
        /// <returns>A <see cref="VirtualItem{T}"/> initialized with the specified data.</returns>
        public static implicit operator VirtualItem<T>(T? itemData)
        {
            string prefix = VirtualStorageState.State.PrefixItem;
            VirtualNodeName nodeName = VirtualNodeName.GenerateNodeName(prefix);
            return new VirtualItem<T>(nodeName, itemData);
        }

        /// <summary>
        /// Implicitly converts the specified node name to a <see cref="VirtualItem{T}"/>.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <returns>A <see cref="VirtualItem{T}"/> initialized with the specified node name.</returns>
        public static implicit operator VirtualItem<T>(VirtualNodeName name)
        {
            return new VirtualItem<T>(name);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{Name}";

        /// <summary>
        /// Creates a deep clone of this item.
        /// </summary>
        /// <param name="recursive">If true, perform a deep clone recursively.</param>
        /// <returns>A deep clone of the current item as a <see cref="VirtualNode"/>.</returns>
        public override VirtualNode DeepClone(bool recursive = false)
        {
            T? newItemData = ItemData;

            // If ItemData implements IVirtualDeepCloneable<T>, call DeepClone().
            if (ItemData is IVirtualDeepCloneable<T> cloneableItem)
            {
                newItemData = cloneableItem.DeepClone();
            }

            return new VirtualItem<T>(Name, newItemData);
        }

        /// <summary>
        /// Releases the resources used by the <see cref="VirtualItem{T}"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="VirtualItem{T}"/> and optionally 
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // If T implements IDisposable, call Dispose().
                    (ItemData as IDisposable)?.Dispose();
                }

                // VirtualItem<T> does not have unmanaged resources to release.
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizer for the <see cref="VirtualItem{T}"/> class.
        /// </summary>
        ~VirtualItem()
        {
            Dispose(false);
        }

        /// <summary>
        /// Updates the current item with the specified node's data.
        /// </summary>
        /// <param name="node">The node containing the data to update.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified node is not of the same type as the current item.
        /// </exception>
        public override void Update(VirtualNode node)
        {
            if (node is not VirtualItem<T> newItem)
            {
                throw new ArgumentException(string.Format(Resources.NodeIsNotVirtualItem, node.Name, typeof(T).Name), nameof(node));
            }

            if (newItem.IsReferencedInStorage)
            {
                newItem = (VirtualItem<T>)newItem.DeepClone();
            }

            CreatedDate = newItem.CreatedDate;
            UpdatedDate = DateTime.Now;
            ItemData = newItem.ItemData;
        }
    }
}
