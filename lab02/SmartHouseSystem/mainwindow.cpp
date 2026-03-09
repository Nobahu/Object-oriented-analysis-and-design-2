#include "mainwindow.h"
#include "ui_mainwindow.h"

QString lightTempToString(LightTemperature temp) {
    switch(temp) {
    case LightTemperature::WARM: return "WARM";
    case LightTemperature::NEUTRAL: return "NEUTRAL";
    case LightTemperature::COLD: return "COLD";
    case LightTemperature::DAYLIGHT: return "DAYLIGHT";
    case LightTemperature::DISCO: return "DISCO";
    }
}

QString modeToString(Mode mode) {
    switch(mode) {
    case MORNING: return "MORNING";
    case DAY: return "DAY";
    case NIGHT: return "NIGHT";
    case PRIVACY: return "PRIVACY";
    }
}

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
    , myHome(new SmartHouseSystem())
{
    ui->setupUi(this);

    ui->roomList->addItem("Master Bedroom");
    ui->roomList->addItem("Hall");
    ui->roomList->addItem("Kitchen");
    ui->roomList->addItem("Bathroom");
    ui->roomList->addItem("Hallway");

    ui->checkBox->setEnabled(false);
    ui->checkBox_2->setEnabled(false);
    ui->securityStatus_checkBox->setEnabled(false);

    connect(ui->roomList, &QListWidget::currentRowChanged,
            ui->stackedWidget, &QStackedWidget::setCurrentIndex);

    connect(ui->Morning_pushButton, &QPushButton::clicked, this, &MainWindow::on_Morning_pushButton_clicked);
    connect(ui->Day_pushButton, &QPushButton::clicked, this, &MainWindow::on_Day_pushButton_clicked);
    connect(ui->Night_pushButton, &QPushButton::clicked, this, &MainWindow::on_Night_pushButton_clicked);
    connect(ui->DiscoParty_pushButton, &QPushButton::clicked, this, &MainWindow::on_DiscoParty_pushButton_clicked);
    connect(ui->EmptyFlat_pushButton, &QPushButton::clicked, this, &MainWindow::on_EmptyFlat_pushButton_clicked);

    // Устанавливаем начальную страницу (первую)
    ui->roomList->setCurrentRow(0);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::updateMasterBedroomWidgets()
{
    // Light
    ui->progressBar->setValue(myHome->getMasterBedroomLightBrightness());
    ui->label_85->setText(lightTempToString(myHome->getMasterBedroomLightTemp()));
    ui->checkBox_2->setChecked(myHome->isMasterBedroomLightOn());

    // Floor Heating
    ui->lcdNumber->display(myHome->getMasterBedroomFloorTemp());
    ui->checkBox->setChecked(myHome->isMasterBedroomFloorOn());

    // Curtain
    ui->label_84->setText(modeToString(myHome->getMasterBedroomCurtainMode()));
    ui->progressBar_2->setValue(myHome->getMasterBedroomCurtainPos());
}

void MainWindow::updateHallWidgets()
{
    // Light
    ui->progressBar_3->setValue(myHome->getHallLightBrightness());
    ui->label_90->setText(lightTempToString(myHome->getHallLightTemp()));
    ui->checkBox_3->setChecked(myHome->isHallLightOn());

    // Floor Heating
    ui->lcdNumber_2->display(myHome->getRestFlatFloorTemp());
    ui->checkBox_4->setChecked(myHome->isRestFlatFloorOn());

    // Curtain
    ui->label_89->setText(modeToString(myHome->getHallCurtainMode()));
    ui->progressBar_4->setValue(myHome->getHallCurtainPos());
}
void MainWindow::updateKitchenWidgets()
{
    // Light
    ui->progressBar_5->setValue(myHome->getKitchenLightBrightness());
    ui->label_93->setText(lightTempToString(myHome->getKitchenLightTemp()));
    ui->checkBox_5->setChecked(myHome->isKitchenLightOn());

    // Floor Heating (если есть на кухне)
    ui->lcdNumber_3->display(myHome->getRestFlatFloorTemp());
    ui->checkBox_6->setChecked(myHome->isRestFlatFloorOn());

    // Curtain
    ui->label_92->setText(modeToString(myHome->getKitchenCurtainMode()));
    ui->progressBar_6->setValue(myHome->getKitchenCurtainPos());
}

void MainWindow::updateBathroomWidgets()
{
    // Light
    ui->progressBar_7->setValue(myHome->getBathroomLightBrightness());
    ui->label_95->setText(lightTempToString(myHome->getBathroomLightTemp()));
    ui->checkBox_7->setChecked(myHome->isBathroomLightOn());

    // Floor Heating
    ui->lcdNumber_4->display(myHome->getBathroomFloorTemp());
    ui->checkBox_8->setChecked(myHome->isBathroomFloorOn());

}

void MainWindow::updateHallwayWidgets()
{
    // Light
    ui->progressBar_9->setValue(myHome->getHallwayLightBrightness());
    ui->label_97->setText(lightTempToString(myHome->getHallwayLightTemp()));
    ui->checkBox_9->setChecked(myHome->isHallwayLightOn());

    // Floor Heating
    ui->lcdNumber_5->display(myHome->getRestFlatFloorTemp());
    ui->checkBox_10->setChecked(myHome->isRestFlatFloorOn());
}

void MainWindow::updateSecurityWidgets()
{
    bool isEnabled = myHome->getSecurityStatus();
    ui->securityStatus_checkBox->setChecked(isEnabled);

    SecurityMode secMode = myHome->getCurrentMode();
    switch(secMode) {
    case SecurityMode::SEC_DISABLED:
        ui->securityMode_label->setText("SEC_DISABLED");
        break;
    case SecurityMode::SEC_ROUTINE:
        ui->securityMode_label->setText("SEC_ROUTINE");
        break;
    case SecurityMode::SEC_ALARM:
        ui->securityMode_label->setText("SEC_ALARM");
        break;
    }

    ValentineMood valMood = myHome->getValentineMood();
    switch(valMood) {
    case ValentineMood::VALENTINE_DISABLED:
        ui->ValentineMood_label->setText("VALENTINE_DISABLED");
        break;
    case ValentineMood::VALENTINE_BABYSITTER:
        ui->ValentineMood_label->setText("VALENTINE_BABYSITTER");
        break;
    case ValentineMood::VALENTINE_DUDE:
        ui->ValentineMood_label->setText("VALENTINE_DUDE");
        break;
    case ValentineMood::VALENTINE_KILLER:
        ui->ValentineMood_label->setText("VALENTINE_KILLER");
        break;
    }
}

void MainWindow::updateAllWidgets()
{
    updateMasterBedroomWidgets();
    updateHallWidgets();
    updateKitchenWidgets();
    updateBathroomWidgets();
    updateHallwayWidgets();
    updateSecurityWidgets();
}


void MainWindow::on_Morning_pushButton_clicked()
{
    myHome->morningScenario();
    updateAllWidgets();
}


void MainWindow::on_Day_pushButton_clicked()
{
    myHome->dayScenario();
    updateAllWidgets();
}


void MainWindow::on_Night_pushButton_clicked()
{
    myHome->nightScenario();
    updateAllWidgets();
}


void MainWindow::on_DiscoParty_pushButton_clicked()
{
    myHome->discoPartyScenario();
    updateAllWidgets();
}


void MainWindow::on_EmptyFlat_pushButton_clicked()
{
    myHome->emptyFlatScenario();
    updateAllWidgets();
}

