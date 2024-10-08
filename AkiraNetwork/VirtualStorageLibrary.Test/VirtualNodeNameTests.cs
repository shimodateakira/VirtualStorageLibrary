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

using System.Diagnostics;
using System.Globalization;

namespace AkiraNetwork.VirtualStorageLibrary.Test
{
    [TestClass]
    public class VirtualNodeNameTests : VirtualTestBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            VirtualStorageSettings.Initialize();
            VirtualNodeName.ResetCounter();
        }

        [TestMethod]
        public void Constructor_SetsNameCorrectly()
        {
            // Arrange
            string expectedName = "TestNode";

            // Act
            var nodeName = new VirtualNodeName(expectedName);

            // Assert
            Assert.AreEqual(expectedName, nodeName.Name, "The name should be set correctly by the constructor.");
        }

        [TestMethod]
        public void Name_Property_ReturnsCorrectName()
        {
            // Arrange
            string testName = "AnotherTestNode";
            var nodeName = new VirtualNodeName(testName);

            // Act
            string actualName = nodeName.Name;

            // Assert
            Assert.AreEqual(testName, actualName, "The Name property should return the correct name.");
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            string testName = "NodeNameForTesting";
            var nodeName = new VirtualNodeName(testName);

            // Act
            string result = nodeName.ToString();

            // Assert
            Assert.AreEqual(testName, result, "ToString should return the name property.");
        }

        [TestMethod]
        public void ImplicitConversionFromString_CreatesCorrectVirtualNodeName()
        {
            // Arrange
            string testName = "ImplicitTestNode";

            // Act
            VirtualNodeName nodeName = testName;  // Implicit conversion from string

            // Assert
            Assert.AreEqual(testName, nodeName.Name, "Implicit conversion from string should set the name correctly.");
        }

        [TestMethod]
        public void ImplicitConversionToString_ReturnsCorrectString()
        {
            // Arrange
            string testName = "ImplicitTestNode";
            VirtualNodeName nodeName = new(testName);

            // Act
            string nameAsString = nodeName;  // Implicit conversion to string

            // Assert
            Assert.AreEqual(testName, nameAsString, "Implicit conversion to string should return the correct name.");
        }

        [TestMethod]
        public void Equals_ReturnsTrueForIdenticalNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act & Assert
            Assert.IsTrue(nodeName1.Equals(nodeName2), "Equals should return true for identical names.");
        }

        [TestMethod]
        public void Equals_ReturnsFalseForDifferentNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node1");
            var nodeName2 = new VirtualNodeName("Node2");

            // Act & Assert
            Assert.IsFalse(nodeName1.Equals(nodeName2), "Equals should return false for different names.");
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameHashCodeForIdenticalNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act & Assert
            Assert.AreEqual(nodeName1.GetHashCode(), nodeName2.GetHashCode(), "GetHashCode should return the same value for identical names.");
        }

        [TestMethod]
        public void CompareTo_ReturnsZeroForIdenticalNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act
            int result = nodeName1.CompareTo(nodeName2);

            // Assert
            Assert.AreEqual(0, result, "CompareTo should return 0 for identical names.");
        }

        [TestMethod]
        public void CompareTo_ReturnsNegativeForAlphabeticallyPrior()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node1");
            var nodeName2 = new VirtualNodeName("Node2");

            // Act
            int result = nodeName1.CompareTo(nodeName2);

            // Assert
            Assert.IsTrue(result < 0, "CompareTo should return a negative number when the first name is alphabetically before the second.");
        }

        [TestMethod]
        public void CompareTo_ReturnsPositiveForAlphabeticallyAfter()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node2");
            var nodeName2 = new VirtualNodeName("Node1");

            // Act
            int result = nodeName1.CompareTo(nodeName2);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo should return a positive number when the first name is alphabetically after the second.");
        }

        [TestMethod]
        public void CompareTo_ReturnsPositiveWhenComparedWithNull()
        {
            // Arrange
            var nodeName = new VirtualNodeName("Node");

            // Act
            int result = nodeName.CompareTo(null);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo should return a positive number when compared with null.");
        }

        [TestMethod]
        public void CompareTo_WithNullObject_ReturnsPositive()
        {
            // Arrange
            VirtualNodeName nodeName = "Node";

            // Act
            int result = nodeName.CompareTo((object?)null);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo(object?) should return a positive number when compared with null.");
        }

        [TestMethod]
        public void CompareTo_ThrowsArgumentExceptionWhenComparedWithDifferentType()
        {
            // Arrange
            VirtualNodeName nodeName = "Node";
            object obj = new();

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => nodeName.CompareTo(obj));

            Assert.AreEqual("The object specified by the parameter is not of type VirtualNodeName. (Parameter 'obj')", ex.Message);

            Debug.WriteLine(ex.Message);
        }

        [TestMethod]
        public void CompareTo_ReturnsZeroForIdenticalNamesUsingObject()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act
            int result = nodeName1.CompareTo((object)nodeName2);

            // Assert
            Assert.AreEqual(0, result, "CompareTo should return 0 for identical names using object parameter.");
        }

        [TestMethod]
        public void CompareTo_ReturnsNegativeForAlphabeticallyPriorUsingObject()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node1");
            var nodeName2 = new VirtualNodeName("Node2");

            // Act
            int result = nodeName1.CompareTo((object)nodeName2);

            // Assert
            Assert.IsTrue(result < 0, "CompareTo should return a negative number when the first name is alphabetically before the second using object parameter.");
        }

        [TestMethod]
        public void CompareTo_ReturnsPositiveForAlphabeticallyAfterUsingObject()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node2");
            var nodeName2 = new VirtualNodeName("Node1");

            // Act
            int result = nodeName1.CompareTo((object)nodeName2);

            // Assert
            Assert.IsTrue(result > 0, "CompareTo should return a positive number when the first name is alphabetically after the second using object parameter.");
        }

        [TestMethod]
        public void OperatorEquals_ReturnsTrueForIdenticalNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act & Assert
            Assert.IsTrue(nodeName1 == nodeName2, "Operator == should return true for identical names.");
        }

        [TestMethod]
        public void OperatorEquals_ReturnsFalseForDifferentNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node1");
            var nodeName2 = new VirtualNodeName("Node2");

            // Act & Assert
            Assert.IsFalse(nodeName1 == nodeName2, "Operator == should return false for different names.");
        }

        [TestMethod]
        public void OperatorNotEquals_ReturnsFalseForIdenticalNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node");
            var nodeName2 = new VirtualNodeName("Node");

            // Act & Assert
            Assert.IsFalse(nodeName1 != nodeName2, "Operator != should return false for identical names.");
        }

        [TestMethod]
        public void OperatorNotEquals_ReturnsTrueForDifferentNames()
        {
            // Arrange
            var nodeName1 = new VirtualNodeName("Node1");
            var nodeName2 = new VirtualNodeName("Node2");

            // Act & Assert
            Assert.IsTrue(nodeName1 != nodeName2, "Operator != should return true for different names.");
        }

        [TestMethod]
        public void OperatorEquals_BothNull_ReturnsTrue()
        {
            // Arrange
            VirtualNodeName? nodeName1 = null;
            VirtualNodeName? nodeName2 = null;

            // Act
            bool result = nodeName1 == nodeName2;

            // Assert
            Assert.IsTrue(result, "Operator == should return true when both left and right are null.");
        }

        [TestMethod]
        public void OperatorEquals_LeftNull_ReturnsFalse()
        {
            // Arrange
            VirtualNodeName? nodeName1 = null;
            VirtualNodeName nodeName2 = "Node";

            // Act
            bool result = nodeName1 == nodeName2;

            // Assert
            Assert.IsFalse(result, "Operator == should return false when left is null and right is not.");
        }

        [TestMethod]
        public void OperatorEquals_RightNull_ReturnsFalse()
        {
            // Arrange
            VirtualNodeName nodeName1 = "Node";
            VirtualNodeName? nodeName2 = null;

            // Act
            bool result = nodeName1 == nodeName2;

            // Assert
            Assert.IsFalse(result, "Operator == should return false when right is null and left is not.");
        }
        [TestMethod]
        public void IsValidNodeName_ReturnsFalseForEmptyName()
        {
            // Act
            bool isValid = VirtualNodeName.IsValidNodeName(string.Empty);

            // Assert
            Assert.IsFalse(isValid, "IsValidNodeName should return false for an empty name.");
        }

        [TestMethod]
        public void IsValidNodeName_ReturnsFalseForInvalidCharacters()
        {
            // Act
            bool isValid = VirtualNodeName.IsValidNodeName("Invalid/Name");

            // Assert
            Assert.IsFalse(isValid, "IsValidNodeName should return false for name containing invalid characters.");
        }

        [TestMethod]
        public void IsValidNodeName_ReturnsFalseForDot()
        {
            // Act
            bool isValid = VirtualNodeName.IsValidNodeName(".");

            // Assert
            Assert.IsFalse(isValid, "IsValidNodeName should return false for name that is '.'.");
        }

        [TestMethod]
        public void IsValidNodeName_ReturnsFalseForDotDot()
        {
            // Act
            bool isValid = VirtualNodeName.IsValidNodeName("..");

            // Assert
            Assert.IsFalse(isValid, "IsValidNodeName should return false for name that is '..'.");
        }

        [TestMethod]
        public void IsValidNodeName_ReturnsTrueForValidName()
        {
            // Act
            bool isValid = VirtualNodeName.IsValidNodeName("ValidNodeName");

            // Assert
            Assert.IsTrue(isValid, "IsValidNodeName should return true for a valid name.");
        }

        [TestMethod]
        // プレフィックス付きの正しい名前を返すか確認する
        public void GenerateNodeName_ShouldReturnCorrectName_WithPrefix()
        {
            // Arrange
            string prefix = "item";

            // Act
            VirtualNodeName result = VirtualNodeName.GenerateNodeName(prefix);
            long actualNumber = long.Parse(result.ToString()[prefix.Length..]);

            // Assert
            StringAssert.StartsWith(result.ToString(), prefix);
            Assert.AreEqual(1, actualNumber);
        }

        [TestMethod]
        // カウンターが正しくインクリメントされるか確認する
        public void GenerateNodeName_ShouldIncrementCounter()
        {
            // Arrange
            string prefix = "node";
            VirtualNodeName firstResult = VirtualNodeName.GenerateNodeName(prefix);
            VirtualNodeName secondResult = VirtualNodeName.GenerateNodeName(prefix);

            // Act
            long firstNumber = long.Parse(firstResult.ToString()[prefix.Length..]);
            long secondNumber = long.Parse(secondResult.ToString()[prefix.Length..]);

            // Assert
            Assert.AreEqual(firstNumber + 1, secondNumber);
        }

        [TestMethod]
        // プレフィックスが空文字の場合に例外がスローされるか確認する
        public void GenerateNodeName_ShouldThrowException_WithEmptyPrefix()
        {
            // Arrange
            string prefix = "";

            // Act & Assert
            Exception err = Assert.ThrowsException<ArgumentException>(() => VirtualNodeName.GenerateNodeName(prefix));

            Assert.AreEqual("Prefix cannot be empty. (Parameter 'prefix')", err.Message);

            Debug.WriteLine(err.Message);
        }

        [TestMethod]
        // 長いプレフィックスでも正しく動作するか確認する
        public void GenerateNodeName_ShouldWorkWithLongPrefix()
        {
            // Arrange
            string prefix = new('a', 100);

            // Act
            VirtualNodeName result = VirtualNodeName.GenerateNodeName(prefix);

            // Assert
            StringAssert.StartsWith(result.ToString(), prefix);
        }

        [TestMethod]
        public void Equals_WithVirtualNodeName_ReturnsTrueForSameName()
        {
            // Arrange
            VirtualNodeName nodeName1 = "Node";
            VirtualNodeName nodeName2 = "Node";

            // Act
            bool result = nodeName1.Equals(nodeName2);

            // Assert
            Assert.IsTrue(result, "Equals(VirtualNodeName) should return true for same name.");
        }

        [TestMethod]
        public void Equals_WithVirtualNodeName_ReturnsFalseForDifferentName()
        {
            // Arrange
            VirtualNodeName nodeName1 = "Node1";
            VirtualNodeName nodeName2 = "Node2";

            // Act
            bool result = nodeName1.Equals(nodeName2);

            // Assert
            Assert.IsFalse(result, "Equals(VirtualNodeName) should return false for different name.");
        }

        [TestMethod]
        public void Equals_WithObject_ReturnsTrueForSameName()
        {
            // Arrange
            VirtualNodeName nodeName1 = "Node";
            VirtualNodeName nodeName2 = "Node";

            // Act
            bool result = nodeName1.Equals((object)nodeName2);

            // Assert
            Assert.IsTrue(result, "Equals(object) should return true for same name.");
        }

        [TestMethod]
        public void Equals_WithObject_ReturnsFalseForDifferentName()
        {
            // Arrange
            VirtualNodeName nodeName1 = "Node1";
            VirtualNodeName nodeName2 = "Node2";

            // Act
            bool result = nodeName1.Equals((object)nodeName2);

            // Assert
            Assert.IsFalse(result, "Equals(object) should return false for different name.");
        }

        [TestMethod]
        public void Equals_WithObject_ReturnsFalseForNull()
        {
            // Arrange
            VirtualNodeName nodeName = "Node";

            // Act
            bool result = nodeName.Equals((object?)null);

            // Assert
            Assert.IsFalse(result, "Equals(object) should return false when compared to null.");
        }

        [TestMethod]
        public void Equals_WithNullOther_ReturnsFalse()
        {
            // Arrange
            VirtualNodeName nodeName = "Node";
            VirtualNodeName? other = null;

            // Act
            bool result = nodeName.Equals(other);

            // Assert
            Assert.IsFalse(result, "Equals(VirtualNodeName?) should return false when other is null.");
        }
    }
}
