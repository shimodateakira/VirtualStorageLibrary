﻿using AkiraNetwork.VirtualStorageLibrary.WildcardMatchers;

namespace AkiraNetwork.VirtualStorageLibrary.Test.WildcardMatchers
{
    [TestClass]
    public class DefaultWildcardMatcherTests
    {
        [TestMethod]
        public void WildcardDictionary_ContainsExpectedPatterns()
        {
            var matcher = new DefaultWildcardMatcher();
            var dictionary = matcher.WildcardDictionary;

            Assert.AreEqual(14, dictionary.Count);
            Assert.AreEqual(@".", dictionary[@"."]);
            Assert.AreEqual(@"*", dictionary[@"*"]);
            Assert.AreEqual(@"+", dictionary[@"+"]);
            Assert.AreEqual(@"?", dictionary[@"?"]);
            Assert.AreEqual(@"^", dictionary[@"^"]);
            Assert.AreEqual(@"$", dictionary[@"$"]);
            Assert.AreEqual(@"|", dictionary[@"|"]);
            Assert.AreEqual(@"(", dictionary[@"("]);
            Assert.AreEqual(@")", dictionary[@")"]);
            Assert.AreEqual(@"[", dictionary[@"["]);
            Assert.AreEqual(@"]", dictionary[@"]"]);
            Assert.AreEqual(@"{", dictionary[@"{"]);
            Assert.AreEqual(@"}", dictionary[@"}"]);
            Assert.AreEqual(@"\", dictionary[@"\"]);
        }

        [TestMethod]
        public void Wildcards_ContainsExpectedKeys()
        {
            var matcher = new DefaultWildcardMatcher();
            var wildcards = matcher.Wildcards.ToList();

            Assert.AreEqual(14, wildcards.Count);
            CollectionAssert.Contains(wildcards, @".");
            CollectionAssert.Contains(wildcards, @"*");
            CollectionAssert.Contains(wildcards, @"+");
            CollectionAssert.Contains(wildcards, @"?");
            CollectionAssert.Contains(wildcards, @"^");
            CollectionAssert.Contains(wildcards, @"$");
            CollectionAssert.Contains(wildcards, @"|");
            CollectionAssert.Contains(wildcards, @"(");
            CollectionAssert.Contains(wildcards, @")");
            CollectionAssert.Contains(wildcards, @"[");
            CollectionAssert.Contains(wildcards, @"]");
            CollectionAssert.Contains(wildcards, @"{");
            CollectionAssert.Contains(wildcards, @"}");
            CollectionAssert.Contains(wildcards, @"\");
        }

        [TestMethod]
        public void Patterns_ContainsExpectedValues()
        {
            var matcher = new DefaultWildcardMatcher();
            var patterns = matcher.Patterns.ToList();

            Assert.AreEqual(14, patterns.Count);
            CollectionAssert.Contains(patterns, @".");
            CollectionAssert.Contains(patterns, @"*");
            CollectionAssert.Contains(patterns, @"+");
            CollectionAssert.Contains(patterns, @"?");
            CollectionAssert.Contains(patterns, @"^");
            CollectionAssert.Contains(patterns, @"$");
            CollectionAssert.Contains(patterns, @"|");
            CollectionAssert.Contains(patterns, @"(");
            CollectionAssert.Contains(patterns, @")");
            CollectionAssert.Contains(patterns, @"[");
            CollectionAssert.Contains(patterns, @"]");
            CollectionAssert.Contains(patterns, @"{");
            CollectionAssert.Contains(patterns, @"}");
            CollectionAssert.Contains(patterns, @"\");
        }

        [TestMethod]
        public void Count_ReturnsCorrectValue()
        {
            var matcher = new DefaultWildcardMatcher();
            Assert.AreEqual(14, matcher.Count);
        }

        [TestMethod]
        public void PatternMatcher_MatchesCorrectly()
        {
            var matcher = new DefaultWildcardMatcher();

            // "." matches any one character
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test.t?t"));

            // "*" matches zero or more characters
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test.*"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "t.*"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", ".*t"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "testX*"));

            // "+" matches one or more characters
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test.+"));
            Assert.IsFalse(matcher.PatternMatcher("test.txt", "testX+"));

            // "?" matches zero or one character
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "t.?st.txt"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "te.?t.txt"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test?.txt"));
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "testX?.txt"));

            // "[" and "]" for character class
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "tes[stu].txt"));
            Assert.IsFalse(matcher.PatternMatcher("test.txt", "tes[pqr].txt"));

            // "^" matches the beginning of the string
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "^test.txt"));

            // "$" matches the end of the string
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test.txt$"));

            // "|" matches either the expression before or the expression after
            Assert.IsTrue(matcher.PatternMatcher("test.txt", "test.txt|sample.txt"));
            Assert.IsFalse(matcher.PatternMatcher("sample.txt", "test.txt|sample1.txt"));
        }
    }
}
