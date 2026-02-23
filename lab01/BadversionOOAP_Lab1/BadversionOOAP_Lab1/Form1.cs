using Microsoft.VisualBasic.Devices;
using System.ComponentModel;

namespace BadversionOOAP_Lab1
{
    public partial class PCBuilder : Form
    {
        private Computer _computer;
        private Label labelCurrPrice;

        public PCBuilder()
        {
            InitializeComponent();
            _computer = new Computer("Новая сборка");
            LoadComponents();
        }

        private void LoadComponents()
        {
            // CPU
            var cpuList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.CPU).ToList();
            cpuList.Insert(0, new Component(ComponentType.CPU, "Не выбрано", 0, ""));
            comboBoxCPU.DataSource = cpuList;
            comboBoxCPU.DisplayMember = "Name";

            // GPU
            var gpuList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.GPU).ToList();
            gpuList.Insert(0, new Component(ComponentType.GPU, "Не выбрано", 0, ""));
            comboBoxGPU.DataSource = gpuList;
            comboBoxGPU.DisplayMember = "Name";

            // Motherboard
            var mbList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.Motherboard).ToList();
            mbList.Insert(0, new Component(ComponentType.Motherboard, "Не выбрано", 0, ""));
            comboBoxMB.DataSource = mbList;
            comboBoxMB.DisplayMember = "Name";

            // RAM
            var ramList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.RAM).ToList();
            ramList.Insert(0, new Component(ComponentType.RAM, "Не выбрано", 0, ""));
            comboBoxRAM.DataSource = ramList;
            comboBoxRAM.DisplayMember = "Name";

            // HDD
            var hddList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.HDD).ToList();
            hddList.Insert(0, new Component(ComponentType.HDD, "Не выбрано", 0, ""));
            comboBoxHDD.DataSource = hddList;
            comboBoxHDD.DisplayMember = "Name";

            // SSD
            var ssdList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.SSD).ToList();
            ssdList.Insert(0, new Component(ComponentType.SSD, "Не выбрано", 0, ""));
            comboBoxSSD.DataSource = ssdList;
            comboBoxSSD.DisplayMember = "Name";

            // PSU
            var psuList = ComponentCatalog.Instance.GetComponentsByType(ComponentType.PSU).ToList();
            psuList.Insert(0, new Component(ComponentType.PSU, "Не выбрано", 0, ""));
            comboBoxPSU.DataSource = psuList;
            comboBoxPSU.DisplayMember = "Name";

            // Устанавливаем "Не выбрано" для всех
            comboBoxCPU.SelectedIndex = 0;
            comboBoxGPU.SelectedIndex = 0;
            comboBoxMB.SelectedIndex = 0;
            comboBoxRAM.SelectedIndex = 0;
            comboBoxHDD.SelectedIndex = 0;
            comboBoxSSD.SelectedIndex = 0;
            comboBoxPSU.SelectedIndex = 0;

            // Подписка на события
            comboBoxCPU.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxGPU.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxMB.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxRAM.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxHDD.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxSSD.SelectedIndexChanged += Component_SelectedIndexChanged;
            comboBoxPSU.SelectedIndexChanged += Component_SelectedIndexChanged;
        }

        private void Component_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem is Component component)
            {
                // Создаем новый Computer при каждом изменении? 
                // Или очищаем старые компоненты?
                // Без билдера логика становится запутанной

                // Вариант 1: Пересоздаем Computer
                _computer = new Computer("Новая сборка");

                // И добавляем все выбранные компоненты
                AddSelectedComponents();
            }
        }

        private void AddSelectedComponents()
        {
            if (comboBoxCPU.SelectedItem is Component cpu && cpu.Price > 0)
                _computer.AddComponent(cpu);
            if (comboBoxGPU.SelectedItem is Component gpu && gpu.Price > 0)
                _computer.AddComponent(gpu);
            if (comboBoxMB.SelectedItem is Component mb && mb.Price > 0)
                _computer.AddComponent(mb);
            if (comboBoxRAM.SelectedItem is Component ram && ram.Price > 0)
                _computer.AddComponent(ram);
            if (comboBoxHDD.SelectedItem is Component hdd && hdd.Price > 0)
                _computer.AddComponent(hdd);
            if (comboBoxSSD.SelectedItem is Component ssd && ssd.Price > 0)
                _computer.AddComponent(ssd);
            if (comboBoxPSU.SelectedItem is Component psu && psu.Price > 0)
                _computer.AddComponent(psu);
        }

        private void PriceCurrentButton_Click(object sender, EventArgs e)
        {
            // Пересчитываем с текущими выбранными компонентами
            AddSelectedComponents();

            decimal price = _computer.TotalPrice;
            MessageBox.Show($"Текущая стоимость сборки: {price:C}",
                "Расчёт стоимости",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Обновляем компоненты перед сборкой
                AddSelectedComponents();

                // Проверяем наличие обязательных компонентов вручную
                List<string> errors = new List<string>();

                if (!_computer.HasComponent(ComponentType.CPU))
                    errors.Add("Отсутствует процессор");
                if (!_computer.HasComponent(ComponentType.Motherboard))
                    errors.Add("Отсутствует материнская плата");
                if (!_computer.HasComponent(ComponentType.RAM))
                    errors.Add("Отсутствует оперативная память");
                if (!_computer.HasComponent(ComponentType.PSU))
                    errors.Add("Отсутствует блок питания");
                if (!_computer.HasComponent(ComponentType.SSD) && !_computer.HasComponent(ComponentType.HDD))
                    errors.Add("Отсутствует SSD или HDD");

                if (errors.Count > 0)
                {
                    throw new InvalidOperationException(string.Join("\n", errors));
                }

                // Показываем результат
                string result = $"ПК успешно собран!\n\n" +
                               $"Название: {_computer.BuildName}\n" +
                               $"Дата: {_computer.BuildDate}\n" +
                               $"Комплектация:\n";

                foreach (ComponentType type in Enum.GetValues(typeof(ComponentType)))
                {
                    if (_computer.HasComponent(type))
                    {
                        var comp = _computer.GetComponent(type);
                        result += $"{comp.Name} - {comp.Price:C}\n";
                    }
                }

                result += $"\nИТОГО: {_computer.TotalPrice:C}";

                MessageBox.Show(result, "Готово!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка сборки:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Программа: PCBuilder\n\n" +
                "Данный прототип программы позволяет создавать конфигурацию ПК\n\n" +
                "Доступные компоненты:\n" +
                "• Процессор (CPU) - обязательный\n" +
                "• Материнская плата (Motherboard) - обязательный\n" +
                "• Оперативная память (RAM) - обязательный\n" +
                "• Видеокарта (GPU) - необязательный\n" +
                "• Жёсткий диск (HDD) - необязательный\n" +
                "• Твердотельный накопитель (SSD) - необязательный\n" +
                "(важно понимать, что обязательно должен быть HDD или SSD)\n" +
                "• Блок питания (PSU) - обязательный\n\n" +
                "Как пользоваться:\n" +
                "1. Выберите компоненты из каталога\n" +
                "2. Программа проверит совместимость\n";

            string caption = "О программе";

            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
