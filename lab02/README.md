## Лабораторная работа 2. Паттерн Facade(фасад)

### Предметная область
Предметной областью данной лабораторной работы стала **Система умного дома**

\
**Проблема:** У пользователя системы существует потребность в настройке **освещения, подогрева полов, штор и системы безопасности** под свои нужды. Он хочет заранее настроить определенные сценарии работы.

\
**Решение:** Программа с графическим пользовательским интерфейсом, где пользователь может выбирать заранее настроенные сценарии работы своего умного дома.

### Реализация

Основной проблемой реализации программы является большое количество настроек, из чего вытекает необходимость вызывать большое количество методов отдельно для того, чтобы произвести гибкую настройку сценария работы умного дома.


#### Версия добавления без паттерна

```cpp title:Badversion
// mainwindow.h
class MainWindow
{

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_Morning_pushButton_clicked();
    void on_Day_pushButton_clicked();
    void on_Night_pushButton_clicked();
    void on_DiscoParty_pushButton_clicked();
    void on_EmptyFlat_pushButton_clicked();

private:
    void updateAllWidgets();
    QString lightTempToString(const LightTemperature& temp);
    QString modeToString(const Mode& mode);
    QString securityModeToString(const SecurityMode& mode);
    QString valentineMoodToString(const ValentineMood& mood);

    Ui::MainWindow* ui;

    // Все объекты системы напрямую в окне
    Light kitchenLight;
    Light masterBedroomLight;
    Light bathroomLight;
    Light hallwayLight;
    Light hallLight;

    FloorHeating masterBedroom_FH;
    FloorHeating bathroom_FH;
    FloorHeating restFlat_FH;

    SmartCurtain masterBedroom_SC;
    SmartCurtain hall_SC;
    SmartCurtain kitchen_SC;

    SecuritySystem security;
};
```

\
Очевидным фактом является то, что у данной реализации есть множество недостатков:
- Божественный класс — по сути MainWindow отвечает сразу за все (сценарии, хранение объектов, отрисовка GUI);
- Окно самостоятельно вызывает каждый метод каждого устройства;
- Все данные привязаны к конкретному окну и их нельзя переиспользовать.

На помощь нам приходит **паттерн Facade**, который позволяет скрыть сложность реализации за простым интерфейсом, определить одну точку взаимодействия между клиентом и системой.

#### Версия добавления с паттерном

\
**Класс-фасад**
```cpp title:FacadeVarsion
class SmartHouseSystem
{
public:
    explicit SmartHouseSystem() {}

    void morningScenario();

    void dayScenario();

    void nightScenario();

    void emptyFlatScenario();

    void discoPartyScenario();

    void UpdateUI(Ui::MainWindow* ui);

    ~SmartHouseSystem() = default;


private:

    QString lightTempToString(const LightTemperature& temp);
    QString modeToString(const Mode& mode);
    QString securityModeToString(const SecurityMode& mode);
    QString valentineMoodToString(const ValentineMood& mood);

    Light kitchenLight;
    Light masterBedroomLight;
    Light bathroomLight;
    Light hallwayLight;
    Light hallLight;

    FloorHeating masterBedroom_FH;
    FloorHeating bathroom_FH;
    FloorHeating restFlat_FH;

    SmartCurtain masterBedroom_SC;
    SmartCurtain hall_SC;
    SmartCurtain kitchen_SC;

    SecuritySystem security;

};
```
\
**Класс-пользователь**
```cpp title:FacadeVarsion
class MainWindow
{
public:
    MainWindow(QWidget *parent = nullptr);

    ~MainWindow();

private slots:

    void on_Morning_pushButton_clicked();

    void on_Day_pushButton_clicked();

    void on_Night_pushButton_clicked();

    void on_DiscoParty_pushButton_clicked();

    void on_EmptyFlat_pushButton_clicked();

private:
    void updateAllWidgets();
    SmartHouseSystem* myHome;
};
```
Класс MainWindow в данном случае отвечает за графический интерфейс, с которым пользователь работает. При нажатии кнопки, активируется соответствующий сценарий, который обрабатывается **объектом класса SmartHouseSystem**, после чего все показатели в интерфейсе обновляются.

### Вывод

В ходе выполнения лабораторной работы был изучен и применен паттерн проектирования Facade (Фасад) на примере разработки **Системы умного дома**.

**Основные преимущества использования паттерна:**
- Инкапсуляция сложности — детали реализации скрыты от пользователя, он взаимодействует только с простым интерфейсом;
- Упрощение клиентского кода — вместо десятков вызовов методов разных классов, клиент (графический интерфейс) вызывает один метод сценария;
- Централизованное управление — вся логика сценариев собрана в одном месте.

**Минусы использования паттерна:**
- Для моего конкретного случая теряется возможность гибкой настройки системы, т.к приходится пользоваться заранее созданной логикой. Конечно, можно добавить функционал создания сценариев, однако кардинально проблему это не решает.