## Лабораторная работа 1. Паттерн Builder(строитель)

### Предметная область
Предметной областью данной лабораторной работы стало создание приложения для сборки ПК

**Проблема:** Для сборки ПК необходимо подбирать комплектующие и производить оценку конечной стоимости сборки в зависимости от самих комплектующих, которые были подобраны.

**Решение:** Программа с графическим пользовательским интерфейсом, где пользователь/сборщик может выбрать необходимые комплектующие будущего ПК, рассчитать стоимость и создать сборку ПК.

### Реализация

Очевидной проблемой, с которой можно столкнуться при разработке программы, является большое количество условий, которое нужно реализовать для добавления тех или иных комплектующих в конечную сборку.
\
**Версия добавления без паттерна**

```cpp title:Badversion
public class Computer
{
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
}
```

Та же самая проблема преследует нас и в случаях, когда необходимо посчитать стоимость или проверить компоненты. Бесконечные if-else конструкции.
\
Для текущей реализации данная проблема - не критично, однако если добавлять новые компоненты для сборки, то проблема становится очевидной. Поэтому здесь разумно использовать **паттерн Builder**.
\
**Версия добавления с паттерном**
```cpp title:Builderversion
 public class ComputerBuilder
 {
     public ComputerBuilder()
     {}

     public ComputerBuilder AddCPU(Component cpu)
     {}

     public ComputerBuilder AddMotherboard(Component motherboard)
     {}

     public ComputerBuilder AddRAM(Component ram)
     {}

     public ComputerBuilder AddGPU(Component gpu)
     {}

     public ComputerBuilder AddHDD(Component hdd)
     {}

     public ComputerBuilder AddSSD(Component ssd)
     {}

     public ComputerBuilder AddPSU(Component psu)
     {}

     //Универсальный метод
     public ComputerBuilder AddComponent(Component component)
     {}

     public ComputerBuilder RemoveComponent(ComponentType type)
     {}

     private bool IsBuildValid()
     {}

     //Вспомогательные методы IsBuildValid
     private bool HasValidComponent(ComponentType type)
     {}

     private void CheckRequiredComponent(ComponentType type, string name)
     {}

     public decimal GetCurrentPrice()
     {}

     public Computer Build()
     {}
 }
```

Данный класс позволяет собирать наш ПК по частям при помощи методов (таких как AddCPU и т.п), обходя проблему спагетти-кода. Вместо того, чтобы каждый раз проверять то, какой именно компонент был выбран, мы просто вызываем метод класса. После подбора комплектующих можно просто вызвать метод **Build()** и объект **Computer** будет собран.

### Вывод

На текущий момент затруднительно оценить то, насколько лучше и быстрее стал работать код программы, однако сомнений в этом нет, т.к мы обходим проблему бесконечных проверок. Также с уверенностью можно сказать, что с применением паттерна Builder код стал выглядеть чище.
