﻿using AkiraNetwork.VirtualStorageLibrary.Localization;

namespace AkiraNetwork.VirtualStorageLibrary
{
    /// <summary>
    /// Represents a virtual symbolic link. A virtual symbolic link provides a virtual reference 
    /// to other nodes (directories, items, or other symbolic links). This class represents 
    /// symbolic links within the virtual storage library and manages paths to other nodes.
    /// 
    /// The virtual symbolic link supports the concept of NULL links. When the TargetPath is null, 
    /// the link resolution is not performed during path traversal. This feature can be useful when 
    /// the target is undefined or dynamically determined.
    /// </summary>
    public class VirtualSymbolicLink : VirtualNode
    {
        /// <summary>
        /// Holds the target path of the symbolic link.
        /// </summary>
        private VirtualPath? _targetPath = null;

        /// <summary>
        /// Gets or sets the target path of the symbolic link.
        /// </summary>
        public VirtualPath? TargetPath
        {
            get => _targetPath;
            set => _targetPath = value;
        }

        /// <summary>
        /// Gets the type of the node. This class always returns SymbolicLink.
        /// </summary>
        public override VirtualNodeType NodeType => VirtualNodeType.SymbolicLink;

        /// <summary>
        /// Gets or sets the type of the target node of the symbolic link.
        /// </summary>
        public VirtualNodeType TargetNodeType { get; set; } = VirtualNodeType.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualSymbolicLink"/> class. The node name is automatically generated using the VirtualNodeName.GenerateNodeName method.
        /// </summary>
        public VirtualSymbolicLink()
             : base(VirtualNodeName.GenerateNodeName(VirtualStorageState.State.PrefixSymbolicLink))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualSymbolicLink"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the symbolic link.</param>
        public VirtualSymbolicLink(VirtualNodeName name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualSymbolicLink"/> class with the specified name and target path.
        /// </summary>
        /// <param name="name">The name of the symbolic link.</param>
        /// <param name="targetPath">The target path of the symbolic link.</param>
        public VirtualSymbolicLink(VirtualNodeName name, VirtualPath? targetPath) : base(name)
        {
            _targetPath = targetPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualSymbolicLink"/> class with the specified name, target path, creation date, and update date.
        /// </summary>
        /// <param name="name">The name of the symbolic link.</param>
        /// <param name="targetPath">The target path of the symbolic link.</param>
        /// <param name="createdDate">The creation date.</param>
        /// <param name="updatedDate">The update date.</param>
        public VirtualSymbolicLink(VirtualNodeName name, VirtualPath? targetPath, DateTime createdDate, DateTime updatedDate) : base(name, createdDate, updatedDate)
        {
            _targetPath = targetPath;
        }

        /// <summary>
        /// Performs an implicit conversion from a tuple to <see cref="VirtualSymbolicLink"/>.
        /// </summary>
        /// <param name="tuple">The tuple to convert.</param>
        public static implicit operator VirtualSymbolicLink((VirtualNodeName nodeName, VirtualPath? targetPath) tuple)
        {
            return new VirtualSymbolicLink(new VirtualNodeName(tuple.nodeName), tuple.targetPath);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="VirtualPath"/> to <see cref="VirtualSymbolicLink"/>. The node name is automatically generated using the VirtualNodeName.GenerateNodeName method.
        /// </summary>
        /// <param name="targetPath">The target path to convert.</param>
        public static implicit operator VirtualSymbolicLink(VirtualPath? targetPath)
        {
            string prefix = VirtualStorageState.State.PrefixSymbolicLink;
            VirtualNodeName nodeName = VirtualNodeName.GenerateNodeName(prefix);
            return new VirtualSymbolicLink(nodeName, targetPath);
        }

        /// <summary>
        /// Returns a string representation of the symbolic link.
        /// </summary>
        /// <returns>A string representation of the symbolic link.</returns>
        public override string ToString() => $"{Name} -> {TargetPath ?? "(null)"}";

        /// <summary>
        /// Creates a deep clone of the symbolic link.
        /// </summary>
        /// <param name="recursive">If true, all links are recursively cloned.</param>
        /// <returns>A deep clone of the symbolic link.</returns>
        public override VirtualNode DeepClone(bool recursive = false)
        {
            return new VirtualSymbolicLink(Name, TargetPath);
        }

        /// <summary>
        /// Updates the current symbolic link with the data from the specified node.
        /// </summary>
        /// <param name="node">The node to use for the update.</param>
        /// <exception cref="ArgumentException">Thrown if the specified node is not a VirtualSymbolicLink.</exception>
        public override void Update(VirtualNode node)
        {
            if (node is not VirtualSymbolicLink newLink)
            {
                throw new ArgumentException(string.Format(Resources.NodeIsNotVirtualSymbolicLink, node.Name), nameof(node));
            }

            if (newLink.IsReferencedInStorage)
            {
                newLink = (VirtualSymbolicLink)newLink.DeepClone();
            }

            CreatedDate = newLink.CreatedDate;
            UpdatedDate = DateTime.Now;
            TargetPath = newLink.TargetPath;
        }
    }
}
