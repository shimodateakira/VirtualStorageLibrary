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
using AkiraNetwork.VirtualStorageLibrary.Utilities;

namespace AkiraNetwork.VirtualStorageLibrary.Test
{
    [TestClass]
    public class VirtualStorageAdapterTests : VirtualTestBase
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
        public void IndexerAdapter_Item_GetExistingItemTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data = [1, 2, 3];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item = (itemName, data);
            VirtualPath itemPath = "/testItem";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath.DirectoryPath, item);

            // Act
            VirtualItem<BinaryData> retrievedItem = vs.Item[itemPath];

            // Assert
            Assert.AreEqual(item.Name, retrievedItem.Name);
            CollectionAssert.AreEqual(item.ItemData!.Data, retrievedItem.ItemData!.Data);
            Assert.IsTrue(retrievedItem.IsReferencedInStorage);
        }

        [TestMethod]
        public void IndexerAdapter_Item_GetNonExistingItemTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath itemPath = "/nonExistingItem";

            // Act and Assert
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Item[itemPath]);
        }

        [TestMethod]
        public void IndexerAdapter_Item_SetExistingItemTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data1 = [1, 2, 3];
            BinaryData data2 = [4, 5, 6];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item1 = (itemName, data1);
            VirtualItem<BinaryData> item2 = (itemName, data2);
            VirtualPath itemPath = "/testItem";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath.DirectoryPath, item1);

            // 仮想ストレージにアイテムを上書き
            vs.Item[itemPath] = item2;

            // Act
            VirtualItem<BinaryData> retrievedItem = vs.Item[itemPath];

            // Assert
            Assert.AreEqual(item2.Name, retrievedItem.Name);
            CollectionAssert.AreEqual(item2.ItemData!.Data, retrievedItem.ItemData!.Data);
            Assert.IsTrue(retrievedItem.IsReferencedInStorage);
            Assert.AreNotSame(item2, retrievedItem);
            Assert.AreSame(item2.ItemData, retrievedItem.ItemData);
        }

        [TestMethod]
        public void IndexerAdapter_Item_SetExistingItemByItemDataTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data1 = [1, 2, 3];
            BinaryData data2 = [4, 5, 6];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item1 = (itemName, data1);
            VirtualItem<BinaryData> item2 = (itemName, data2);
            VirtualPath itemPath = "/testItem";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath.DirectoryPath, item1);

            // 仮想ストレージにデータで上書き
            // VirtualItem<T>は implicit operatorでVirtualItemData<T>に変換される
            vs.Item[itemPath] = data2;

            // Act
            VirtualItem<BinaryData> retrievedItem = vs.Item[itemPath];

            // Assert
            Assert.AreEqual(item2.Name, retrievedItem.Name);
            CollectionAssert.AreEqual(item2.ItemData!.Data, retrievedItem.ItemData!.Data);
            Assert.IsTrue(retrievedItem.IsReferencedInStorage);
            Assert.AreNotSame(item2, retrievedItem);
            Assert.AreSame(item2.ItemData, retrievedItem.ItemData);
        }

        [TestMethod]
        public void IndexerAdapter_Item_SetItemWithInvalidPathTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data = [1, 2, 3];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item = (itemName, data);
            VirtualPath invalidPath = "/nonexistent/directory/path";

            // Act and Assert
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Item[invalidPath] = item);
        }

        [TestMethod]
        public void IndexerAdapter_Item_GetAndSetIndexerTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data1 = [1, 2, 3];
            BinaryData data2 = [4, 5, 6];
            VirtualNodeName itemName1 = "testItem1";
            VirtualNodeName itemName2 = "testItem2";
            VirtualItem<BinaryData> item1 = (itemName1, data1);
            VirtualItem<BinaryData> item2 = (itemName2, data2);
            VirtualPath itemPath1 = "/testItem1";
            VirtualPath itemPath2 = "/testItem2";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath1.DirectoryPath, item1);
            vs.AddItem(itemPath2.DirectoryPath, item2);

            // Act
            vs.Item[itemPath1] = vs.Item[itemPath2];

            // Assert
            VirtualItem<BinaryData> retrievedItem1 = vs.Item[itemPath1];
            VirtualItem<BinaryData> retrievedItem2 = vs.Item[itemPath2];

            // itemPath1の確認
            Assert.AreEqual(itemName1, retrievedItem1.Name); // 名前は変わらない
            CollectionAssert.AreEqual(item2.ItemData!.Data, retrievedItem1.ItemData!.Data); // データは同じ

            // ノードの実体が異なることを確認
            Assert.AreNotSame(retrievedItem1, retrievedItem2);

            // Dataの実体が異なることを確認
            Assert.AreNotSame(retrievedItem1.ItemData, retrievedItem2.ItemData);
        }

        [TestMethod]
        public void IndexerAdapter_Item_InvalidCastTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName dirName = "testDir";
            VirtualDirectory dir = new(dirName);
            VirtualPath dirPath = "/testDir";

            // 仮想ストレージにディレクトリを追加
            vs.AddDirectory(dirPath.DirectoryPath, dir);

            // Act and Assert
            Exception err = Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Item[dirPath]);

            Assert.AreEqual("Path not found. [/testDir]", err.Message);

            Debug.WriteLine(err.Message);
        }

        [TestMethod]
        public void IndexerAdapter_Directory_GetExistingDirectoryTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName dirName = "testDir";
            VirtualDirectory dir = new(dirName);
            VirtualPath dirPath = "/testDir";

            // 仮想ストレージにディレクトリを追加
            vs.AddDirectory(dirPath.DirectoryPath, dir);

            // Act
            VirtualDirectory retrievedDir = vs[dirPath];

            // Assert
            Assert.AreEqual(dir.Name, retrievedDir.Name);
            Assert.IsTrue(retrievedDir.IsReferencedInStorage);
        }

        [TestMethod]
        public void IndexerAdapter_Directory_GetNonExistingDirectoryTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath dirPath = "/nonExistingDir";

            // Act and Assert
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs[dirPath]);
        }

        [TestMethod]
        public void IndexerAdapter_Directory_SetExistingDirectoryTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath dir1Path = "/dir1";
            VirtualDirectory dir2 = new("dir2");

            vs.AddDirectory(dir1Path);

            // Act
            vs[dir1Path] = dir2;

            // Assert
            VirtualDirectory retrievedDir = vs[dir1Path];
            Assert.AreEqual(dir1Path.NodeName, retrievedDir.Name);
            Assert.IsTrue(retrievedDir.IsReferencedInStorage);
            Assert.AreNotSame(dir2, retrievedDir);
        }

        [TestMethod]
        public void IndexerAdapter_Directory_SetExistingDirectoryWithItemsTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath dir1Path = "/dir1";
            VirtualPath dir2Path = "/dir2";

            BinaryData data1 = [1, 2, 3];
            BinaryData data2 = [4, 5, 6];

            VirtualItem<BinaryData> item1 = new("item1", data1);
            VirtualItem<BinaryData> item2a = new("item2", data1);
            VirtualItem<BinaryData> item2b = new("item2", data2);
            VirtualItem<BinaryData> item3 = new("item3", data2);

            vs.AddDirectory(dir1Path);
            vs.AddDirectory(dir2Path);

            vs.AddItem(dir1Path, item1);
            vs.AddItem(dir1Path, item2a);
            vs.AddItem(dir2Path, item2b);
            vs.AddItem(dir2Path, item3);

            // Act
            vs[dir1Path] = vs[dir2Path];

            // Assert
            VirtualDirectory retrievedDir = vs[dir1Path];

            // /dir1の確認
            Assert.AreEqual(dir1Path.NodeName, retrievedDir.Name);
            Assert.IsTrue(retrievedDir.IsReferencedInStorage);

            // /dir1/item1の確認
            VirtualItem<BinaryData> retrievedItem1 = vs.Item[dir1Path + "item1"];
            CollectionAssert.AreEqual(data1.Data, retrievedItem1.ItemData!.Data);
            Assert.IsTrue(retrievedItem1.IsReferencedInStorage);

            // /dir1/item2の確認
            VirtualItem<BinaryData> retrievedItem2 = vs.Item[dir1Path + "item2"];
            CollectionAssert.AreEqual(data2.Data, retrievedItem2.ItemData!.Data);
            Assert.IsTrue(retrievedItem2.IsReferencedInStorage);

            // /dir1/item3の確認
            VirtualItem<BinaryData> retrievedItem3 = vs.Item[dir1Path + "item3"];
            CollectionAssert.AreEqual(data2.Data, retrievedItem3.ItemData!.Data);
            Assert.IsTrue(retrievedItem3.IsReferencedInStorage);

            // 各ノードの実体が個別であることを確認
            Assert.AreNotSame(vs[dir1Path], vs[dir2Path]);
            Assert.AreNotSame(vs.Item[dir1Path + "item2"], vs.Item[dir2Path + "item2"]);

            // 各アイテムのデータの実体が個別であることを確認
            Assert.AreNotSame(vs.Item[dir2Path + "item2"].ItemData,
                              vs.Item[dir1Path + "item2"].ItemData);
        }

        [TestMethod]
        public void IndexerAdapter_Directory_SetVirtualDirectoryInstanceWithItemsTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath dir1Path = "/dir1";

            BinaryData data1 = [1, 2, 3];
            BinaryData data2 = [4, 5, 6];

            VirtualItem<BinaryData> item1 = new("item1", data1);
            VirtualItem<BinaryData> item2a = new("item2", data1);
            VirtualItem<BinaryData> item2b = new("item2", data2);
            VirtualItem<BinaryData> item3 = new("item3", data2);
            VirtualDirectory subDir1 = new("subDir1");

            vs.AddDirectory(dir1Path);

            vs.AddItem(dir1Path, item1);
            vs.AddItem(dir1Path, item2a);

            VirtualDirectory dir2 = new("dir2")
            {
                item2b,
                item3,
                subDir1
            };

            // ディレクトリ構造を出力
            Debug.WriteLine("処理前:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/"), true, false);

            // Act
            vs[dir1Path] = dir2;

            // ディレクトリ構造を出力
            Debug.WriteLine("処理後:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/"), true, false);

            // Assert
            VirtualDirectory retrievedDir = vs[dir1Path];

            // /dir1の確認
            Assert.AreEqual(dir1Path.NodeName, retrievedDir.Name);
            Assert.IsTrue(retrievedDir.IsReferencedInStorage);

            // /dir1/item1の確認
            VirtualItem<BinaryData> retrievedItem1 = vs.Item[dir1Path + "item1"];
            CollectionAssert.AreEqual(data1.Data, retrievedItem1.ItemData!.Data);
            Assert.IsTrue(retrievedItem1.IsReferencedInStorage);

            // /dir1/item2の確認
            VirtualItem<BinaryData> retrievedItem2 = vs.Item[dir1Path + "item2"];
            CollectionAssert.AreEqual(data2.Data, retrievedItem2.ItemData!.Data);
            Assert.IsTrue(retrievedItem2.IsReferencedInStorage);

            // /dir1/item3の確認
            VirtualItem<BinaryData> retrievedItem3 = vs.Item[dir1Path + "item3"];
            CollectionAssert.AreEqual(data2.Data, retrievedItem3.ItemData!.Data);
            Assert.IsTrue(retrievedItem3.IsReferencedInStorage);

            // /dir1/subDir1の確認
            VirtualDirectory retrievedSubDir1 = vs[dir1Path + "subDir1"];
            Assert.AreEqual(subDir1.Name, retrievedSubDir1.Name);
            Assert.IsTrue(retrievedSubDir1.IsReferencedInStorage);

            // 各ノードの実体が個別であることを確認
            Assert.AreNotSame(vs[dir1Path], dir2);
            Assert.AreNotSame(vs.Item[dir1Path + "item2"], dir2.Get("item2"));

            // 各アイテムのデータの実体が個別であることを確認
            Assert.AreNotSame(((VirtualItem<BinaryData>?)dir2.Get("item2"))!.ItemData,
                              vs.Item[dir1Path + "item2"].ItemData);

            // 各サブディレクトリの実体が個別であることを確認
            Assert.AreNotSame(vs[dir1Path + "subDir1"], dir2.Get("subDir1"));
        }

        [TestMethod]
        public void IndexerAdapter_Directory_InvalidCastTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data = [1, 2, 3];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item = (itemName, data);
            VirtualPath itemPath = "/testItem";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath.DirectoryPath, item);

            // Act and Assert
            Exception err = Assert.ThrowsException<InvalidOperationException>(() => vs[itemPath]);

            Assert.AreEqual("The specified node [testItem] is not of type VirtualDirectory.", err.Message);

            Debug.WriteLine(err.Message);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_GetExistingSymbolicLinkTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName linkName = "testLink";
            VirtualPath targetPath = "/targetPath";
            VirtualSymbolicLink link = new(linkName, targetPath);
            VirtualPath linkPath = "/testLink";

            // 仮想ストレージにシンボリックリンクを追加
            vs.AddSymbolicLink(linkPath.DirectoryPath, link);

            // Act
            // followLinks=false リンク解決はせず、リンクそのものを扱う
            VirtualSymbolicLink retrievedLink = vs.Link[linkPath];

            // Assert
            Assert.AreEqual(link.Name, retrievedLink.Name);
            Assert.AreEqual(link.TargetPath, retrievedLink.TargetPath);
            Assert.IsTrue(retrievedLink.IsReferencedInStorage);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_GetNonExistingSymbolicLinkTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualPath linkPath = "/nonExistingLink";

            // Act and Assert
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Link[linkPath]);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_SetExistingSymbolicLinkTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName linkName = "testLink";
            VirtualPath targetPath1 = "/targetPath1";
            VirtualPath targetPath2 = "/targetPath2";
            VirtualSymbolicLink link1 = new(linkName, targetPath1);
            VirtualSymbolicLink link2 = new(linkName, targetPath2);
            VirtualPath linkPath = "/testLink";

            // 仮想ストレージにシンボリックリンクを追加
            vs.AddSymbolicLink(linkPath.DirectoryPath, link1);

            // 仮想ストレージにシンボリックリンクを上書き
            vs.Link[linkPath] = link2;

            // Act
            // followLinks=false リンク解決はせず、リンクそのものを扱う
            VirtualSymbolicLink retrievedLink = vs.Link[linkPath];

            // Assert
            Assert.AreEqual(link2.Name, retrievedLink.Name);
            Assert.AreEqual(link2.TargetPath, retrievedLink.TargetPath);
            Assert.IsTrue(retrievedLink.IsReferencedInStorage);
            Assert.AreNotSame(link2, retrievedLink);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_SetExistingSymbolicLinkByTargetPathTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName linkName = "testLink";
            VirtualPath targetPath1 = "/targetPath1";
            VirtualPath targetPath2 = "/targetPath2";
            VirtualSymbolicLink link1 = new(linkName, targetPath1);
            VirtualSymbolicLink link2 = new(linkName, targetPath2);
            VirtualPath linkPath = "/testLink";

            // 仮想ストレージにシンボリックリンクを追加
            vs.AddSymbolicLink(linkPath.DirectoryPath, link1);

            // 仮想ストレージにターゲットパスで上書き
            // VirtualSymbolicLinkは implicit operatorでVirtualSymbolicLinkに変換される
            vs.Link[linkPath] = targetPath2;

            // Act
            // followLinks=false リンク解決はせず、リンクそのものを扱う
            VirtualSymbolicLink retrievedLink = vs.Link[linkPath];

            // Assert
            Assert.AreEqual(link2.Name, retrievedLink.Name);
            Assert.AreEqual(link2.TargetPath, retrievedLink.TargetPath);
            Assert.IsTrue(retrievedLink.IsReferencedInStorage);
            Assert.AreNotSame(link2, retrievedLink);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_SetSymbolicLinkWithInvalidPathTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName linkName = "testLink";
            VirtualPath targetPath = "/targetPath";
            VirtualSymbolicLink link = new(linkName, targetPath);
            VirtualPath invalidPath = "/nonexistent/directory/path";  // 存在しないディレクトリへのパス

            // Act and Assert
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Link[invalidPath] = link);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_GetAndSetIndexerTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            VirtualNodeName linkName1 = "testLink1";
            VirtualNodeName linkName2 = "testLink2";
            VirtualPath targetPath1 = "/targetPath1";
            VirtualPath targetPath2 = "/targetPath2";
            VirtualSymbolicLink link1 = new(linkName1, targetPath1);
            VirtualSymbolicLink link2 = new(linkName2, targetPath2);
            VirtualPath linkPath1 = "/testLink1";
            VirtualPath linkPath2 = "/testLink2";

            // 仮想ストレージにシンボリックリンクを追加
            vs.AddSymbolicLink(linkPath1.DirectoryPath, link1);
            vs.AddSymbolicLink(linkPath2.DirectoryPath, link2);

            // Act
            // followLinks=false リンク解決はせず、リンクそのものを扱う
            vs.Link[linkPath1] = vs.Link[linkPath2];

            // Assert
            VirtualSymbolicLink retrievedLink1 = vs.Link[linkPath1];
            VirtualSymbolicLink retrievedLink2 = vs.Link[linkPath2];

            // linkPath1の確認
            Assert.AreEqual(linkName1, retrievedLink1.Name); // 名前は変わらない
            Assert.AreEqual(targetPath2, retrievedLink1.TargetPath); // ターゲットパスは同じ
            Assert.IsTrue(retrievedLink1.IsReferencedInStorage);

            // ノードの実体が異なることを確認
            Assert.AreNotSame(retrievedLink1, retrievedLink2);

            // TargetPathの実体が同じであることを確認
            // (VirtualPath内の_pathは string でありイミュータブルなので問題なし)
            Assert.AreSame(retrievedLink1.TargetPath, retrievedLink2.TargetPath);
        }

        [TestMethod]
        public void IndexerAdapter_OperatorPlusTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data1 = [1, 2, 3];
            VirtualNodeName dirName1 = "dir1";
            VirtualNodeName dirName2 = "dir2";
            VirtualNodeName itemName = "item1";
            VirtualNodeName linkName = "link1";
            VirtualPath targetPath = "/targetPath";
            VirtualDirectory dir1 = new(dirName1);
            VirtualDirectory dir2 = new(dirName2);
            VirtualItem<BinaryData> item1 = new(itemName, data1);
            VirtualSymbolicLink link1 = new(linkName, targetPath);

            // ディレクトリ構造を出力
            Debug.WriteLine("処理前:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/", true, false));

            // Act
            vs["/"] += dir1;
            vs["/dir1"] += dir2;
            vs["/dir1"] += item1;
            vs["/dir1"] += link1;

            // ディレクトリ構造を出力
            Debug.WriteLine("処理後:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/", true, false));

            // Assert
            Assert.AreEqual(dir1.Name, vs["/dir1"].Name);
            Assert.AreEqual(dir2.Name, vs["/dir1/dir2"].Name);
            Assert.AreEqual(item1.Name, vs.Item["/dir1/item1"].Name);
            CollectionAssert.AreEqual(data1.Data, vs.Item["/dir1/item1"].ItemData!.Data);
            Assert.AreEqual(link1.Name, vs.Link["/dir1/link1"].Name);
            Assert.AreEqual(link1.TargetPath, vs.Link["/dir1/link1"].TargetPath);
            Assert.IsTrue(vs["/"].IsReferencedInStorage);
            Assert.IsTrue(vs["/dir1"].IsReferencedInStorage);
            Assert.IsTrue(vs["/dir1/dir2"].IsReferencedInStorage);
            Assert.IsTrue(vs.Item["/dir1/item1"].IsReferencedInStorage);
            Assert.IsTrue(vs.Link["/dir1/link1"].IsReferencedInStorage);
        }

        [TestMethod]
        public void IndexerAdapter_OperatorMinusTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data1 = [1, 2, 3];
            VirtualNodeName dirName1 = "dir1";
            VirtualNodeName dirName2 = "dir2";
            VirtualNodeName itemName = "item1";
            VirtualNodeName linkName = "link1";
            VirtualPath targetPath = "/targetPath";
            VirtualDirectory dir1 = new(dirName1);
            VirtualDirectory dir2 = new(dirName2);
            VirtualItem<BinaryData> item1 = new(itemName, data1);
            VirtualSymbolicLink link1 = new(linkName, targetPath);

            // ディレクトリ構造を出力
            Debug.WriteLine("処理前:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/", true, false));

            // Act - ノードを追加
            vs["/"] += dir1;
            vs["/dir1"] += dir2;
            vs["/dir1"] += item1;
            vs["/dir1"] += link1;

            // ディレクトリ構造を出力
            Debug.WriteLine("追加後:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/", true, false));

            // ノードを取得
            VirtualDirectory retrievedDir2 = vs["/dir1/dir2"];
            VirtualItem<BinaryData> retrievedItem1 = vs.Item["/dir1/item1"];
            VirtualSymbolicLink retrievedLink1 = vs.Link["/dir1/link1"];

            // Act - ノードを削除
            vs["/dir1"] -= dir2;
            vs["/dir1"] -= item1;
            vs["/dir1"] -= link1;

            // ディレクトリ構造を出力
            Debug.WriteLine("削除後:");
            Debug.WriteLine(vs.GenerateTreeDebugText("/", true, false));

            // Assert - ノードが削除されたことを確認
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs["/dir1/dir2"]);
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Item["/dir1/item1"]);
            Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Link["/dir1/link1"]);

            // 削除されたノードのIsReferencedInStorageフラグを確認
            Assert.IsFalse(retrievedDir2.IsReferencedInStorage);
            Assert.IsFalse(retrievedItem1.IsReferencedInStorage);
            Assert.IsFalse(retrievedLink1.IsReferencedInStorage);

            Assert.IsTrue(vs["/"].IsReferencedInStorage);
            Assert.IsTrue(vs["/dir1"].IsReferencedInStorage);
        }

        [TestMethod]
        public void IndexerAdapter_SymbolicLink_InvalidCastTest()
        {
            // Arrange
            VirtualStorage<BinaryData> vs = new();
            BinaryData data = [1, 2, 3];
            VirtualNodeName itemName = "testItem";
            VirtualItem<BinaryData> item = (itemName, data);
            VirtualPath itemPath = "/testItem";

            // 仮想ストレージにアイテムを追加
            vs.AddItem(itemPath.DirectoryPath, item);

            // Act and Assert
            Exception err = Assert.ThrowsException<VirtualPathNotFoundException>(() => vs.Link[itemPath]);

            Assert.AreEqual("Path not found. [/testItem]", err.Message);

            Debug.WriteLine(err.Message);
        }
    }
}
