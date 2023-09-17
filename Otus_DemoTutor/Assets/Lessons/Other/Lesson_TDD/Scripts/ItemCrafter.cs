// using System;
//
// namespace Lessons.TDD
// {
//     public sealed class ItemCrafter
//     {
//         private readonly IInventory inventory;
//
//         public ItemCrafter(IInventory inventory)
//         {
//             this.inventory = inventory;
//         }
//
//         public bool CanCraft(ItemReceipt receipt)
//         {
//             foreach (var ingredient in receipt.ingredients)
//             {
//                 if (inventory.GetCount(ingredient.item) < ingredient.count)
//                 {
//                     return false;
//                 }
//             }
//
//             return true;
//         }
//
//         public void Craft(ItemReceipt receipt) //Receipt
//         {
//             foreach (var ingredient in receipt.ingredients)
//             {
//                 if (inventory.GetCount(ingredient.item) < ingredient.count)
//                 {
//                     throw new Exception($"Not enough item {ingredient.item} {ingredient.count}");
//                 }
//             }
//
//             foreach (var ingredient  in receipt.ingredients)
//             {
//                 inventory.RemoveAll(ingredient.item, ingredient.count);
//             }
//
//             inventory.Add(receipt.craftItem);
//         }
//     }
// }