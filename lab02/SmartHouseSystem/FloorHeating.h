#ifndef FLOORHEATING_H
#define FLOORHEATING_H

#include <string>

class FloorHeating
{
public:

    explicit FloorHeating(const std::string& loc)
        : location(loc)
        , isOn(false)
        , temperature(18)
    {}

    void On(const int& temp = 24)
    {
        isOn = true;
        temperature = temp;
    }

    void Off()
    {
        isOn = false;
    }

    int getCurrentTemp() const { return temperature; }
    bool getStatus() const { return isOn; }

private:
    std::string location;
    int temperature;
    bool isOn;
};

#endif // FLOORHEATING_H
