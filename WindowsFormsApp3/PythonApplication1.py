from tkinter import *
from tkinter import filedialog
from tkinter.filedialog import asksaveasfile
from tkinter.filedialog import askopenfile
filename = None

def newFile():
    global filename
    filename = "Untitled"
    text.delete(0.0, END)

def saveFile():
    global filename
    t = text.get(0.0, END)
    f = open(filename, 'w')
    f.write(t)
    f.close()

def saveAs():
    f = asksaveasfile(defaultextension='.txt')
    t = text.get(0.0, END)
    try:
        f.write(t.rstrip())
    except:
        showerror(title="Greska!", message="Datoteka se ne moze sacuvati...")

root = Tk()
        
def openFile():
    global filename
    file = askopenfile(parent=root,title='Odaberite datoteku')
    filename = file.name
    t = file.read()
    text.delete(0.0, END)
    text.insert(0.0, t)
    file.close()



root.title("Pisanje teksta realizirano u pythonu")
root.minsize(width=400, height=400)
root.maxsize(width=400, height=400)

text = Text(root, width=400, height=400)
text.pack()

menubar = Menu(root)
filemenu = Menu(menubar)
filemenu.add_command(label="Novo", command=newFile)
filemenu.add_command(label="Otvori", command=openFile)
filemenu.add_command(label="Spremi", command=saveFile)
filemenu.add_command(label="Spremi kao", command=saveAs)
filemenu.add_separator()
filemenu.add_command(label="Zatvori", command=root.quit)
menubar.add_cascade(label="Datoteka", menu=filemenu)

root.config(menu=menubar)
root.mainloop()
