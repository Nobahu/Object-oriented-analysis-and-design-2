## Лабораторная работа 3. Паттерн Observer (Наблюдатель)

### Предметная область
Предметной областью данной лабораторной работы стал **прототип программы для оповещения гражданских и гос.служб о городских преступлениях**

**Проблема:** У граждан и гос.служащих существует потребность в информировании о происходящих преступлениях в своих городах. Однако требуется, чтобы **гражданские, дежурные в больнице и полицейские** получали **разную информацию** о произошедшем.

**Решение:** Прототип программы с пользовательским интерфейсом в формате формы для заполнения заявки (для публикатора) и чата с информацией (для гражданских и гос.служащих).

### Реализация

#### Без паттерна

Основной проблемой при создании прототипа является то, что **IncidentPublisher** должен быть один для всех принимающих информацию. Из этого следует, что он должен знать кому и какую именно информацию он отправляет. Это добавляет сложности при реализации кода.

\
**Версия кода без паттерна**
```cpp title:Badversion_1
class IncidentPublisher:
    def __init__(self):
        self.civilians = []  # Список гражданских
        self.police_deps = []  # Список полицейских
        self.ambulance_stations = []  # Список скорых
        self.incident_story = {}
        self.incident_id = 0
    
    def add_civilian(self, civilian):
        self.civilians.append(civilian)
    
    def add_police(self, police):
        self.police_deps.append(police)

    def add_ambulance(self, ambulance):
        self.ambulance_stations.append(ambulance)
          
    def _notify_all(self, incident, incident_id):
            for civilian in self.civilians:
                if incident.full_report.location == civilian.location:
                    civilian.receive_notification(incident, incident_id)
            
            for police in self.police_depts:
                if incident.full_report.location == police.location:
                    police.process_incident(incident, incident_id)
            
            for ambulance in self.ambulance_stations:
                if (incident.full_report.location == ambulance.location and 
                    incident.full_report.has_injured):
                    ambulance.handle_emergency(incident, incident_id)
```

```cpp title:BadVersion_2
class Civilian:
    def __init__(self, location, name)
    def receive_public_report(self, incident_id, report_text)

class Police:
    def __init__(self, department, location)
    def handle_full_report(self, incident_id, full_text, incident_type, severity)

class Ambulance:
    def __init__(self, station, location)
    def dispatch_ambulance(self, incident_id, location, severity)
    
```

\
Выше представлен код без применения паттернов. Из него можно видеть отчетливую проблему: **Хранение всех оповещающих в разных списках**. Из этого вытекает огромная сложность для поддержания программы, т.к при добавлении новых типов оповещаемых (пожарные, МЧС и т.п) потребуется вводить новые списки и добавлять соответствующие проверки с циклами. Рано или поздно это превратиться в громоздкий и неудобный для работы код.

#### Применение паттерна Observer
```cpp title:ObserverVersion_1
class IncidentPublisher:
    def __init__(self):
        self.observers: List[Observer] = []
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self.incident_id = 0
    
    def add_observer(self, observer: Observer) -> None:
        self.observers.append(observer)
          
    def remove_observer(self, observer: Observer) -> None
    
    def create_incident_report(self,
                              incident_type: IncidentType,
                              location: str,
                              time: str,
                              public_report: str,
                              full_report: str,
                              severity_level: int,
                              is_confirmed: bool,
                              has_injured: bool) -> None
    
    def _notify_all(self, incident: Incident, incident_id: int) -> None:
        if not self.observers:
            return
        for observer in self.observers:
            observer.update(incident_id, incident)
```

```cpp title:ObserverVersion_2
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
    
    def update(self, incident_id: int, incident: Incident) -> None

class PoliceObserver(Observer):
    def __init__(self, department: str, location: str, logger: Optional[Callable] = None):
        self._department = department
        self._location = location
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self._logger = logger or print
  
    def update(self, incident_id: int, incident: Incident) -> None

class AmbulanceObserver(Observer):
    def __init__(self, station: str, location: str, logger: Optional[Callable] = None):
        self._station = station
        self._location = location
        self.incident_story: Dict[int, IncidentFullReport] = {}
        self._logger = logger or print
    
    def update(self, incident_id: int, incident: Incident) -> None
```

\
В следствии паттерна можно заметить явное улучшение структуры кода и изменение взаимодействия классов друг с другом. Если ранее **IncidentPublisher** обязан был знать кому и какую информацию он отправляет, то теперь он знает лишь количество своих наблюдателей и всем отправляет полные отчеты. В то же время наблюдатели имеют свои наборы инцидентов, которые хранят только ту информацию, которая им отведена. Таким образом теперь с наблюдателя снимается нагрузка на проверки и мы, при помощи интерфейса (абстрактного класса), задаем общее поведение каждому классу, который от него наследуется.

### Вывод

В ходе выполнения лабораторной работы был изучен и применен паттерн проектирования Observer (Наблюдатель) на примере разработки **прототипа программы для оповещения гражданских и гос.служб о городских преступлениях**.

**Основные преимущества использования паттерна:**
- **Наблюдатели связаны общим интерфейсом** - они имеют общее поведение, что позволяет избежать дублирования кода;
- **Простота добавления наблюдателей** - не требуется изменять существующий код **IncidentPublisher**, это будет особенно заметно, если будет 3-5 публикаторов;
- **SRP** - каждый класс отвечает за свою логику.

**Минус использования паттерна:**
- **Отсутсвие порядка уведомления** - класс **IncidentPublisher** не различает наблюдателей, а значит может отправить данные скорой или гражданскому перед полицией. Это может быть серьезной проблемой в реальной жизни.