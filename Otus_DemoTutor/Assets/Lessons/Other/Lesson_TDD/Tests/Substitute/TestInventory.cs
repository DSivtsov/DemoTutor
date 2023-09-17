// using System.Collections.Generic;
// using System.Linq;
//
// namespace Lessons.TDD
// {
//     public sealed class TestInventory : IInventory
//     {
//         private List<string> items;
//         
//         public TestInventory(params string[] items)
//         {
//             this.items = new List<string>(items);
//         }
//
//         public void Setup(params string[] items)
//         {
//             this.items = new List<string>(items);
//         }
//
//         public int GetCount(string item)
//         {
//             return this.items.Count(it => it == item);
//         }
//
//         public void Add(string item)
//         {
//             this.items.Add(item);
//         }
//
//         public void RemoveAll(string item, int count)
//         {
//             for (int i = 0; i < count; i++)
//             {
//                 this.items.Remove(item);
//             }
//         }
//     }
// }