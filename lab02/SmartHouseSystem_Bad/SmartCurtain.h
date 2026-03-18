#ifndef SMARTCURTAIN_H
#define SMARTCURTAIN_H

#include <string>

enum Mode
{
    MORNING,
    DAY,
    NIGHT,
    PRIVACY
};

class SmartCurtain
{
public:

    explicit SmartCurtain(const std::string& loc)
        : location(loc)
        , curtainMode(DAY)
        , position(100)
    {}

    void setMode(const Mode& mode)
    {
        curtainMode = mode;

        switch (mode) {

        case MORNING:
            position = 50;
            break;
        case DAY:
            position = 100;
            break;
        case NIGHT:
            position = 0;
            break;
        case PRIVACY:
            position = 20;
            break;
        }
    }

    Mode getMode() { return curtainMode; }
    int getPosition() { return position; }

private:

    std::string location;
    Mode curtainMode;
    int position;

};

#endif // SMARTCURTAIN_H
