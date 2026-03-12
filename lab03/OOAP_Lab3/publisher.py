from typing import Dict, List
from incident import Incident, IncidentFullReport, IncidentType
from observer import Observer

class IncidentPublisher:
    def __init__(self):
        self.observers: List[Observer] = []
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self.incident_id = 0
    
    def add_observer(self, observer: Observer) -> None:
        self.observers.append(observer)
    
    def remove_observer(self, observer: Observer) -> None:
        if observer in self.observers:
            self.observers.remove(observer)
    
    def create_incident_report(self,
                              incident_type: IncidentType,
                              location: str,
                              time: str,
                              public_report: str,
                              full_report: str,
                              severity_level: int,
                              is_confirmed: bool,
                              has_injured: bool) -> None:
        full = IncidentFullReport(
            location, time, public_report, full_report,
            severity_level, is_confirmed, has_injured
        )
        incident = Incident(incident_type, full)
        self.incident_story[self.incident_id] = full
        self._notify_all(incident, self.incident_id)
        self.incident_id += 1
    
    def _notify_all(self, incident: Incident, incident_id: int) -> None:
        if not self.observers:
            return
        for observer in self.observers:
            observer.update(incident_id, incident)