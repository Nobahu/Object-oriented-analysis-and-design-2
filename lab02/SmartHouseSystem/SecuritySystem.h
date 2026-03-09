#ifndef SECURITYSYSTEM_H
#define SECURITYSYSTEM_H

enum SecurityMode
{
    SEC_DISABLED,
    SEC_ROUTINE,
    SEC_ALARM
};

enum ValentineMood
{
    VALENTINE_DISABLED,
    VALENTINE_BABYSITTER,
    VALENTINE_DUDE,
    VALENTINE_KILLER
};

class RoboKiller
{
public:
    explicit RoboKiller()
    {
        mood = VALENTINE_DISABLED;
    }

    void setMood(const ValentineMood& newMood)
    {
        mood = newMood;
    }

    ValentineMood getValentineMood()
    {
        return mood;
    }

private:
    ValentineMood mood;
};

class SecuritySystem
{
public:

    explicit SecuritySystem()
        : currentMode(SecurityMode::SEC_ROUTINE)
        , isOn(true)
        , Valentine()
    { }

    void setSecurityMode(const SecurityMode& mode)
    {
        currentMode = mode;
        switch (currentMode) {
        case SEC_DISABLED:
            isOn = false;
            setValentineMood(VALENTINE_DISABLED);
            break;
        case SEC_ROUTINE:
            isOn = true;
            setValentineMood(VALENTINE_BABYSITTER);
            break;
        case SEC_ALARM:
            isOn = true;
            setValentineMood(VALENTINE_KILLER);
            break;
        }

    }

    void setValentineMood(const ValentineMood& mood)
    {
        Valentine.setMood(mood);
    }

    SecurityMode getCurrentMode() { return currentMode; }
    bool getStatus() { return isOn; }
    ValentineMood getValentineMood() { return Valentine.getValentineMood(); }

private:

    SecurityMode currentMode;
    RoboKiller Valentine;
    bool isOn;

};

#endif // SECURITYSYSTEM_H
