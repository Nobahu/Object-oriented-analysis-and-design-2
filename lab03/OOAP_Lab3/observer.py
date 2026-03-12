from abc import ABC, abstractmethod
from typing import Dict, Optional, Callable
from incident import Incident, IncidentPublicReport, IncidentFullReport

class Observer(ABC):
    @abstractmethod
    def update(self, incident_id: int, incident: Incident) -> None:
        pass

class CivillianObserver(Observer):
    def __init__(self, location: str, name: str, logger: Optional[Callable] = None):
        self._location = location
        self._name = name
        self.incident_story: Dict[int, IncidentPublicReport] = {}
        self._logger = logger or print
    
    def update(self, incident_id: int, incident: Incident) -> None:
        if incident.full_report.location != self._location or not(incident.full_report.is_confirmed):
            return
        
        msg = f"[Гражданский] {self._name} ({self._location}): получено уведомление"
        self._logger(msg)
        
        # Добавляем в GUI окно если есть
        if hasattr(self, '_gui_window'):
            self._gui_window.log(
                f"\n"
                f"   Отчет: {incident.full_report.report}\n"
                f"   Тип: {incident.type.value}\n"
                f"   Время: {incident.full_report.time}\n",
                severity=incident.full_report.severity_level,
                incident_id=incident_id
            )
        
        report = incident.full_report
        self.incident_story[incident_id] = report

class PoliceObserver(Observer):
    def __init__(self, department: str, location: str, logger: Optional[Callable] = None):
        self._department = department
        self._location = location
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self._logger = logger or print
    
    def update(self, incident_id: int, incident: Incident) -> None:
        if incident.full_report.location != self._location:
            return
        
        msg = f"[Полиция] {self._department} ({self._location}): получено уведомление"
        self._logger(msg)
        
        # Добавляем в GUI окно если есть
        if hasattr(self, '_gui_window'):
            self._gui_window.log(
                f"\n"
                f"   Отчет: {incident.full_report.full_report}\n"
                f"   Тип: {incident.type.value}\n"
                f"   Место: {incident.full_report.location}\n"
                f"   Время: {incident.full_report.time}\n"
                f"   Тяжесть: {incident.full_report.severity_level}/10\n"
                f"   Подтверждено: {'да' if incident.full_report.is_confirmed else 'нет'}\n"
                f"   Пострадавшие: {'есть' if incident.full_report.has_injured else 'нет'}",
                severity=incident.full_report.severity_level,
                incident_id=incident_id
            )
        
        self.incident_story[incident_id] = incident.full_report

class AmbulanceObserver(Observer):
    def __init__(self, station: str, location: str, logger: Optional[Callable] = None):
        self._station = station
        self._location = location
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self._logger = logger or print
    
    def update(self, incident_id: int, incident: Incident) -> None:
        if incident.full_report.location != self._location or not(incident.full_report.is_confirmed):
            return
        
        if incident.full_report.has_injured:
            msg = f"[Скорая] {self._station} ({self._location}): получено уведомление"
            self._logger(msg)
            
            # Добавляем в GUI окно если есть
            if hasattr(self, '_gui_window'):
                self._gui_window.log(
                    f"\n"
                    f"   Отчет: {incident.full_report.full_report}\n"
                    f"   Место: {incident.full_report.location}\n"
                    f"   Время: {incident.full_report.time}\n"
                    f"   Тяжесть: {incident.full_report.severity_level}/10",
                    severity=incident.full_report.severity_level,
                    incident_id=incident_id
                )
            
            self.incident_story[incident_id] = incident.full_report