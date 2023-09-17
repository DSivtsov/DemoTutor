// using Lessons.TDD;
// using NUnit.Framework;
//
// [TestFixture]
// public sealed class CraftingTests
// {
//     private TestInventory testInventory;
//     private ItemCrafter itemCrafter;
//
//     private readonly ItemReceipt axeReceipt = new ItemReceipt("Axe",
//         new ItemIngredient("Wood", 2),
//         new ItemIngredient("Stone", 1)
//     );
//     
//     private readonly ItemReceipt swordReceipt = new ItemReceipt("Sword",
//         new ItemIngredient("Wood", 1),
//         new ItemIngredient("Stone", 1),
//         new ItemIngredient("Steal", 1)
//     );
//
//     [SetUp]
//     public void Setup()
//     {
//         this.testInventory = new TestInventory();
//         this.itemCrafter = new ItemCrafter(this.testInventory);
//     }
//
//     [Test]
//     public void CraftAxeTest()
//     {
//         //Arrange:
//         this.testInventory.Setup(
//             "Wood",
//             "Wood",
//             "Wood",
//             "Wood",
//             "Wood",
//             "Stone",
//             "Stone"
//         );
//
//         //Act:
//         this.itemCrafter.Craft(this.axeReceipt);
//
//         //Assert:
//         Assert.True(this.testInventory.GetCount("Wood") == 3);
//         Assert.True(this.testInventory.GetCount("Stone") == 1);
//         Assert.True(this.testInventory.GetCount("Axe") == 1);
//     }
//
//
//     [Test]
//     public void CraftSwordTest()
//     {
//         //Arrange:
//         this.testInventory.Setup(
//             "Wood",
//             "Wood",
//             "Wood",
//             "Stone",
//             "Stone",
//             "Steal"
//         );
//
//         //Act:
//         this.itemCrafter.Craft(this.swordReceipt);
//
//         //Assert:
//         Assert.True(this.testInventory.GetCount("Wood") == 2);
//         Assert.True(this.testInventory.GetCount("Stone") == 1);
//         Assert.True(this.testInventory.GetCount("Steal") == 0);
//         Assert.True(this.testInventory.GetCount("Sword") == 1);
//     }
//     
//     [Test]
//     public void CanCraftSwordTest()
//     {
//         //Arrange:
//         this.testInventory.Setup(
//             "Wood",
//             "Stone",
//             "Steal"
//         );
//
//         //Act:
//         var canCraft = this.itemCrafter.CanCraft(this.swordReceipt);
//
//         //Assert:
//         Assert.True(canCraft);
//     }
//     
//     [Test]
//     public void CannotCraftSwordTest()
//     {
//         //Arrange:
//         this.testInventory.Setup(
//             "Steal"
//         );
//
//         //Act:
//         var canCraft = this.itemCrafter.CanCraft(this.swordReceipt);
//
//         //Assert:
//         Assert.False(canCraft);
//     }
// }