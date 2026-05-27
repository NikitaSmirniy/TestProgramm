using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Item iPhone12 = new Item("IPhone 12");
            Item iPhone11 = new Item("IPhone 11");

            ItemsWarehouse itemsWarehouse = new ItemsWarehouse();

            Shop shop = new Shop(itemsWarehouse);

            itemsWarehouse.AddItem(iPhone12, 10);
            itemsWarehouse.AddItem(iPhone11, 1);

            itemsWarehouse.ShowItems();

            Cart cart = shop.CreateCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3);

            cart.ShowGoods();
            
            int paylink = cart.GetPaylink();

            Console.WriteLine(($"Paylink: {paylink}"));

            cart.Add(iPhone12, 9);

            itemsWarehouse.Remove(cart.GetOrder());

            Console.ReadLine();
        }
    }

    public class Cart
    {
        private readonly IItemsWarehouse _itemsWarehouse;
        private readonly List<Cell> _cellsOfItem = new List<Cell>();

        public Cart(IItemsWarehouse itemsWarehouse)
        {
            _itemsWarehouse = itemsWarehouse;
        }

        public void Add(Item item, int count)
        {
            if (_itemsWarehouse.Contains(item, count, out Cell _))
                _cellsOfItem.Add(new Cell(item, count));
        }

        public void ShowGoods()
        {
            foreach (Cell cell in _cellsOfItem)
            {
                Console.WriteLine($"{cell.Item.Name}: {cell.Count}");
            }
        }

        public IReadOnlyList<Cell> GetOrder()
        {
            return _cellsOfItem;
        }

        public int GetPaylink()
        {
            int paylink = 0;

            foreach (Cell cell in _cellsOfItem)
                paylink += cell.Count;

            return paylink;
        }
    }

    public class Shop
    {
        private readonly ItemsWarehouse _itemsWarehouse;

        public Shop(ItemsWarehouse itemsWarehouse)
        {
            _itemsWarehouse = itemsWarehouse;
        }

        public Cart CreateCart()
        {
            return new Cart(_itemsWarehouse);
        }
    }

    public class ItemsWarehouse : IItemsWarehouse
    {
        private List<Cell> _cells = new List<Cell>();

        public void AddItem(Item item, int count)
        {
            ThrowHelper.ValidateEnteringItemData(item, count);

            foreach (var cell in _cells)
            {
                if (cell.Item == item)
                {
                    cell.AddItemCount(count);

                    return;
                }
            }

            _cells.Add(new Cell(item, count));
        }

        public bool Contains(Item item, int count, out Cell foundCell)
        {
            ThrowHelper.ValidateEnteringItemData(item, count);

            foreach (var cell in _cells)
            {
                if (cell.Item == item)
                {
                    foundCell = cell;
                    return true;
                }
            }

            foundCell = null;

            return false;
        }

        public void Remove(IReadOnlyList<Cell> cells)
        {
            foreach (var cell in cells)
            {
                if (Contains(cell.Item, cell.Count, out Cell foundCell))
                    cell.RemoveItemCount(foundCell.Count);

                if (foundCell.IsEmpty)
                    _cells.Remove(cell);
            }
        }

        public void ShowItems()
        {
            foreach (Cell cell in _cells)
                Console.WriteLine($"{cell.Item.Name}: {cell.Count}");
        }
    }

    public interface IItemsWarehouse
    {
        bool Contains(Item item, int count, out Cell foundCell);
    }

    public class Cell
    {
        public Cell(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public readonly Item Item;

        public int Count { get; private set; }

        public bool IsEmpty => Count <= 0;

        public void AddItemCount(int count)
        {
            ThrowHelper.ValidateEnteringItemData(count);

            Count += count;
        }

        public void RemoveItemCount(int count)
        {
            ThrowHelper.ValidateEnteringItemData(Count, count);

            Count -= count;
        }
    }

    public class Item
    {
        public Item(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public static class ThrowHelper
    {
        public static void ValidateEnteringItemData(Item item, int count)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
        }

        public static void ValidateEnteringItemData(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
        }

        public static void ValidateEnteringItemData(int currentCount, int deductibleCount)
        {
            if (deductibleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(deductibleCount));

            int itemAmount = currentCount - deductibleCount;
            if (itemAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(itemAmount));
        }
    }
}