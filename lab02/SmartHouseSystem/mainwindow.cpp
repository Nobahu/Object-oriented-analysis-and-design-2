#include "mainwindow.h"
#include "ui_mainwindow.h"

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

void MainWindow::updateAllWidgets()
{
    myHome->UpdateUI(ui);
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

