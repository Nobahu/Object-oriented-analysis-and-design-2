namespace OOAP_Lab1
{
    public partial class PCBuilder : Form
    {
        private ComputerBuilder _builder;
        private Label labelCurrPrice;
        public PCBuilder()
        {
            InitializeComponent();
            _builder = new ComputerBuilder();
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
                switch (component.Type)
                {
                    case ComponentType.CPU:
                        _builder.AddCPU(component);
                        break;
                    case ComponentType.GPU:
                        _builder.AddGPU(component);
                        break;
                    case ComponentType.Motherboard:
                        _builder.AddMotherboard(component);
                        break;
                    case ComponentType.RAM:
                        _builder.AddRAM(component);
                        break;
                    case ComponentType.HDD:
                        _builder.AddHDD(component);
                        break;
                    case ComponentType.SSD:
                        _builder.AddSSD(component);
                        break;
                    case ComponentType.PSU:
                        _builder.AddPSU(component);
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PriceCurrentButton_Click(object sender, EventArgs e)
        {
            decimal price = _builder.GetCurrentPrice();
            MessageBox.Show($"Текущая стоимость сборки: {price:C}",
                "Расчёт стоимости",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                Computer computer = _builder.Build();

                // Показываем результат
                string result = $"ПК успешно собран!\n\n" +
                               $"Название: {computer.BuildName}\n" +
                               $"Дата: {computer.BuildDate}\n" +
                               $"Комплектация:\n";

                foreach (ComponentType type in Enum.GetValues(typeof(ComponentType)))
                {
                    if (computer.HasComponent(type))
                    {
                        var comp = computer.GetComponent(type);
                        result += $"{comp.Name} - {comp.Price:C}\n";
                    }
                }

                result += $"\nИТОГО: {computer.TotalPrice:C}";

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
    }
}
