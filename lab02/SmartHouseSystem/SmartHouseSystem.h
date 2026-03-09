#ifndef SMARTHOUSESYSTEM_H
#define SMARTHOUSESYSTEM_H

#include "FlatLight.h"
#include "FloorHeating.h"
#include "SmartCurtain.h"
#include "SecuritySystem.h"

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

    ~SmartHouseSystem() = default;

    // ГЕТТЕРЫ

    // Геттеры для света
    int getKitchenLightBrightness() { return kitchenLight.getBrightness(); }
    int getMasterBedroomLightBrightness() { return masterBedroomLight.getBrightness(); }
    int getBathroomLightBrightness() { return bathroomLight.getBrightness(); }
    int getHallwayLightBrightness() { return hallwayLight.getBrightness(); }
    int getHallLightBrightness() { return hallLight.getBrightness(); }

    bool isKitchenLightOn() { return kitchenLight.getStatus(); }
    bool isMasterBedroomLightOn() { return masterBedroomLight.getStatus(); }
    bool isBathroomLightOn() { return bathroomLight.getStatus(); }
    bool isHallwayLightOn() { return hallwayLight.getStatus(); }
    bool isHallLightOn() { return hallLight.getStatus(); }

    LightTemperature getKitchenLightTemp() { return kitchenLight.getLightTemperature(); }
    LightTemperature getMasterBedroomLightTemp() { return masterBedroomLight.getLightTemperature(); }
    LightTemperature getBathroomLightTemp() { return bathroomLight.getLightTemperature(); }
    LightTemperature getHallwayLightTemp() { return hallwayLight.getLightTemperature(); }
    LightTemperature getHallLightTemp() { return hallLight.getLightTemperature(); }

    // Геттеры для теплого пола
    int getMasterBedroomFloorTemp() { return masterBedroom_FH.getCurrentTemp(); }
    int getBathroomFloorTemp() { return bathroom_FH.getCurrentTemp(); }
    int getRestFlatFloorTemp() { return restFlat_FH.getCurrentTemp(); }

    bool isMasterBedroomFloorOn() { return masterBedroom_FH.getStatus(); }
    bool isBathroomFloorOn() { return bathroom_FH.getStatus(); }
    bool isRestFlatFloorOn() { return restFlat_FH.getStatus(); }

    // Геттеры для штор
    int getMasterBedroomCurtainPos() { return masterBedroom_SC.getPosition(); }
    int getHallCurtainPos() { return hall_SC.getPosition(); }
    int getKitchenCurtainPos() { return kitchen_SC.getPosition(); }

    Mode getMasterBedroomCurtainMode() { return masterBedroom_SC.getMode(); }
    Mode getHallCurtainMode() { return hall_SC.getMode(); }
    Mode getKitchenCurtainMode() { return kitchen_SC.getMode(); }

    // Информация о системе безопасности
    SecurityMode getCurrentMode() { return security.getCurrentMode(); }
    bool getSecurityStatus() { return security.getStatus(); }
    ValentineMood getValentineMood() { return security.getValentineMood(); }

private:

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
