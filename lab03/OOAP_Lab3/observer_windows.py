import tkinter as tk
from tkinter import scrolledtext
import time

class ObserverWindow:
    def __init__(self, title: str, color: str, name: str, location: str, geometry: str = "400x500"):
        self.window = tk.Toplevel()
        self.window.title(f"{title} - {name}")
        self.window.geometry(geometry)
        self.window.configure(bg=color)
        
        self.window.protocol("WM_DELETE_WINDOW", self._hide)
        
        # Заголовок с именем
        header = tk.Label(self.window, text=f"{title}\n{name}", font=("Arial", 14, "bold"),
                         bg=color, fg="white")
        header.pack(pady=10, fill=tk.X)
        
        # Информационная строка
        self.info_frame = tk.Frame(self.window, bg=color)
        self.info_frame.pack(fill=tk.X, padx=10, pady=5)
        
        self.lbl_location = tk.Label(self.info_frame, text=f"📍 {location}",
                                     font=("Arial", 9), bg=color)
        self.lbl_location.pack(side=tk.LEFT)
        
        self.lbl_count = tk.Label(self.info_frame, text="📊 Получено: 0",
                                  font=("Arial", 9), bg=color)
        self.lbl_count.pack(side=tk.RIGHT)
        
        # Чат
        chat_frame = tk.LabelFrame(self.window, text="Уведомления", font=("Arial", 10, "bold"))
        chat_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)
        
        self.txt_chat = scrolledtext.ScrolledText(chat_frame, wrap=tk.WORD,
                                                  font=("Consolas", 10),
                                                  state='disabled',
                                                  height=15)
        self.txt_chat.pack(fill=tk.BOTH, expand=True, padx=5, pady=5)
        
        # Статус
        self.lbl_status = tk.Label(self.window, text="Ожидание событий...",
                                   font=("Arial", 8), bg=color, fg="gray")
        self.lbl_status.pack(pady=5)
        
        # Кнопка очистки
        btn_frame = tk.Frame(self.window, bg=color)
        btn_frame.pack(fill=tk.X, padx=10, pady=5)
        
        tk.Button(btn_frame, text="Очистить чат",
                 command=self._clear_chat,
                 bg=color, relief=tk.RAISED).pack(side=tk.RIGHT)
        
        self.incident_count = 0
        self._hidden = False
        self._location = location
        self._name = name
    
    def _clear_chat(self):
        self.txt_chat.config(state='normal')
        self.txt_chat.delete(1.0, tk.END)
        self.txt_chat.config(state='disabled')
        self.incident_count = 0
        self._update_count()
    
    def _update_count(self):
        self.lbl_count.config(text=f"📊 Получено: {self.incident_count}")
    
    def _hide(self):
        self.window.withdraw()
        self._hidden = True
    
    def show(self):
        self.window.deiconify()
        self._hidden = False
    
    def is_hidden(self):
        return self._hidden
    
    def log(self, message: str, severity: int = 0, incident_id: int = 0):
        self.txt_chat.config(state='normal')
        
        self.txt_chat.insert(tk.END, f"ID:{incident_id} {message}\n")
        self.txt_chat.see(tk.END)
        self.txt_chat.config(state='disabled')
        
        self.incident_count += 1
        self._update_count()


class CivillianWindow(ObserverWindow):
    def __init__(self, name: str, location: str):
        super().__init__("👤 Гражданские наблюдатели", "#e8f4e8", name, location, "400x500")


class PoliceWindow(ObserverWindow):
    def __init__(self, name: str, location: str):
        super().__init__("🚓 Полицейские наблюдатели", "#e0e8ff", name, location, "450x550")


class AmbulanceWindow(ObserverWindow):
    def __init__(self, name: str, location: str):
        super().__init__("🚑 Скорая помощь", "#ffe0e0", name, location, "450x550")