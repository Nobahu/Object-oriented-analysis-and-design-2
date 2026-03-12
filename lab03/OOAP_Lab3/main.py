import tkinter as tk
from tkinter import ttk
from gui import IncidentGUI

if __name__ == "__main__":
    root = tk.Tk()
    
    # Установка стиля для лучшего вида
    style = ttk.Style()
    style.theme_use('clam')

    # Передаем None вместо окон
    app = IncidentGUI(root, None, None, None)
    
    root.mainloop()