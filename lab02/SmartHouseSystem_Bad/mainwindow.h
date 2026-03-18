//Bad version without facade
#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "FlatLight.h"
#include "FloorHeating.h"
#include "SmartCurtain.h"
#include "SecuritySystem.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

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
    QString lightTempToString(const FlatLight::LightTemperature& temp);
    QString modeToString(const Mode& mode);
    QString securityModeToString(const SecurityMode& mode);
    QString valentineMoodToString(const ValentineMood& mood);

    FlatLight::Light kitchenLight;
    FlatLight::Light masterBedroomLight;
    FlatLight::Light bathroomLight;
    FlatLight::Light hallwayLight;
    FlatLight::Light hallLight;

    FloorHeating masterBedroom_FH;
    FloorHeating bathroom_FH;
    FloorHeating restFlat_FH;

    SmartCurtain masterBedroom_SC;
    SmartCurtain hall_SC;
    SmartCurtain kitchen_SC;

    SecuritySystem security;

    Ui::MainWindow* ui;
};

#endif // MAINWINDOW_H
