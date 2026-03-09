#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "SmartHouseSystem.h"

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);

    void updateAllWidgets();

    ~MainWindow();

private slots:

    void on_Morning_pushButton_clicked();

    void on_Day_pushButton_clicked();

    void on_Night_pushButton_clicked();

    void on_DiscoParty_pushButton_clicked();

    void on_EmptyFlat_pushButton_clicked();

private:

    void updateMasterBedroomWidgets();
    void updateHallWidgets();
    void updateKitchenWidgets();
    void updateBathroomWidgets();
    void updateHallwayWidgets();
    void updateSecurityWidgets();

    Ui::MainWindow* ui;
    SmartHouseSystem* myHome;
};
#endif // MAINWINDOW_H
