using System;
using System.Collections.Generic;
using System.Linq;
using pract2;

namespace pract2
{
    internal class Garden : IGarden
    {
        private List<Plant> _plants = new List<Plant>();

        public void AddPlant(Plant plant)
        {
            if (plant == null)
            {
                Console.WriteLine("Попытка добавить null в сад!");
                return;
            }

            _plants.Add(plant);
            Console.WriteLine("Добавлен: " + plant.ToString());
        }

        public void RemovePlant(Plant plant)
        {
            _plants.Remove(plant);
        }

        public void ShowPlants()
        {
            Console.WriteLine($"\nСад содержит {_plants.Count} растений:");
            for (int i = 0; i < _plants.Count; i++)
            {
                Console.WriteLine($"{i} - {_plants[i]}");
            }
        }

        public void CutDownTree(Tree tree)
        {
            RemovePlant(tree);
            Console.WriteLine("Дерево срезано.");
        }

        // Метод для получения всех растений определённого типа
        public List<T> GetPlantsOfType<T>() where T : Plant
        {
            List<T> result = new List<T>();

            foreach (var plant in _plants)
            {
                if (plant is T tPlant)
                    result.Add(tPlant);
            }

            return result;
        }
    }
}