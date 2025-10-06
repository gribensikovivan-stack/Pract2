using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pract2;

namespace pract2
{
    internal class Plant : PlantTypes
    {
        protected Height _height;
        protected TreeType _type;
        public Height Height
        {
            get => _height;
            set
            {
                if (value.Meters < 0)
                    throw new ArgumentException("Рост должен быть положительным");
                _height = value;
            }
        }

        public TreeType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(PlantTypes.TreeType), value))
                    throw new ArgumentException("Неверный ввод типа растения");
                _type = value;
            }
        }

        private readonly string _name;
        public string Name => _name;

        protected Plant()
        {
            Height = new Height(0);
            Type = TreeType.None;
            _name = "Неизвестное дерево";
        }

        protected Plant(Height height, TreeType type, string name)
        {
            Height = height;
            Type = type;
            _name = name;
        }

        public void Deconstruct(out Height height, out TreeType type)
        {
            height = Height;
            type = Type;
        }
    }
}
