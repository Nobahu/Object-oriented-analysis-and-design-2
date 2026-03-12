import tkinter as tk
from tkinter import ttk, scrolledtext, messagebox

from incident import IncidentType
from observer import CivillianObserver, PoliceObserver, AmbulanceObserver
from publisher import IncidentPublisher

class IncidentGUI:
    def __init__(self, root, civ_window, police_window, ambulance_window):
        self.root = root
        self.civ_window = civ_window
        self.police_window = police_window
        self.ambulance_window = ambulance_window
        self.root.title("Система Учета Инцидентов")
        self.root.geometry("1100x800")

        self.publisher = IncidentPublisher()
        self.civilians = []
        self.police_officers = []
        self.ambulances = []
        
        # Переменные формы инцидента
        self.var_type = tk.StringVar()
        self.var_location = tk.StringVar()
        self.var_time = tk.StringVar()
        self.var_public_report = tk.StringVar()
        self.var_severity = tk.IntVar(value=1)
        self.var_confirmed = tk.BooleanVar()
        self.var_injured = tk.BooleanVar()

        # Переменные для формы добавления наблюдателей - УПРОЩАЕМ до одной!
        self.var_new_observer_type = tk.StringVar(value="civilian")
        self.var_new_name = tk.StringVar()  # Единая переменная для имени/отдела/станции
        self.var_new_obs_location = tk.StringVar()

        self._create_widgets()
        self._setup_observers()

    def _setup_observers(self):
        self._add_civilian("Tomsk", "Alexey")
        self._add_police("Tomsk", "Tomsk1Department")
        self._add_ambulance("Tomsk", "GKB2Tomsk")

    def _on_add_observer_click(self):
        o_type = self.var_new_observer_type.get()
        loc = self.var_new_obs_location.get()
        name_value = self.var_new_name.get()
        
        if not loc:
            messagebox.showwarning("Внимание", "Укажите локацию!")
            return

        if not name_value:
            if o_type == "civilian":
                messagebox.showwarning("Внимание", "Укажите имя!")
            elif o_type == "police":
                messagebox.showwarning("Внимание", "Укажите отдел!")
            else:
                messagebox.showwarning("Внимание", "Укажите станцию!")
            return

        if o_type == "civilian":
            self._add_civilian(loc, name_value)
        elif o_type == "police":
            self._add_police(loc, name_value)
        else:  # ambulance
            self._add_ambulance(loc, name_value)
        
        # Очищаем поля
        self.var_new_name.set("")
        self.var_new_obs_location.set("")

    def _add_civilian(self, location: str, name: str):
        from observer_windows import CivillianWindow
        window = CivillianWindow(name, location)  # Создаем новое окно
        
        obs = CivillianObserver(location, name, logger=self._log)
        obs._gui_window = window  # Привязываем именно ЭТО окно
        self.publisher.add_observer(obs)
        self.civilians.append(obs)
        window.show()  # Показываем окно
        self._log(f"Добавлен гражданский: {name} ({location})")
        self._update_observer_list()
        self._update_stats()

    def _add_police(self, location: str, department: str):
        from observer_windows import PoliceWindow
        window = PoliceWindow(department, location)  # Создаем новое окно
        
        obs = PoliceObserver(department, location, logger=self._log)
        obs._gui_window = window  # Привязываем именно ЭТО окно
        self.publisher.add_observer(obs)
        self.police_officers.append(obs)
        window.show()  # Показываем окно
        self._log(f"Добавлен полицейский: {department} ({location})")
        self._update_observer_list()
        self._update_stats()

    def _add_ambulance(self, location: str, station: str):
        from observer_windows import AmbulanceWindow
        window = AmbulanceWindow(station, location)  # Создаем новое окно
        
        obs = AmbulanceObserver(station, location, logger=self._log)
        obs._gui_window = window  # Привязываем именно ЭТО окно
        self.publisher.add_observer(obs)
        self.ambulances.append(obs)
        window.show()  # Показываем окно
        self._log(f"Добавлена скорая: {station} ({location})")
        self._update_observer_list()
        self._update_stats()

    def _remove_civilian(self, index: int):
        """Удалить гражданского по индексу"""
        if 0 <= index < len(self.civilians):
            obs = self.civilians[index]
            if hasattr(obs, '_gui_window') and obs._gui_window:
                obs._gui_window.window.destroy()
            self.publisher.remove_observer(obs)
            self.civilians.pop(index)
            self._log("Гражданский удален")
            self._update_observer_list()
            self._update_stats()

    def _remove_police(self, index: int):
        """Удалить полицейского по индексу"""
        if 0 <= index < len(self.police_officers):
            obs = self.police_officers[index]
            if hasattr(obs, '_gui_window') and obs._gui_window:
                obs._gui_window.window.destroy()
            self.publisher.remove_observer(obs)
            self.police_officers.pop(index)
            self._log("Полицейский удален")
            self._update_observer_list()
            self._update_stats()

    def _remove_ambulance(self, index: int):
        """Удалить скорую по индексу"""
        if 0 <= index < len(self.ambulances):
            obs = self.ambulances[index]
            if hasattr(obs, '_gui_window') and obs._gui_window:
                obs._gui_window.window.destroy()
            self.publisher.remove_observer(obs)
            self.ambulances.pop(index)
            self._log("Скорая удалена")
            self._update_observer_list()
            self._update_stats()

    def _on_remove_observer_click(self):
        """Обработчик удаления наблюдателя через диалог"""
        dialog = tk.Toplevel(self.root)
        dialog.title("Удалить наблюдателя")
        dialog.geometry("400x300")
        dialog.transient(self.root)
        dialog.grab_set()
        
        ttk.Label(dialog, text="Выберите тип и индекс для удаления", 
                font=("Arial", 10)).pack(pady=10)
        
        frame = ttk.Frame(dialog, padding="10")
        frame.pack(fill=tk.BOTH, expand=True)
        
        # Тип наблюдателя
        ttk.Label(frame, text="Тип:").grid(row=0, column=0, sticky=tk.W, pady=5)
        type_var = tk.StringVar(value="civilian")
        ttk.Radiobutton(frame, text="Гражданский", variable=type_var, 
                    value="civilian").grid(row=0, column=1, sticky=tk.W)
        ttk.Radiobutton(frame, text="Полиция", variable=type_var, 
                    value="police").grid(row=0, column=2, sticky=tk.W)
        ttk.Radiobutton(frame, text="Скорая", variable=type_var, 
                    value="ambulance").grid(row=0, column=3, sticky=tk.W)
        
        # Индекс
        ttk.Label(frame, text="Индекс:").grid(row=1, column=0, sticky=tk.W, pady=5)
        index_var = tk.IntVar(value=0)
        index_spin = ttk.Spinbox(frame, from_=0, to=10, textvariable=index_var, width=5)
        index_spin.grid(row=1, column=1, sticky=tk.W, pady=5)
        
        # Информация о текущих наблюдателях
        info_text = tk.Text(frame, height=8, width=40, state='normal')
        info_text.grid(row=2, column=0, columnspan=4, pady=10)
        
        # Заполняем информацию
        info = "=== ГРАЖДАНСКИЕ ===\n"
        for i, c in enumerate(self.civilians):
            info += f"[{i}] {c._name} ({c._location})\n"
        info += "\n=== ПОЛИЦИЯ ===\n"
        for i, p in enumerate(self.police_officers):
            info += f"[{i}] {p._department} ({p._location})\n"
        info += "\n=== СКОРАЯ ===\n"
        for i, a in enumerate(self.ambulances):
            info += f"[{i}] {a._station} ({a._location})\n"
        
        info_text.insert(1.0, info)
        info_text.config(state='disabled')
    
        def on_remove():
            try:
                idx = index_var.get()
                if type_var.get() == "civilian":
                    self._remove_civilian(idx)
                elif type_var.get() == "police":
                    self._remove_police(idx)
                else:
                    self._remove_ambulance(idx)
                dialog.destroy()
            except Exception as e:
                messagebox.showerror("Ошибка", str(e))
    
        ttk.Button(frame, text="Удалить", command=on_remove).grid(row=3, column=0, columnspan=4, pady=10)

    def _update_observer_list(self):
        self.txt_observers.config(state='normal')
        self.txt_observers.delete(1.0, tk.END)
        
        self.txt_observers.insert(tk.END, "=== ГРАЖДАНСКИЕ ===\n")
        for c in self.civilians:
            self.txt_observers.insert(tk.END, f"• {c._name} ({c._location})\n")
            
        self.txt_observers.insert(tk.END, "\n=== ПОЛИЦИЯ ===\n")
        for p in self.police_officers:
            self.txt_observers.insert(tk.END, f"• {p._department} ({p._location})\n")
            
        self.txt_observers.insert(tk.END, "\n=== СКОРАЯ ===\n")
        for a in self.ambulances:
            self.txt_observers.insert(tk.END, f"• {a._station} ({a._location})\n")
            
        self.txt_observers.config(state='disabled')

    def _show_all_windows(self):
        """Показать все открытые окна наблюдателей"""
        count = 0
        for obs in self.civilians + self.police_officers + self.ambulances:
            if hasattr(obs, '_gui_window') and obs._gui_window:
                obs._gui_window.show()
                count += 1
        self._log(f"Показано окон: {count}")

    def _create_widgets(self):
        main_frame = ttk.Frame(self.root, padding="10")
        main_frame.grid(row=0, column=0, sticky=(tk.W, tk.E, tk.N, tk.S))
        self.root.columnconfigure(0, weight=1)
        self.root.rowconfigure(0, weight=1)

        # Левая панель: Форма создания инцидента
        form_frame = ttk.LabelFrame(main_frame, text="Создание отчета об инциденте", padding="10")
        form_frame.grid(row=0, column=0, sticky=(tk.N, tk.S, tk.E, tk.W), padx=5, pady=5)
        main_frame.columnconfigure(0, weight=1)
        main_frame.rowconfigure(0, weight=1)

        ttk.Label(form_frame, text="Тип инцидента:").grid(row=0, column=0, sticky=tk.W, pady=2)
        types = [t.value for t in IncidentType]
        self.combo_type = ttk.Combobox(form_frame, textvariable=self.var_type, values=types, state="readonly")
        self.combo_type.current(0)
        self.combo_type.grid(row=0, column=1, sticky=(tk.W, tk.E), pady=2)

        ttk.Label(form_frame, text="Место:").grid(row=1, column=0, sticky=tk.W, pady=2)
        ttk.Entry(form_frame, textvariable=self.var_location).grid(row=1, column=1, sticky=(tk.W, tk.E), pady=2)

        ttk.Label(form_frame, text="Время:").grid(row=2, column=0, sticky=tk.W, pady=2)
        ttk.Entry(form_frame, textvariable=self.var_time).grid(row=2, column=1, sticky=(tk.W, tk.E), pady=2)

        ttk.Label(form_frame, text="Публичный отчет:").grid(row=3, column=0, sticky=tk.W, pady=2)
        ttk.Entry(form_frame, textvariable=self.var_public_report).grid(row=3, column=1, sticky=(tk.W, tk.E), pady=2)

        ttk.Label(form_frame, text="Полный отчет:").grid(row=4, column=0, sticky=tk.NW, pady=2)
        self.txt_full_report = scrolledtext.ScrolledText(form_frame, height=5, width=30)
        self.txt_full_report.grid(row=4, column=1, sticky=(tk.W, tk.E), pady=2)

        ttk.Label(form_frame, text="Уровень (1-10):").grid(row=5, column=0, sticky=tk.W, pady=2)
        ttk.Spinbox(form_frame, from_=1, to=10, textvariable=self.var_severity, width=5).grid(row=5, column=1, sticky=tk.W, pady=2)

        chk_conf = ttk.Checkbutton(form_frame, text="Подтверждено", variable=self.var_confirmed)
        chk_conf.grid(row=6, column=0, columnspan=2, sticky=tk.W, pady=2)
        
        chk_inj = ttk.Checkbutton(form_frame, text="Есть пострадавшие", variable=self.var_injured)
        chk_inj.grid(row=7, column=0, columnspan=2, sticky=tk.W, pady=2)

        btn_create = ttk.Button(form_frame, text="Создать инцидент", command=self._on_create_click)
        btn_create.grid(row=8, column=0, columnspan=2, pady=10, sticky=(tk.W, tk.E))

        # Левая панель: Форма добавления наблюдателя
        obs_frame = ttk.LabelFrame(main_frame, text="Добавить наблюдателя", padding="10")
        obs_frame.grid(row=1, column=0, sticky=(tk.N, tk.S, tk.E, tk.W), padx=5, pady=5)
        main_frame.rowconfigure(1, weight=0)
        
        ttk.Label(obs_frame, text="Тип:").grid(row=0, column=0, sticky=tk.W, pady=2)
        obs_types = [("Гражданский", "civilian"), ("Полиция", "police"), ("Скорая", "ambulance")]
        for i, (text, val) in enumerate(obs_types):
            ttk.Radiobutton(obs_frame, text=text, variable=self.var_new_observer_type, value=val).grid(row=0, column=i+1, sticky=tk.W)
        
        ttk.Label(obs_frame, text="Локация:").grid(row=1, column=0, sticky=tk.W, pady=2)
        ttk.Entry(obs_frame, textvariable=self.var_new_obs_location).grid(row=1, column=1, columnspan=2, sticky=(tk.W, tk.E), pady=2)
        
        # Функция обновления подписи поля
        def update_label(*args):
            if self.var_new_observer_type.get() == "civilian":
                self.lbl_obs_detail.config(text="Имя:")
            elif self.var_new_observer_type.get() == "police":
                self.lbl_obs_detail.config(text="Отдел:")
            else:
                self.lbl_obs_detail.config(text="Станция:")
        
        self.var_new_observer_type.trace_add("write", update_label)
        
        self.lbl_obs_detail = ttk.Label(obs_frame, text="Имя:")
        self.lbl_obs_detail.grid(row=2, column=0, sticky=tk.W, pady=2)
        self.entry_obs_detail = ttk.Entry(obs_frame, textvariable=self.var_new_name)
        self.entry_obs_detail.grid(row=2, column=1, columnspan=2, sticky=(tk.W, tk.E), pady=2)
        
        ttk.Button(obs_frame, text="Добавить наблюдателя", command=self._on_add_observer_click).grid(row=3, column=0, columnspan=3, pady=5)
        
        # Устанавливаем начальное состояние
        update_label()

        # Правая панель: Логи и Статистика
        status_frame = ttk.LabelFrame(main_frame, text="Логи и Статистика", padding="10")
        status_frame.grid(row=0, column=1, rowspan=2, sticky=(tk.N, tk.S, tk.E, tk.W), padx=5, pady=5)
        main_frame.columnconfigure(1, weight=2)

        ttk.Label(status_frame, text="Активные наблюдатели:").grid(row=0, column=0, sticky=tk.W, pady=2)
        self.txt_observers = scrolledtext.ScrolledText(status_frame, height=8, state='disabled')
        self.txt_observers.grid(row=1, column=0, sticky=(tk.N, tk.S, tk.E, tk.W), pady=5)
        status_frame.columnconfigure(0, weight=1)
        status_frame.rowconfigure(1, weight=1)
        
        ttk.Label(status_frame, text="Логи событий:").grid(row=2, column=0, sticky=tk.W, pady=2)
        self.txt_log = scrolledtext.ScrolledText(status_frame, height=10, state='disabled')
        self.txt_log.grid(row=3, column=0, sticky=(tk.N, tk.S, tk.E, tk.W), pady=5)
        status_frame.rowconfigure(3, weight=2)
        
        stats_frame = ttk.Frame(status_frame)
        stats_frame.grid(row=4, column=0, sticky=(tk.W, tk.E), pady=5)
        
        self.lbl_civ = ttk.Label(stats_frame, text="Гражданские: 0", foreground="blue")
        self.lbl_civ.grid(row=0, column=0, padx=10)
        self.lbl_pol = ttk.Label(stats_frame, text="Полиция: 0", foreground="darkgreen")
        self.lbl_pol.grid(row=0, column=1, padx=10)
        self.lbl_amb = ttk.Label(stats_frame, text="Скорая: 0", foreground="red")
        self.lbl_amb.grid(row=0, column=2, padx=10)

        ttk.Button(status_frame, text="Очистить логи", command=self._clear_logs).grid(row=5, column=0, sticky=tk.E, pady=5)
        btn_show_all = ttk.Button(status_frame, text="👁️ Показать все окна", 
                          command=self._show_all_windows)
        btn_show_all.grid(row=6, column=0, sticky=tk.W, pady=5)

        btn_remove = ttk.Button(status_frame, text="❌ Удалить наблюдателя", 
                       command=self._on_remove_observer_click)
        btn_remove.grid(row=7, column=0, sticky=tk.W, pady=5)
        
        self._update_observer_list()

    def _log(self, message):
        self.txt_log.config(state='normal')
        self.txt_log.insert(tk.END, message + "\n")
        self.txt_log.see(tk.END)
        self.txt_log.config(state='disabled')

    def _clear_logs(self):
        self.txt_log.config(state='normal')
        self.txt_log.delete(1.0, tk.END)
        self.txt_log.config(state='disabled')

    def _update_stats(self):
        self.lbl_civ.config(text=f"Гражданские: {len(self.civilians)}")
        self.lbl_pol.config(text=f"Полиция: {len(self.police_officers)}")
        self.lbl_amb.config(text=f"Скорая: {len(self.ambulances)}")

    def _on_create_click(self):
        try:
            type_value = self.var_type.get()
            incident_type = IncidentType(type_value)
            full_report_text = self.txt_full_report.get("1.0", tk.END).strip()

            if not self.var_location.get() or not self.var_time.get():
                self._log("ОШИБКА: Заполните место и время!")
                return

            self.publisher.create_incident_report(
                incident_type=incident_type,
                location=self.var_location.get(),
                time=self.var_time.get(),
                public_report=self.var_public_report.get(),
                full_report=full_report_text,
                severity_level=self.var_severity.get(),
                is_confirmed=self.var_confirmed.get(),
                has_injured=self.var_injured.get()
            )

            self._log("--- Инцидент успешно создан ---")
            self._update_stats()

        except Exception as e:
            self._log(f"ОШИБКА СОЗДАНИЯ: {str(e)}")