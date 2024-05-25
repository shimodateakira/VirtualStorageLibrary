﻿namespace AkiraNet.VirtualStorageLibrary
{
    public class VirtualSymbolicLink : VirtualNode
    {
        public VirtualPath? TargetPath { get; set; }

        public VirtualNodeType TargetNodeType { get; set; }

        public override VirtualNodeType NodeType => VirtualNodeType.SymbolicLink;

        public VirtualSymbolicLink(VirtualNodeName name) : base(name)
        {
            TargetPath = null;
        }

        public VirtualSymbolicLink(VirtualNodeName name, VirtualPath? targetPath) : base(name)
        {
            TargetPath = targetPath;
        }

        public VirtualSymbolicLink(VirtualNodeName name, VirtualPath? targetPath, DateTime createdDate, DateTime updatedDate) : base(name, createdDate, updatedDate)
        {
            TargetPath = targetPath;
        }

        public override string ToString() => $"{Name} -> {TargetPath}";

        public override VirtualNode DeepClone()
        {
            return new VirtualSymbolicLink(Name, TargetPath, CreatedDate, UpdatedDate);
        }
    }
}