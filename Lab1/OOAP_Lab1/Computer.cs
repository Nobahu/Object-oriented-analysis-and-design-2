using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOAP_Lab1
{
    public class Computer
    {
        public string BuildName { get; }
        public DateTime BuildDate { get; }

        private Dictionary<ComponentType, Component> components;

        internal Computer(string buildName, Dictionary<ComponentType, Component> components)
        {
            BuildName = buildName;
            BuildDate = DateTime.Now;
            this.components = components ?? new Dictionary<ComponentType, Component>();
        }

        public Component GetComponent(ComponentType componentType)
        {
            return components.ContainsKey(componentType) ? components[componentType] : null;
        }

        public bool HasComponent(ComponentType componentType)
        {
            return components.ContainsKey(componentType);
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var component in components.Values)
                {
                    total += component.Price;
                }
                return total;
            }
        }

    }

    public class Component
    {
        public ComponentType Type { get; }
        public string Name { get;}
        public decimal Price { get; }

        public string Description { get; }

        public Component(ComponentType type, string name, decimal price, string description)
        {
            Type = type;
            Name = name;
            Price = price;
            Description = description;
        }


    }
}
