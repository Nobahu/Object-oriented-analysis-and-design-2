using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadversionOOAP_Lab1
{
    public enum ComponentType
    {
        CPU,
        Motherboard,
        RAM,
        GPU,
        HDD,
        SSD,
        PSU
    }

    public sealed class ComponentCatalog
    {
        private static ComponentCatalog _instance;
        private List<Component> _components;
        private static readonly object _lock = new object();
        private ComponentCatalog()
        {
            _components = new List<Component>();
            InitializeCatalog();
        }

        public static ComponentCatalog Instance
        {
            get
            {
                // Потокобезопасная ленивая инициализация
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ComponentCatalog();
                    }
                    return _instance;
                }
            }
        }

        private void InitializeCatalog()
        {
            // Процессоры Intel
            _components.Add(new Component(
                ComponentType.CPU,
                "Intel Core i3-13100",
                150,
                "4 ядра, 3.4GHz, LGA1700"));

            _components.Add(new Component(
                ComponentType.CPU,
                "Intel Core i5-13600K",
                300,
                "14 ядер, 3.5GHz, LGA1700"));

            _components.Add(new Component(
                ComponentType.CPU,
                "Intel Core i7-13700K",
                450,
                "16 ядер, 3.4GHz, LGA1700"));

            _components.Add(new Component(
                ComponentType.CPU,
                "Intel Core i9-13900K",
                650,
                "24 ядра, 3.0GHz, LGA1700"));

            // Процессоры AMD
            _components.Add(new Component(
                ComponentType.CPU,
                "AMD Ryzen 5 7600X",
                250,
                "6 ядер, 4.7GHz, AM5"));

            _components.Add(new Component(
                ComponentType.CPU,
                "AMD Ryzen 7 7800X3D",
                400,
                "8 ядер, 4.2GHz, AM5"));

            _components.Add(new Component(
                ComponentType.CPU,
                "AMD Ryzen 9 7950X",
                600,
                "16 ядер, 4.5GHz, AM5"));

            // Видеокарты
            _components.Add(new Component(
                ComponentType.GPU,
                "Встроенная графика",
                0,
                "В процессоре"));

            _components.Add(new Component(
                ComponentType.GPU,
                "NVIDIA RTX 4060",
                300,
                "8GB GDDR6"));

            _components.Add(new Component(
                ComponentType.GPU,
                "NVIDIA RTX 4070",
                600,
                "12GB GDDR6X"));

            _components.Add(new Component(
                ComponentType.GPU,
                "NVIDIA RTX 4080",
                1200,
                "16GB GDDR6X"));

            _components.Add(new Component(
                ComponentType.GPU,
                "AMD RX 7700 XT",
                450,
                "12GB GDDR6"));

            // Оперативная память
            _components.Add(new Component(
                ComponentType.RAM,
                "DDR4 8GB",
                40,
                "3200MHz"));

            _components.Add(new Component(
                ComponentType.RAM,
                "DDR4 16GB",
                70,
                "3200MHz"));

            _components.Add(new Component(
                ComponentType.RAM,
                "DDR5 32GB",
                150,
                "5600MHz"));

            _components.Add(new Component(
                ComponentType.RAM,
                "DDR5 64GB",
                280,
                "5600MHz"));

            // SSD
            _components.Add(new Component(
                ComponentType.SSD,
                "SSD 256GB",
                30,
                "SATA III"));

            _components.Add(new Component(
                ComponentType.SSD,
                "SSD 512GB",
                50,
                "NVMe M.2"));

            _components.Add(new Component(
                ComponentType.SSD,
                "SSD 1TB NVMe",
                80,
                "3500MB/s"));

            _components.Add(new Component(
                ComponentType.SSD,
                "SSD 2TB NVMe",
                150,
                "7000MB/s"));

            // HDD
            _components.Add(new Component(
                ComponentType.HDD,
                "HDD 1TB",
                40,
                "7200rpm"));

            _components.Add(new Component(
                ComponentType.HDD,
                "HDD 2TB",
                60,
                "7200rpm"));

            _components.Add(new Component(
                ComponentType.HDD,
                "HDD 4TB",
                100,
                "5400rpm"));

            // Материнские платы
            _components.Add(new Component(
                ComponentType.Motherboard,
                "ASUS B660M",
                120,
                "LGA1700, DDR4, mATX"));

            _components.Add(new Component(
                ComponentType.Motherboard,
                "MSI B760",
                150,
                "LGA1700, DDR5, ATX"));

            _components.Add(new Component(
                ComponentType.Motherboard,
                "Gigabyte Z790",
                250,
                "LGA1700, DDR5, PCIe 5.0, ATX"));

            _components.Add(new Component(
                ComponentType.Motherboard,
                "ASUS B650",
                180,
                "AM5, DDR5, ATX"));

            _components.Add(new Component(
                ComponentType.Motherboard,
                "MSI X670",
                320,
                "AM5, DDR5, PCIe 5.0, ATX"));

            // Блоки питания
            _components.Add(new Component(
                ComponentType.PSU,
                "БП 450W",
                50,
                "80+ Bronze"));

            _components.Add(new Component(
                ComponentType.PSU,
                "БП 650W Gold",
                90,
                "80+ Gold"));

            _components.Add(new Component(
                ComponentType.PSU,
                "БП 850W Gold",
                130,
                "80+ Gold, модульный"));

            _components.Add(new Component(
                ComponentType.PSU,
                "БП 1000W Platinum",
                200,
                "80+ Platinum, модульный"));
        }

        public List<Component> GetComponentsByType(ComponentType type)
        {
            return _components.Where(c => c.Type == type).ToList();
        }

        public bool RemoveComponent(Component component)
        {
            return _components.Remove(component);
        }

    }
}
