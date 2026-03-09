#include "SmartHouseSystem.h"

void SmartHouseSystem::morningScenario()
{
    //Свет
    kitchenLight.On(80, LightTemperature::NEUTRAL);
    masterBedroomLight.On(60, LightTemperature::WARM);
    bathroomLight.On(80, LightTemperature::NEUTRAL);
    hallwayLight.On(60, LightTemperature::WARM);
    hallLight.On(60, LightTemperature::WARM);

    //Подогрев пола
    masterBedroom_FH.On(25);
    bathroom_FH.On(30);
    restFlat_FH.On(25);

    //Шторы
    masterBedroom_SC.setMode(MORNING);
    hall_SC.setMode(MORNING);
    kitchen_SC.setMode(MORNING);

    security.setSecurityMode(SecurityMode::SEC_ROUTINE);
}

void SmartHouseSystem::dayScenario()
{
    kitchenLight.On(100, LightTemperature::DAYLIGHT);
    masterBedroomLight.On(100, LightTemperature::DAYLIGHT);
    bathroomLight.On(100, LightTemperature::NEUTRAL);
    hallwayLight.On(100, LightTemperature::DAYLIGHT);
    hallLight.On(100, LightTemperature::DAYLIGHT);

    masterBedroom_FH.On(22);
    bathroom_FH.On(27);
    restFlat_FH.On(22);

    masterBedroom_SC.setMode(DAY);
    hall_SC.setMode(DAY);
    kitchen_SC.setMode(DAY);

    security.setSecurityMode(SecurityMode::SEC_ROUTINE);
}

void SmartHouseSystem::nightScenario()
{
    kitchenLight.Off();
    masterBedroomLight.Off();
    bathroomLight.On(20, LightTemperature::NEUTRAL);
    hallwayLight.On(20, LightTemperature::NEUTRAL);
    hallLight.Off();

    masterBedroom_FH.On(22);
    bathroom_FH.On(27);
    restFlat_FH.On(22);

    masterBedroom_SC.setMode(NIGHT);
    hall_SC.setMode(NIGHT);
    kitchen_SC.setMode(PRIVACY);

    security.setSecurityMode(SecurityMode::SEC_ROUTINE);
}

void SmartHouseSystem::emptyFlatScenario()
{
    kitchenLight.Off();
    masterBedroomLight.Off();
    bathroomLight.Off();
    hallwayLight.Off();
    hallLight.Off();

    masterBedroom_FH.Off();
    bathroom_FH.On(27);
    restFlat_FH.Off();

    masterBedroom_SC.setMode(PRIVACY);
    hall_SC.setMode(PRIVACY);
    kitchen_SC.setMode(PRIVACY);

    security.setSecurityMode(SecurityMode::SEC_ALARM);
}

void SmartHouseSystem::discoPartyScenario()
{
    kitchenLight.On(60, LightTemperature::COLD);
    masterBedroomLight.On(30, LightTemperature::COLD);
    bathroomLight.On(100, LightTemperature::NEUTRAL);
    hallwayLight.On(60, LightTemperature::DISCO);
    hallLight.On(100, LightTemperature::DISCO);

    masterBedroom_FH.On(22);
    bathroom_FH.On(27);
    restFlat_FH.On(22);

    masterBedroom_SC.setMode(DAY);
    hall_SC.setMode(PRIVACY);
    kitchen_SC.setMode(DAY);

    security.setSecurityMode(SecurityMode::SEC_ROUTINE);
    security.setValentineMood(ValentineMood::VALENTINE_DUDE);
}
