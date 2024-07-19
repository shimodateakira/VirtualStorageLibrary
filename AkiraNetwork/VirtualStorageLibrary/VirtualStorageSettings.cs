﻿namespace AkiraNetwork.VirtualStorageLibrary
{
    [DebuggerStepThrough]
    public class VirtualStorageSettings
    {
        private static VirtualStorageSettings _settings = new();

        public static VirtualStorageSettings Settings => _settings;

        private VirtualStorageSettings()
        {
            InvalidNodeNameCharacters = [PathSeparator];
            InvalidFullNodeNames = [PathDot, PathDotDot];

            WildcardMatcher = new PowerShellWildcardMatcher();

            NodeListConditions = new()
            {
                Filter = VirtualNodeTypeFilter.All,
                GroupCondition = new(node => node.ResolveNodeType(), true),
                SortConditions =
                [
                    new(node => node.Name, true)
                ]
            };
        }

        public static void Initialize()
        {
            _settings = new();

            // stateへの設定を反映
            VirtualStorageState.InitializeFromSettings(_settings);
        }

        public char PathSeparator { get; set; } = '/';

        public string PathRoot { get; set; } = "/";

        public string PathDot { get; set; } = ".";

        public string PathDotDot { get; set; } = "..";

        public char[] InvalidNodeNameCharacters { get; set; }

        public string[] InvalidFullNodeNames { get; set; }

        public IVirtualWildcardMatcher? WildcardMatcher { get; set; }

        public VirtualNodeListConditions NodeListConditions { get; set; }

        public string PrefixItem { get; set; } = "item";

        public string PrefixDirectory { get; set; } = "dir";

        public string PrefixSymbolicLink { get; set; } = "link";

        public string Locale { get; set; } = string.Empty; // string.Empty is neutral culture
    }
}
