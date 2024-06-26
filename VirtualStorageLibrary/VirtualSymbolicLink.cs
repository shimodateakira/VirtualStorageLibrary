﻿namespace AkiraNet.VirtualStorageLibrary
{
    public class VirtualSymbolicLink : VirtualNode
    {
        public VirtualPath? TargetPath { get; set; }

        public override VirtualNodeType NodeType => VirtualNodeType.SymbolicLink;

        public VirtualNodeType TargetNodeType { get; set; } = VirtualNodeType.None;

        public VirtualSymbolicLink()
             : base(VirtualNodeName.GenerateNodeName(VirtualStorageState.State.PrefixSymbolicLink))
        {
            TargetPath = null;
        }

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

        // タプルからVirtualSymbolicLinkへの暗黙的な変換
        public static implicit operator VirtualSymbolicLink((VirtualNodeName nodeName, VirtualPath? targetPath) tuple)
        {
            return new VirtualSymbolicLink(new VirtualNodeName(tuple.nodeName), tuple.targetPath);
        }

        // VirtualPathからVirtualSymbolicLinkへの暗黙的な変換
        public static implicit operator VirtualSymbolicLink(VirtualPath? targetPath)
        {
            string prefix = VirtualStorageState.State.PrefixSymbolicLink;
            VirtualNodeName nodeName = VirtualNodeName.GenerateNodeName(prefix);
            return new VirtualSymbolicLink(nodeName, targetPath);
        }

        public override string ToString() => $"{Name} -> {TargetPath ?? "(null)"}";

        public override VirtualNode DeepClone(bool recursive = false)
        {
            return new VirtualSymbolicLink(Name, TargetPath, CreatedDate, UpdatedDate);
        }

        public override void Update(VirtualNode node)
        {
            if (node is not VirtualSymbolicLink link)
            {
                throw new ArgumentException($"このノード {node.Name} はシンボリックリンクではありません。");
            }

            TargetPath = link.TargetPath;
            UpdatedDate = link.UpdatedDate;
        }
    }
}
