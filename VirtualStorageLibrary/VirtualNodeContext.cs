﻿using System.Diagnostics;

namespace AkiraNet.VirtualStorageLibrary
{
    [method: DebuggerStepThrough]
    public class VirtualNodeContext(
        VirtualNode? node,
        VirtualPath traversalPath,
        VirtualDirectory? parentNode = null,
        int depth = 0,
        int index = 0,
        VirtualPath? resolvedPath = null,
        bool resolved = false)
    {
        public VirtualNode? Node { [DebuggerStepThrough] get; } = node;

        public VirtualPath TraversalPath { [DebuggerStepThrough] get; } = traversalPath;

        public VirtualDirectory? ParentDirectory { [DebuggerStepThrough] get; } = parentNode;

        public int Depth { [DebuggerStepThrough] get; } = depth;

        public int Index { [DebuggerStepThrough] get; } = index;

        public VirtualPath? ResolvedPath { [DebuggerStepThrough] get; } = resolvedPath;

        public bool Resolved { [DebuggerStepThrough] get; } = resolved;

        [DebuggerStepThrough]
        public override string ToString()
        {
            List<string> parts =
            [
                $"NodeName: {Node?.Name}",
                $"TraversalPath: {TraversalPath}",
                $"ParentDirectory: {ParentDirectory?.Name}",
                $"Depth: {Depth}",
                $"Index: {Index}"
            ];

            if (ResolvedPath != null)
            {
                parts.Add($"ResolvedPath: {ResolvedPath}");
            }

            parts.Add($"Resolved: {Resolved}");

            return string.Join(", ", parts);
        }
    }
}
