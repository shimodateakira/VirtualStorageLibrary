﻿using System.Collections.Specialized;
using System.Reflection;

namespace AkiraNetwork.VirtualStorageLibrary.Utilities
{
    public static class VirtualTextFormatter
    {
        // 仮想ストレージのツリー構造をテキストベースで作成し返却する拡張メソッド
        // 返却するテストは以下の出力の形式とする。
        // 出力例:
        // /
        // ├dir1/
        // │├subdir1/
        // ││└item3
        // │└item2
        // ├link-to-dir -> /dir1
        // ├item1
        // └link-to-item -> /item1
        public static string GenerateTreeDebugText<T>(this VirtualStorage<T> vs, VirtualPath basePath, bool recursive = true, bool followLinks = false)
        {
            const char FullWidthSpaceChar = '\u3000';
            StringBuilder tree = new();

            basePath = vs.ConvertToAbsolutePath(basePath).NormalizePath();
            IEnumerable<VirtualNodeContext> nodeContexts = vs.WalkPathTree(basePath, VirtualNodeTypeFilter.All, recursive, followLinks);
            StringBuilder line = new();
            string prevLine = string.Empty;

            VirtualNodeContext nodeFirstContext = nodeContexts.First();
            VirtualPath baseAbsolutePath = basePath + nodeFirstContext.TraversalPath;

            if (basePath.IsRoot)
            {
                tree.AppendLine("/");
            }
            else
            {
                tree.AppendLine(baseAbsolutePath);
            }

            foreach (var nodeInfo in nodeContexts.Skip(1))
            {
                VirtualNode node = nodeInfo.Node!;
                string nodeName = nodeInfo.TraversalPath.NodeName;
                int depth = nodeInfo.Depth;
                int count = nodeInfo.ParentDirectory!.Count;
                int index = nodeInfo.Index;

                line.Clear();

                if (depth > 0)
                {
                    for (int i = 0; i < depth - 1; i++)
                    {
                        switch (prevLine[i])
                        {
                            case FullWidthSpaceChar:
                                line.Append(FullWidthSpaceChar);
                                break;
                            case '│':
                                line.Append('│');
                                break;
                            case '├':
                                line.Append('│');
                                break;
                            default:
                                line.Append(FullWidthSpaceChar);
                                break;
                        }
                    }

                    if (index == count - 1)
                    {
                        line.Append('└');
                    }
                    else
                    {
                        line.Append('├');
                    }
                }

                line.Append(node.ToString());

                prevLine = line.ToString();
                tree.AppendLine(line.ToString());
            }

            return tree.ToString();
        }

        public static string GenerateLinkTableDebugText<T>(this VirtualStorage<T> vs)
        {
            if (vs.LinkDictionary.Count == 0)
            {
                return "(リンク辞書は空です。)";
            }

            List<IEnumerable<string>> tableData =
            [
                ["TargetPath", "LinkPath"]
            ];

            foreach (var entry in vs.LinkDictionary)
            {
                var targetPath = entry.Key;
                var linkPathsWithTypes = entry.Value
                    .Select(vp =>
                    {
                        var symbolicLinkNode = vs.TryGetSymbolicLink(vp);
                        var targetNodeType = symbolicLinkNode?.TargetNodeType.ToString() ?? "Unknown";
                        return $"{vp}({targetNodeType})";
                    })
                    .ToList();

                tableData.Add(
                [
                    targetPath.ToString(),
                    string.Join(", ", linkPathsWithTypes)
                ]);
            }

            return VirtualTextFormatter.FormatTable(tableData);
        }

        public static string GenerateTableDebugText<T>(this IEnumerable<T> enumerableObject)
        {
            if (!enumerableObject.Any())
            {
                return "(コレクションは空です。)";
            }

            List<IEnumerable<string>> tableData = [];

            Type type = typeof(T);
            bool isSimpleType = type == typeof(string) || Nullable.GetUnderlyingType(type) != null || type.IsPrimitive;

            if (isSimpleType)
            {
                // Handle simple types (string, nullable types, and primitives)
                tableData.Add(["Value"]);

                foreach (var obj in enumerableObject)
                {
                    tableData.Add([obj?.ToString() ?? "(null)"]);
                }
            }
            else
            {
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (properties.Length > 0)
                {
                    tableData.Add(properties.Select(p => p.Name));

                    foreach (var obj in enumerableObject)
                    {
                        List<string> row = [];
                        foreach (var property in properties)
                        {
                            object? value = property.GetValue(obj);
                            row.Add(value?.ToString() ?? "(null)");
                        }
                        tableData.Add(row);
                    }
                }
                else
                {
                    tableData.Add(["Value"]);

                    foreach (var obj in enumerableObject)
                    {
                        tableData.Add([obj?.ToString() ?? "(null)"]);
                    }
                }
            }

            return FormatTable(tableData);
        }

        public static string GenerateSingleTableDebugText<T>(this T singleObject)
        {
            Type type = typeof(T);
            List<PropertyInfo> properties = [];

            if (type == typeof(string))
            {
                List<IEnumerable<string>> stringTable =
                [
                    ["Value"],
                    [singleObject?.ToString() ?? "(null)"]
                ];
                return FormatTable(stringTable);
            }

            bool isSimpleType = Nullable.GetUnderlyingType(type) != null || type.IsPrimitive;

            if (isSimpleType)
            {
                List<IEnumerable<string>> simpleTable =
                [
                    ["Value"],
                    [singleObject?.ToString() ?? "(null)"]
                ];
                return FormatTable(simpleTable);
            }

            // Handle IEnumerable separately
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                List<IEnumerable<string>> enumerableTable =
                [
                    ["Value"],
                    ["(no output)"]
                ];
                return FormatTable(enumerableTable);
            }

            // Get properties from base classes first
            Type? currentType = type;
            while (currentType != null)
            {
                properties.InsertRange(0, currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
                currentType = currentType.BaseType;
            }

            var uniqueProperties = properties.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            List<string> headers = uniqueProperties.Select(p => p.Name).ToList();
            List<string> row = [];

            foreach (var property in uniqueProperties)
            {
                try
                {
                    object? value = property.GetValue(singleObject);
                    if (value != null)
                    {
                        if (IsToStringOverridden(value.GetType()))
                        {
                            row.Add(value.ToString()!);
                        }
                        else if (value is IEnumerable && value is not string)
                        {
                            row.Add("(no output)");
                        }
                        else
                        {
                            row.Add(value.ToString()!);
                        }
                    }
                    else
                    {
                        row.Add("(null)");
                    }
                }
                catch
                {
                    row.Add("(exception)");
                }
            }

            if (headers.Count == 0)
            {
                headers.Add("Value");
                if (singleObject != null)
                {
                    row.Add(singleObject.ToString()!);
                }
                else
                {
                    row.Add("(null)");
                }
                //row.Add(singleObject?.ToString()!);
            }

            List<IEnumerable<string>> tableData =
            [
                headers,
            row
            ];

            return FormatTable(tableData);
        }

        private static string FormatTable(IEnumerable<IEnumerable<string>> tableData)
        {
            if (!tableData.Any())
            {
                return string.Empty;
            }

            var tableDataList = tableData.Select(row => row.ToList()).ToList(); // Convert to List<List<string>>
            int[] columnWidths = GetColumnWidths(tableDataList);

            string horizontalLine = new('-', columnWidths.Sum() + columnWidths.Length * 3 + 1);
            StringBuilder formattedTable = new();
            formattedTable.AppendLine(horizontalLine);

            foreach (var row in tableDataList)
            {
                formattedTable.Append('|');
                for (int i = 0; i < row.Count; i++)
                {
                    formattedTable.Append(" " + row[i].PadRight(columnWidths[i] - GetStringWidth(row[i]) + row[i].Length) + " |");
                }
                formattedTable.AppendLine();
                formattedTable.AppendLine(horizontalLine);
            }

            return formattedTable.ToString();
        }

        private static int[] GetColumnWidths(IEnumerable<IEnumerable<string>> tableData)
        {
            var tableDataList = tableData.Select(row => row.ToList()).ToList();
            int columnCount = tableDataList[0].Count;
            int[] columnWidths = new int[columnCount];

            foreach (var row in tableDataList)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    int cellWidth = GetStringWidth(row[i]);
                    if (cellWidth > columnWidths[i])
                    {
                        columnWidths[i] = cellWidth;
                    }
                }
            }

            return columnWidths;
        }

        private static int GetStringWidth(string str)
        {
            int width = 0;
            foreach (char c in str)
            {
                if (IsFullWidth(c))
                {
                    width += 2;
                }
                else
                {
                    width += 1;
                }
            }
            return width;
        }

        // Custom method to check if a character is full-width
        private static bool IsFullWidth(char c)
        {
            // Full-width character ranges
            return c >= 0x1100 && c <= 0x115F ||
                   c >= 0x2E80 && c <= 0xA4CF && c != 0x303F ||
                   c >= 0xAC00 && c <= 0xD7A3 ||
                   c >= 0xF900 && c <= 0xFAFF ||
                   c >= 0xFE10 && c <= 0xFE19 ||
                   c >= 0xFE30 && c <= 0xFE6F ||
                   c >= 0xFF00 && c <= 0xFF60 ||
                   c >= 0xFFE0 && c <= 0xFFE6;
        }

        private static bool IsToStringOverridden(Type type)
        {
            MethodInfo? toStringMethod = type.GetMethod("ToString", Type.EmptyTypes);
            return toStringMethod?.DeclaringType != typeof(object);
        }
    }
}