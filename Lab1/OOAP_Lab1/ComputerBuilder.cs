using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOAP_Lab1
{
    public class ComputerBuilder
    {
        private string _buildName;
        private Dictionary<ComponentType, Component> _components;
        private List<string> _validationErrors;

        public ComputerBuilder()
        {
            _components = new Dictionary<ComponentType, Component>();
            _validationErrors = new List<string>();
            _buildName = "Новая сборка";
        }

        public ComputerBuilder AddCPU(Component cpu)
        {
            if(cpu.Type == ComponentType.CPU)
            {
                _components[ComponentType.CPU] = cpu;
            }
            return this;
        }

        public ComputerBuilder AddMotherboard(Component motherboard)
        {
            if (motherboard.Type == ComponentType.Motherboard)
            {
                _components[ComponentType.Motherboard] = motherboard;
            }
            return this;
        }

        public ComputerBuilder AddRAM(Component ram)
        {
            if (ram.Type == ComponentType.RAM)
            {
                _components[ComponentType.RAM] = ram;
            }
            return this;
        }

        public ComputerBuilder AddGPU(Component gpu)
        {
            if (gpu.Type == ComponentType.GPU)
            {
                _components[ComponentType.GPU] = gpu;
            }
            return this;
        }

        public ComputerBuilder AddHDD(Component hdd)
        {
            if (hdd.Type == ComponentType.HDD)
            {
                _components[ComponentType.HDD] = hdd;
            }
            return this;
        }

        public ComputerBuilder AddSSD(Component ssd)
        {
            if (ssd.Type == ComponentType.SSD)
            {
                _components[ComponentType.SSD] = ssd;
            }
            return this;
        }

        public ComputerBuilder AddPSU(Component psu)
        {
            if (psu.Type == ComponentType.PSU)
            {
                _components[ComponentType.PSU] = psu;
            }
            return this;
        }

        //Универсальный метод
        public ComputerBuilder AddComponent(Component component)
        {
            _components[component.Type] = component;
            return this;
        }

        public ComputerBuilder RemoveComponent(ComponentType type)
        {
            if (_components.ContainsKey(type))
                _components.Remove(type);
            return this;
        }

        private bool IsBuildValid()
        {
            _validationErrors.Clear();

            CheckRequiredComponent(ComponentType.CPU, "процессор");
            CheckRequiredComponent(ComponentType.Motherboard, "материнская плата");
            CheckRequiredComponent(ComponentType.RAM, "оперативная память");
            CheckRequiredComponent(ComponentType.PSU, "блок питания");

            // Проверка накопителей
            if (!HasValidComponent(ComponentType.SSD) && !HasValidComponent(ComponentType.HDD))
            {
                _validationErrors.Add("Отсутствует SSD или HDD");
            }

            return _validationErrors.Count == 0;
        }

        //Вспомогательные методы IsBuildValid
        private bool HasValidComponent(ComponentType type)
        {
            return _components.ContainsKey(type) && _components[type].Price > 0;
        }

        private void CheckRequiredComponent(ComponentType type, string name)
        {
            if (!HasValidComponent(type))
            {
                _validationErrors.Add($"Отсутствует {name}");
            }
        }

        public decimal GetCurrentPrice()
        {
            decimal total = 0;
            foreach (var component in _components.Values)
            {
                total += component.Price;
            }
            return total;
        }

        public Computer Build()
        {
            if(!IsBuildValid())
            {
                string errors = string.Join("\n", _validationErrors);
                throw new InvalidOperationException($"Невозможно собрать ПК:\n{errors}");
            }

            return new Computer(_buildName, _components);
        }
    }
}
