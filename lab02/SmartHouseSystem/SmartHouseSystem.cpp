#include "SmartHouseSystem.h"

//Вспомогательные методы для работы с UI
QString SmartHouseSystem::lightTempToString(const LightTemperature& temp)
{
    switch(temp) {
    case LightTemperature::WARM: return "WARM";
    case LightTemperature::NEUTRAL: return "NEUTRAL";
    case LightTemperature::COLD: return "COLD";
    case LightTemperature::DAYLIGHT: return "DAYLIGHT";
    case LightTemperature::DISCO: return "DISCO";
    default: return "UNKNOWN";
    }
}

QString SmartHouseSystem::modeToString(const Mode& mode)
{
    switch(mode) {
    case MORNING: return "MORNING";
    case DAY: return "DAY";
    case NIGHT: return "NIGHT";
    case PRIVACY: return "PRIVACY";
    default: return "UNKNOWN";
    }
}

QString SmartHouseSystem::securityModeToString(const SecurityMode& mode)
{
    switch(mode) {
    case SEC_DISABLED: return "DISABLED";
    case SEC_ROUTINE: return "ROUTINE";
    case SEC_ALARM: return "ALARM";
    default: return "UNKNOWN";
    }
}

QString SmartHouseSystem::valentineMoodToString(const ValentineMood& mood)
{
    switch(mood) {
    case VALENTINE_DISABLED: return "DISABLED";
    case VALENTINE_BABYSITTER: return "BABYSITTER";
    case VALENTINE_DUDE: return "DUDE";
    case VALENTINE_KILLER: return "KILLER";
    default: return "UNKNOWN";
    }
}

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

// Метод для обновления UI

void SmartHouseSystem::UpdateUI(Ui::MainWindow* ui)
{
    // Master Bedroom
    ui->progressBar->setValue(masterBedroomLight.getBrightness());
    ui->label_85->setText(lightTempToString(masterBedroomLight.getLightTemperature()));
    ui->checkBox_2->setChecked(masterBedroomLight.getStatus());

    ui->lcdNumber->display(masterBedroom_FH.getCurrentTemp());
    ui->checkBox->setChecked(masterBedroom_FH.getStatus());

    ui->label_84->setText(modeToString(masterBedroom_SC.getMode()));
    ui->progressBar_2->setValue(masterBedroom_SC.getPosition());

    // Hall
    ui->progressBar_3->setValue(hallLight.getBrightness());
    ui->label_90->setText(lightTempToString(hallLight.getLightTemperature()));
    ui->checkBox_3->setChecked(hallLight.getStatus());

    ui->lcdNumber_2->display(restFlat_FH.getCurrentTemp());
    ui->checkBox_4->setChecked(restFlat_FH.getStatus());

    ui->label_89->setText(modeToString(hall_SC.getMode()));
    ui->progressBar_4->setValue(hall_SC.getPosition());

    // Kitchen
    ui->progressBar_5->setValue(kitchenLight.getBrightness());
    ui->label_93->setText(lightTempToString(kitchenLight.getLightTemperature()));
    ui->checkBox_5->setChecked(kitchenLight.getStatus());

    ui->lcdNumber_3->display(restFlat_FH.getCurrentTemp());
    ui->checkBox_6->setChecked(restFlat_FH.getStatus());

    ui->label_92->setText(modeToString(kitchen_SC.getMode()));
    ui->progressBar_6->setValue(kitchen_SC.getPosition());

    // Bathroom
    ui->progressBar_7->setValue(bathroomLight.getBrightness());
    ui->label_95->setText(lightTempToString(bathroomLight.getLightTemperature()));
    ui->checkBox_7->setChecked(bathroomLight.getStatus());

    ui->lcdNumber_4->display(bathroom_FH.getCurrentTemp());
    ui->checkBox_8->setChecked(bathroom_FH.getStatus());

    // Hallway
    ui->progressBar_9->setValue(hallwayLight.getBrightness());
    ui->label_97->setText(lightTempToString(hallwayLight.getLightTemperature()));
    ui->checkBox_9->setChecked(hallwayLight.getStatus());

    ui->lcdNumber_5->display(restFlat_FH.getCurrentTemp());
    ui->checkBox_10->setChecked(restFlat_FH.getStatus());

    // Security
    ui->securityStatus_checkBox->setChecked(security.getStatus());
    ui->securityMode_label->setText(securityModeToString(security.getCurrentMode()));
    ui->ValentineMood_label->setText(valentineMoodToString(security.getValentineMood()));
}
