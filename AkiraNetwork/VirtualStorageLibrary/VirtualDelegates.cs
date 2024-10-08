﻿// This file is part of VirtualStorageLibrary.
//
// Copyright (C) 2024 Akira Shimodate
//
// VirtualStorageLibrary is free software, and it is distributed under the terms of 
// the GNU Lesser General Public License (version 3, or at your option, any later 
// version). This license is published by the Free Software Foundation.
//
// VirtualStorageLibrary is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY, without even the implied warranty of MERCHANTABILITY or 
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for 
// more details.
//
// You should have received a copy of the GNU Lesser General Public License along 
// with VirtualStorageLibrary. If not, see https://www.gnu.org/licenses/.

namespace AkiraNetwork.VirtualStorageLibrary
{
    /// <summary>
    /// A delegate used to notify the status of a specific node during node traversal.
    /// </summary>
    /// <param name="path">The virtual path of the node being notified.</param>
    /// <param name="node">The instance of the node being notified.</param>
    public delegate void NotifyNodeDelegate(VirtualPath path, VirtualNode? node);

    /// <summary>
    /// A delegate used to perform an action on a specific node during node traversal.
    /// </summary>
    /// <param name="parentDirectory">The parent directory of the target node.</param>
    /// <param name="nodeName">The name of the target node.</param>
    /// <param name="nodePath">The path of the target node.</param>
    /// <returns>
    /// Returns true to continue node traversal, or false to stop node traversal.
    /// </returns>
    public delegate bool ActionNodeDelegate(VirtualDirectory parentDirectory, VirtualNodeName nodeName, VirtualPath nodePath);

    /// <summary>
    /// A delegate used to determine if a node name matches a pattern.
    /// </summary>
    /// <param name="nodeName">The name of the node to be matched.</param>
    /// <param name="pattern">The pattern to match against.</param>
    /// <returns>True if the node name matches the pattern; otherwise, false.</returns>
    public delegate bool PatternMatch(string nodeName, string pattern);
}
