using System;
using System.Linq.Expressions;

namespace pract2
{
    public class PlantTypes
    {
        public enum TreeType
        {
            Oak, Pine, Birch, Maple, None
        }

        public struct Height
        {
            public double Meters { get; }

            public Height(double meters)
            {
                if (meters < 0)
                    throw new ArgumentException("Высота не должна быть отрицательной");
                Meters = meters;
            }

            public string HeightToString() => $"{Meters:F2} м";
        }
    }
}