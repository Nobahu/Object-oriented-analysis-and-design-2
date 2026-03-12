from enum import Enum

class IncidentType(Enum):
    THEFT = "кража"
    HIJACKING = "угон"
    ROBBERY = "разбой"
    FIGHT = "драка"
    MURDER = "убийство"
    DRUGS = "наркотики"
    STREET_ASSAULT = "уличное нападение"

class IncidentPublicReport:
    def __init__(self, location: str, time: str, report: str):
        self.location = location
        self.time = time
        self.report = report

class IncidentFullReport(IncidentPublicReport):
    def __init__(self, location: str, time: str, report: str,
                 full_report: str, severity_level: int,
                 is_confirmed: bool, has_injured: bool):
        super().__init__(location, time, report)
        self.full_report = full_report
        self.severity_level = severity_level
        self.is_confirmed = is_confirmed
        self.has_injured = has_injured

class Incident:
    def __init__(self, incident_type: IncidentType, full_report: IncidentFullReport):
        self.type = incident_type
        self.full_report = full_report