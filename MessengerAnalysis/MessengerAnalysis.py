import os
import json
import getpass
from pprint import pprint

def openJson():
    user = getpass.getuser()
    path = os.path.join('C:\\Users\\', user, 'Documents\\StatsMessengerJson\\','messages.json')
    with open(path, encoding="utf8") as f:
        data = json.load(f)
    # os.remove(path)
    return data

json = openJson()