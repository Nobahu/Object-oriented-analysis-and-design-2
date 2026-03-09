#ifndef FLATLIGHT_H
#define FLATLIGHT_H

#include <string>

namespace FlatLight {

enum class LightTemperature
{
    WARM,
    NEUTRAL,
    COLD,
    DAYLIGHT,
    DISCO

};

class Light
{
public:

    explicit Light(const std::string& loc)
        : location(loc)
        , brightness(0)
        , temperature(LightTemperature::NEUTRAL)
        , isOn(false)
    {
    }

    void On(const int& percents = 100, const LightTemperature& temp = LightTemperature::NEUTRAL)
    {
        isOn = true;
        brightness = percents;
        temperature = temp;
    }

    void Off()
    {
        isOn = false;
        brightness = 0;
    }

    void setSettings(const int& percents = 100, const LightTemperature& temp = LightTemperature::NEUTRAL)
    {
        if (percents >= 0 && percents <= 100) {
            this->brightness = percents;
        }
        this->temperature = temp;
    }

    int getBrightness() { return brightness; }
    LightTemperature getLightTemperature() { return temperature; }
    bool getStatus() { return isOn; }

private:

    std::string location;
    int brightness;
    LightTemperature temperature;
    bool isOn;
};

}

#endif // FLATLIGHT_H
