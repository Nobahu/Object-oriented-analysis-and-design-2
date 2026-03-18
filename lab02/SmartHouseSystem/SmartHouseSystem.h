#ifndef SMARTHOUSESYSTEM_H
#define SMARTHOUSESYSTEM_H

#include "FlatLight.h"
#include "FloorHeating.h"
#include "SmartCurtain.h"
#include "SecuritySystem.h"
#include "ui_mainwindow.h"

using namespace FlatLight;

class SmartHouseSystem
{
public:
    explicit SmartHouseSystem()
        : kitchenLight("Кухня")
        , masterBedroomLight("Спальня")
        , bathroomLight("Ванная")
        , hallwayLight("Коридор")
        , hallLight("Зал")
        , masterBedroom_FH("Спальня")
        , bathroom_FH("Ванная")
        , restFlat_FH("Остальные комнаты")
        , masterBedroom_SC("Спальня")
        , hall_SC("Зал")
        , kitchen_SC("Кухня")
        , security()
    {}

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

#endif // SMARTHOUSESYSTEM_H
