using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadversionOOAP_Lab1
{
    public class Computer
    {
        public string BuildName { get; }
        public DateTime BuildDate { get; }
        private Component cpu;
        private Component motherboard;
        private Component ram;
        private Component gpu;
        private Component hdd;
        private Component ssd;
        private Component psu;

        public Computer(string buildName)
        {
            BuildName = buildName;
            BuildDate = DateTime.Now;
        }

        public void AddComponent(Component component)
        {
            if (component.Type == ComponentType.CPU)
            {
                if (cpu == null)
                {
                    cpu = component;
                }
            }
            else if (component.Type == ComponentType.Motherboard)
            {
                if (motherboard == null)
                {
                    motherboard = component;
                }
            }
            else if (component.Type == ComponentType.RAM)
            {
                if (ram == null)
                {
                    ram = component;
                }
            }
            else if (component.Type == ComponentType.GPU)
            {
                if (gpu == null)
                {
                    gpu = component;
                }
            }
            else if (component.Type == ComponentType.HDD)
            {
                if (hdd == null)
                {
                    hdd = component;
                }
            }
            else if (component.Type == ComponentType.SSD)
            {
                if (ssd == null)
                {
                    ssd = component;
                }
            }
            else if (component.Type == ComponentType.PSU)
            {
                if (psu == null)
                {
                    psu = component;
                }
            }
        }

        public Component GetComponent(ComponentType componentType)
        {
            if (componentType == ComponentType.CPU)
            {
                return cpu;
            }
            else if (componentType == ComponentType.Motherboard)
            {
                return motherboard;
            }
            else if (componentType == ComponentType.RAM)
            {
                return ram;
            }
            else if (componentType == ComponentType.GPU)
            {
                return gpu;
            }
            else if (componentType == ComponentType.HDD)
            {
                return hdd;
            }
            else if (componentType == ComponentType.SSD)
            {
                return ssd;
            }
            else if (componentType == ComponentType.PSU)
            {
                return psu;
            }
            return null;
        }

        public bool HasComponent(ComponentType componentType)
        {
            if (componentType == ComponentType.CPU)
            {
                return cpu != null;
            }
            else if (componentType == ComponentType.Motherboard)
            {
                return motherboard != null;
            }
            else if (componentType == ComponentType.RAM)
            {
                return ram != null;
            }
            else if (componentType == ComponentType.GPU)
            {
                return gpu != null;
            }
            else if (componentType == ComponentType.HDD)
            {
                return hdd != null;
            }
            else if (componentType == ComponentType.SSD)
            {
                return ssd != null;
            }
            else if (componentType == ComponentType.PSU)
            {
                return psu != null;
            }
            return false;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                if (cpu != null)
                {
                    total += cpu.Price;
                }
                if (motherboard != null)
                {
                    total += motherboard.Price;
                }
                if (ram != null)
                {
                    total += ram.Price;
                }
                if (gpu != null)
                {
                    total += gpu.Price;
                }
                if (hdd != null)
                {
                    total += hdd.Price;
                }
                if (ssd != null)
                {
                    total += ssd.Price;
                }
                if (psu != null)
                {
                    total += psu.Price;
                }
                return total;
            }
        }
    }

    public class Component
    {
        public ComponentType Type { get; }
        public string Name { get; }
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

