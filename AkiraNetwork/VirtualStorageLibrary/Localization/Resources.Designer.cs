﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AkiraNetwork.VirtualStorageLibrary.Localization {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AkiraNetwork.VirtualStorageLibrary.Localization.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   The specified base path is not an absolute path. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string BasePathIsNotAbsolute {
            get {
                return ResourceManager.GetString("BasePathIsNotAbsolute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot add the root to a directory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotAddRoot {
            get {
                return ResourceManager.GetString("CannotAddRoot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot change the target path of the links associated with the storage. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotChangeTargetPath {
            get {
                return ResourceManager.GetString("CannotChangeTargetPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot move the node if the source path is the current directory or a parent directory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotMoveCurrentOrParentDirectory {
            get {
                return ResourceManager.GetString("CannotMoveCurrentOrParentDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot move the root directory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotMoveRootDirectory {
            get {
                return ResourceManager.GetString("CannotMoveRootDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot reach node [{0}]. Node [{1}] is an item. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotReachBecauseNodeItem {
            get {
                return ResourceManager.GetString("CannotReachBecauseNodeItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot reach node [{0}]. Node [{1}] is a symbolic link, and the followLinks parameter is set to false. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotReachBecauseNodeSymbolicLink {
            get {
                return ResourceManager.GetString("CannotReachBecauseNodeSymbolicLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot remove the current or parent directory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotRemoveCurrentOrParentDirectory {
            get {
                return ResourceManager.GetString("CannotRemoveCurrentOrParentDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot remove the directory because it is not empty and the recursive parameter is set to false. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotRemoveNonEmptyDirectory {
            get {
                return ResourceManager.GetString("CannotRemoveNonEmptyDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot remove the root directory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CannotRemoveRoot {
            get {
                return ResourceManager.GetString("CannotRemoveRoot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Circular reference detected. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string CircularReferenceDetected {
            get {
                return ResourceManager.GetString("CircularReferenceDetected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The destination path is a subdirectory of the source path. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DestinationIsSubdirectoryOfSource {
            get {
                return ResourceManager.GetString("DestinationIsSubdirectoryOfSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The destination node is an item or a symbolic link. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DestinationNodeIsItemOrSymbolicLink {
            get {
                return ResourceManager.GetString("DestinationNodeIsItemOrSymbolicLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Node name [{0}] is invalid. Forbidden characters are used. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidNodeName {
            get {
                return ResourceManager.GetString("InvalidNodeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Target path [{0}] is invalid. Forbidden characters are used. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidTargetPath {
            get {
                return ResourceManager.GetString("InvalidTargetPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Invalid wildcard pattern found in path [{0}]. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidWildcardPatternInPath {
            get {
                return ResourceManager.GetString("InvalidWildcardPatternInPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The symbolic link path specified in the parameter must be an absolute path. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string LinkPathIsNotAbsolutePath {
            get {
                return ResourceManager.GetString("LinkPathIsNotAbsolutePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot rename because a node with the new name already exists. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NewNameNodeAlreadyExists {
            get {
                return ResourceManager.GetString("NewNameNodeAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The new name is the same as the current name. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NewNameSameAsCurrent {
            get {
                return ResourceManager.GetString("NewNameSameAsCurrent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Node [{0}] already exists. Overwriting is not allowed. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeAlreadyExists {
            get {
                return ResourceManager.GetString("NodeAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The specified node [{0}] is not of type VirtualDirectory. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeIsNotVirtualDirectory {
            get {
                return ResourceManager.GetString("NodeIsNotVirtualDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The specified node [{0}] is not of type VirtualItem&lt;{1}&gt;. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeIsNotVirtualItem {
            get {
                return ResourceManager.GetString("NodeIsNotVirtualItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The specified node [{0}] is not of type VirtualSymbolicLink. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeIsNotVirtualSymbolicLink {
            get {
                return ResourceManager.GetString("NodeIsNotVirtualSymbolicLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Node not found. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeNotFound {
            get {
                return ResourceManager.GetString("NodeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   A node with the same name already exists at the destination path. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NodeWithSameNameAtDestination {
            get {
                return ResourceManager.GetString("NodeWithSameNameAtDestination", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The values ​​specified in the parameters are empty. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ParameterIsEmpty {
            get {
                return ResourceManager.GetString("ParameterIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The object specified by the parameter is not of type VirtualNodeName. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ParameterIsNotVirtualNodeName {
            get {
                return ResourceManager.GetString("ParameterIsNotVirtualNodeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The object specified by the parameter is not of type VirtualPath. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ParameterIsNotVirtualPath {
            get {
                return ResourceManager.GetString("ParameterIsNotVirtualPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The values ​​specified in the parameters are null or empty. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ParameterIsNullOrEmpty {
            get {
                return ResourceManager.GetString("ParameterIsNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   An empty string cannot be specified as a path. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PathCannotBeEmpty {
            get {
                return ResourceManager.GetString("PathCannotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   This path is not an absolute path. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PathIsNotAbsolutePath {
            get {
                return ResourceManager.GetString("PathIsNotAbsolutePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Due to path normalization, it is above the root directory. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PathNormalizationAboveRoot {
            get {
                return ResourceManager.GetString("PathNormalizationAboveRoot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Path not found. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PathNotFound {
            get {
                return ResourceManager.GetString("PathNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Prefix cannot be empty. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PrefixIsEmpty {
            get {
                return ResourceManager.GetString("PrefixIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   If the recursive parameter is set to true, the source or destination cannot be a subdirectory of each other. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RecursiveSubdirectoryConflict {
            get {
                return ResourceManager.GetString("RecursiveSubdirectoryConflict", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The root directory already exists. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RootAlreadyExists {
            get {
                return ResourceManager.GetString("RootAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The source path and the destination path for the copy operation are the same. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SourceAndDestinationPathSameForCopy {
            get {
                return ResourceManager.GetString("SourceAndDestinationPathSameForCopy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The source path and the destination path for the move operation are the same. [{0}] [{1}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string SourceAndDestinationPathSameForMove {
            get {
                return ResourceManager.GetString("SourceAndDestinationPathSameForMove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The target path specified in the parameter must be an absolute path. [{0}] に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string TargetPathIsNotAbsolutePath {
            get {
                return ResourceManager.GetString("TargetPathIsNotAbsolutePath", resourceCulture);
            }
        }
    }
}
