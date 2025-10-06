using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace pract2
{
    internal class Tree : Plant
    {
        private double _age;
        private int _countFruit;
        private bool _treeSick;

        public double Age
        {
            get => _age;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Возраст должен быть положительным");
                _age = value;
            }
        }

        public int CountFruit
        {
            get => _countFruit;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество фруктов должно быть положительным");
                _countFruit = value;
            }
        }

        public bool TreeSick
        {
            get => _treeSick;
            set => _treeSick = value;
        }

        public Tree()
        {
            _age = 0;
            _countFruit = 0;
            _treeSick = false;
        }

        public Tree(Height height, TreeType type, string name, double age, int countFruit, bool treeSick)
            : base(height, type, name)
        {
            Age = age;
            CountFruit = countFruit;
            TreeSick = treeSick;
        }

        public int Harvesting(int amountToHarvest)
        {
            if (amountToHarvest <= 0)
                throw new ArgumentException("Количество для сбора должно быть положительным");

            if (TreeSick)
            {
                Console.WriteLine("Дерево болеет, урожай испорчен");
                return 0;
            }

            if (_countFruit < amountToHarvest)
                throw new InvalidOperationException("Недостаточно фруктов для сбора");

            _countFruit -= amountToHarvest;
            return amountToHarvest;
        }

        public void TakeCare()
        {
            var rand = new Random();
            if (TreeSick)
            {
                Console.WriteLine("Вы попытались вылечить дерево...");
                if (rand.Next(0, 2) == 0)
                {
                    TreeSick = false;
                    Console.WriteLine("Дерево выздоровело!");
                }
                else
                {
                    Console.WriteLine("Дерево ещё больное.");
                }
            }
            // Добавляем случайное количество фруктов (симулируем рост)
            _countFruit += rand.Next(5, 15);
            Console.WriteLine($"Теперь на дереве {_countFruit} фруктов.");
        }

        public void Touch()
        {
            var rand = new Random();
            Console.WriteLine("Вы трогаете дерево под вдохновляющую музыку...");

            int effects = rand.Next(0, 3); // случайный эффект
            switch (effects)
            {
                case 0:
                    _countFruit += 5;
                    Console.WriteLine("Дерево расцвело! Появились новые фрукты.");
                    break;
                case 1:
                    if (TreeSick == true)
                    {
                        TreeSick = false;
                        Console.WriteLine("Дерево исцелилось!");
                    }
                    break;
                case 2:
                    Console.WriteLine("Дерево шелестит листвой!");
                    break;
            }
        }

        public override string ToString()
        {
            string state = TreeSick ? "болеет" : "здорово";
            string height = Height.HeightToString();
            return $"{Name}: тип - {Type} возраст {Age} лет, рост  - {height}, {CountFruit} фруктов, состояние - {state}";
        }

        // Создаёт новое дерево как комбинацию двух: усредняет рост и возраст, суммирует фрукты.
        public static Tree operator +(Tree t1, Tree t2)
        {
            return new Tree(
                new PlantTypes.Height((t1.Height.Meters + t2.Height.Meters) / 2),
                t1.Age >= t2.Age ? t1.Type : t2.Type,
                t1.Name + " " + t2.Name,
                (t1.Age + t2.Age) / 2,
                t1.CountFruit + t2.CountFruit,
                t1.TreeSick || t2.TreeSick
            );
        }

        // В C# перегрузка оператора присваивания невозможна,
        // поэтому реализован метод CopyFrom(Tree t), выполняющий аналогичную функцию.
        public void CopyForm(Tree t)
        {
            _type = t.Type;
            _height = t.Height;
            _age = t.Age;
            _countFruit = t.CountFruit;
            _treeSick = t.TreeSick;
        }
    }
}
