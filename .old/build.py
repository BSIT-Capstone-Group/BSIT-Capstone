import subprocess, shutil, os
from pathlib import Path

def cls():
    os.system("cls")
WSL_NAMES = subprocess.run(["wsl", "-l"], capture_output=True, text=True)
WSL_NAMES = WSL_NAMES.stdout.strip().replace("\x00", "")
WSL_NAMES = [d.split(" ")[0] for d in WSL_NAMES.split("\n")[1:] if d]
WSL_NAME = WSL_NAMES[0]
ROOT_USER = "root"
PRE_COMMANDS = ["wsl", "-d", WSL_NAME, "-u", ROOT_USER, "sh"]

SOURCE_PATH = Path(os.path.join(os.getcwd(), "source"))

def runWSLCommand(*args, **kwargs):
    cmd_string = " ".join(("cd", "~", "&&") + args)
    sp = subprocess.run(PRE_COMMANDS + ["-c", cmd_string], **kwargs)
    return sp

def asWSLPath(path):
    path = Path(path)
    drive = path.drive[:-1]
    parts = path.parts[1:]
    npath = "/".join((drive, *parts)).lower()

    return f'"/mnt/{npath}"'

# print(SOURCE_PATH)

# runWSLCommand("cd", asWSLPath(SOURCE_PATH), "&&", "pwd")
runWSLCommand("cp", asWSLPath(SOURCE_PATH), Path(os.getcwd()).name, "-r", "--parents")