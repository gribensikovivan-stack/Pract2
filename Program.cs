using System;
using System.Collections.Generic;
using pract2;

namespace pract2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garden garden = new Garden();

            while (true)
            {
                Console.WriteLine("\nЧто вы хотите сделать?\n1 - добавить дерево в сад\n" +
                                  "2 - добавить куст в сад\n3 - отобразить растения в саду\n" +
                                  "4 - взаимодействовать с деревом\n5 - взаимодействовать с кустом\n" +
                                  "6 - завершить работу");

                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Некорректный ввод, введите число в диапазоне 1-6.");
                    continue;
                }

                try
                {
                    switch (input)
                    {
                        case 1:
                            var newTree = CreateTree();
                            garden.AddPlant(newTree);
                            break;

                        case 2:
                            var newShrub = CreateShrub();
                            garden.AddPlant(newShrub);
                            break;

                        case 3:
                            garden.ShowPlants();
                            break;

                        case 4:
                            InteractWithTree(garden);
                            break;

                        case 5:
                            InteractWithShrub(garden);
                            break;

                        case 6:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }
        }

        // Общий метод для ввода данных растения (рост, тип, имя)
        static (double height, PlantTypes.TreeType type, string name) InputPlantData()
        {
            Console.Write("Введите высоту в метрах (напр. 12,3): ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Выберите тип (0 - Oak, 1 - Pine, 2 - Birch, 3 - Maple, 4 - None): ");
            var type = (PlantTypes.TreeType)int.Parse(Console.ReadLine());
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            return (height, type, name);
        }

        static Tree CreateTree()
        {
            var (height, type, name) = InputPlantData();
            Console.Write("Введите возраст дерева: ");
            double age = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите количество фруктов на дереве: ");
            int countFruit = Convert.ToInt32(Console.ReadLine());
            Console.Write("Дерево больное? (0 = нет, 1 = да): ");
            bool treeSick = Convert.ToBoolean(int.Parse(Console.ReadLine()));

            return new Tree(new PlantTypes.Height(height), type, name, age, countFruit, treeSick);
        }

        static Shrub CreateShrub()
        {
            var (height, type, name) = InputPlantData();
            return new Shrub(new PlantTypes.Height(height), type, name);
        }

        static void InteractWithTree(Garden garden)
        {
            var trees = garden.GetPlantsOfType<Tree>(); // получаем все объекты типа Tree
            if (trees.Count == 0)
            {
                Console.WriteLine("В саду нет деревьев!");
                return;
            }

            Console.WriteLine("Выберите дерево по индексу:");
            for (int i = 0; i < trees.Count; i++)
                Console.WriteLine($"{i} - {trees[i].Name}");

            int index = int.Parse(Console.ReadLine());
            var tree = trees[index];

            Console.WriteLine("1 - Показать свойства дерева\n2 - Собрать урожай\n" +
                              "3 - Позаботиться о дереве\n4 - Потрогать под вдохновляющую музыку\n" +
                              "5 - Объединить это дерево с другим\n6 - Скопировать свойства другого дерева");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    Console.WriteLine(tree); // вызывает tree.ToString()
                    break;
                case 2:
                    Console.Write($"Сколько фруктов собрать (сейчас на дереве {trees[index].CountFruit} фруктов)?");
                    int amount = int.Parse(Console.ReadLine());
                    int harvested = tree.Harvesting(amount);
                    Console.WriteLine($"Собрано {harvested} фруктов.");
                    break;
                case 3:
                    tree.TakeCare();
                    break;
                case 4:
                    tree.Touch();
                    break;
                case 5:
                    Console.WriteLine("Выберите второе дерево для объединения:");
                    for (int i = 0; i < trees.Count; i++)
                        if (i != index)
                            Console.WriteLine($"{i} - {trees[i].Name}");

                    int secondIndex = int.Parse(Console.ReadLine());
                    var combinedTree = tree + trees[secondIndex];  // используется перегруженный оператор +

                    garden.AddPlant(combinedTree);
                    Console.WriteLine("Создано новое дерево:");
                    Console.WriteLine(combinedTree);
                    break;
                case 6:
                    Console.WriteLine("Выберите дерево, с которого скопировать свойства:");
                    for (int i = 0; i < trees.Count; i++)
                        if (i != index)
                            Console.WriteLine($"{i} - {trees[i].Name}");

                    int copyIndex = int.Parse(Console.ReadLine());
                    tree.CopyForm(trees[copyIndex]);
                    Console.WriteLine($"Свойства дерева {trees[copyIndex].Name} скопированы в {tree.Name}");
                    break;
            }
        }

        static void InteractWithShrub(Garden garden)
        {
            var shrubs = garden.GetPlantsOfType<Shrub>();
            if (shrubs.Count == 0)
            {
                Console.WriteLine("В саду нет кустов!");
                return;
            }

            Console.WriteLine("Выберите куст по индексу:");
            for (int i = 0; i < shrubs.Count; i++)
                Console.WriteLine($"{i} - {shrubs[i].Name}");

            int index = int.Parse(Console.ReadLine());
            var shrub = shrubs[index];

            Console.Write("На сколько вырастить куст в метрах? ");
            double growAmount = double.Parse(Console.ReadLine());
            shrub.Grow(growAmount);
        }
    }
}
