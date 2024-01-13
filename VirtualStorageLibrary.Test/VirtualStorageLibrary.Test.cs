namespace VirtualStorageLibrary.Test
{
    [TestClass]
    public class VirtualItemTest
    {
        [TestMethod]
        public void VirtualItemConstructor_CreatesObjectCorrectly()
        {
            // テストデータ
            var testData = new byte[] { 1, 2, 3 };

            // BinaryData オブジェクトを作成
            var binaryData = new BinaryData(testData);

            // VirtualItem<BinaryData> オブジェクトを作成
            string name = "TestBinaryItem";
            var virtualItem = new VirtualItem<BinaryData>(name, binaryData);

            // オブジェクトが正しく作成されたか検証
            Assert.IsNotNull(virtualItem);
            Assert.AreEqual(name, virtualItem.Name);
            Assert.AreEqual(binaryData, virtualItem.Item);
            CollectionAssert.AreEqual(virtualItem.Item.Data, testData);
        }

        [TestMethod]
        public void VirtualItemDeepClone_CreatesDistinctCopyWithSameData()
        {
            // テストデータ
            var testData = new byte[] { 1, 2, 3 };

            // BinaryData オブジェクトを作成
            var binaryData = new BinaryData(testData);

            // VirtualItem<BinaryData> オブジェクトを作成
            string name = "TestBinaryItem";
            var originalVirtualItem = new VirtualItem<BinaryData>(name, binaryData);

            // DeepClone メソッドを使用してクローンを作成
            var clonedVirtualItem = originalVirtualItem.DeepClone() as VirtualItem<BinaryData>;

            // クローンが正しく作成されたか検証
            Assert.IsNotNull(clonedVirtualItem);
            Assert.AreNotSame(originalVirtualItem, clonedVirtualItem);
            Assert.AreEqual(originalVirtualItem.Name, clonedVirtualItem.Name);
            Assert.AreNotSame(originalVirtualItem.Item, clonedVirtualItem.Item);
            CollectionAssert.AreEqual(originalVirtualItem.Item.Data, clonedVirtualItem.Item.Data);
        }
    }

    [TestClass]
    public class VirtualDirectoryTests
    {
        [TestMethod]
        public void VirtualDirectoryConstructor_CreatesObjectCorrectly()
        {
            // VirtualDirectory オブジェクトを作成
            string name = "TestDirectory";
            var virtualDirectory = new VirtualDirectory(name);

            // オブジェクトが正しく作成されたか検証
            Assert.IsNotNull(virtualDirectory);
            Assert.AreEqual(name, virtualDirectory.Name);
            Assert.IsNotNull(virtualDirectory.Nodes);
            Assert.AreEqual(0, virtualDirectory.Nodes.Count);
        }

        [TestMethod]
        public void VirtualDirectoryDeepClone_CreatesDistinctCopyWithSameData()
        {
            // VirtualDirectory オブジェクトを作成し、いくつかのノードを追加
            string name = "TestDirectory";
            var originalDirectory = new VirtualDirectory(name);
            originalDirectory.Nodes.Add("Node1", new VirtualItem<BinaryData>("Item1", new BinaryData()));
            originalDirectory.Nodes.Add("Node2", new VirtualItem<BinaryData>("Item2", new BinaryData()));

            // DeepClone メソッドを使用してクローンを作成
            var clonedDirectory = originalDirectory.DeepClone() as VirtualDirectory;

            // クローンが正しく作成されたか検証
            Assert.IsNotNull(clonedDirectory);
            Assert.AreNotSame(originalDirectory, clonedDirectory);
            Assert.AreEqual(originalDirectory.Name, clonedDirectory.Name);
            Assert.AreNotSame(originalDirectory.Nodes, clonedDirectory.Nodes);
            Assert.AreEqual(originalDirectory.Nodes.Count, clonedDirectory.Nodes.Count);

            // 各ノードも適切にクローンされていることを検証
            foreach (var key in originalDirectory.Nodes.Keys)
            {
                Assert.AreNotSame(originalDirectory.Nodes[key], clonedDirectory.Nodes[key]);
            }
        }

        [TestMethod]
        public void Add_NewNode_AddsNodeCorrectly()
        {
            var directory = new VirtualDirectory("TestDirectory");
            var testData = new byte[] { 1, 2, 3 };
            var newNode = new VirtualItem<BinaryData>("NewItem", new BinaryData(testData));

            directory.Add("NewItemKey", newNode);

            Assert.IsTrue(directory.Nodes.ContainsKey("NewItemKey"));
            Assert.AreEqual(newNode, directory.Nodes["NewItemKey"]);
            CollectionAssert.AreEqual(testData, ((BinaryData)((VirtualItem<BinaryData>)directory.Nodes["NewItemKey"]).Item).Data);
        }

        [TestMethod]
        public void Add_ExistingNodeWithoutOverwrite_ThrowsInvalidOperationException()
        {
            var directory = new VirtualDirectory("TestDirectory");
            var testData = new byte[] { 1, 2, 3 };
            var originalNode = new VirtualItem<BinaryData>("OriginalItem", new BinaryData(testData));
            directory.Add("NodeKey", originalNode);

            var newNode = new VirtualItem<BinaryData>("NewItem", new BinaryData(testData));

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                directory.Add("NodeKey", newNode);
            });
        }

        [TestMethod]
        public void Add_ExistingNodeWithOverwrite_OverwritesNode()
        {
            var directory = new VirtualDirectory("TestDirectory");
            var testData = new byte[] { 1, 2, 3 };
            var originalNode = new VirtualItem<BinaryData>("OriginalItem", new BinaryData(testData));
            directory.Add("NodeKey", originalNode);

            var newTestData = new byte[] { 4, 5, 6 };
            var newNode = new VirtualItem<BinaryData>("NewItem", new BinaryData(newTestData));

            directory.Add("NodeKey", newNode, allowOverwrite: true);

            Assert.AreEqual(newNode, directory.Nodes["NodeKey"]);
            CollectionAssert.AreEqual(newTestData, ((BinaryData)((VirtualItem<BinaryData>)directory.Nodes["NodeKey"]).Item).Data);
        }

        [TestMethod]
        public void Indexer_ValidKey_ReturnsNode()
        {
            var directory = new VirtualDirectory("TestDirectory");
            var node = new VirtualItem<BinaryData>("Item", new BinaryData());
            directory.Add("ItemKey", node);

            var result = directory["ItemKey"];

            Assert.AreEqual(node, result);
        }

        [TestMethod]
        public void Indexer_InvalidKey_ThrowsKeyNotFoundException()
        {
            var directory = new VirtualDirectory("TestDirectory");

            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                var result = directory["InvalidKey"];
            });
        }

        [TestMethod]
        public void Indexer_Setter_UpdatesNode()
        {
            var directory = new VirtualDirectory("TestDirectory");
            var newNode = new VirtualItem<BinaryData>("NewItem", new BinaryData());

            directory["NewItemKey"] = newNode;
            var result = directory["NewItemKey"];

            Assert.AreEqual(newNode, result);
        }
    }
}
