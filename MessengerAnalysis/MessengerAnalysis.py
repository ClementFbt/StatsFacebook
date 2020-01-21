import os
import json
import getpass
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from pprint import pprint

sns.set()

#import json into python code
def openJson():
    user = getpass.getuser()
    path = os.path.join('C:\\Users\\', user, 'Documents\\StatsMessengerJson\\','messages.json')
    with open(path, encoding="utf8") as f:
        data = json.load(f)
    # os.remove(path)
    return data

json = openJson()

messages = pd.DataFrame.from_dict(json['messages'])

# messages.head()


# count number of messages by users
def countByUser(data):
    dictUser = {}
    for message in data['messages']:
        if message['sender_name'] in dictUser:
            if 'content' in message.keys():
                dictUser[message['sender_name']]['content'] += 1
            if 'photos' in message.keys():
                dictUser[message['sender_name']]['photos'] += 1
            if 'videos' in message.keys():
                dictUser[message['sender_name']]['videos'] += 1
            if 'gifs' in message.keys():
                dictUser[message['sender_name']]['gifs'] += 1
            if 'sticker' in message.keys():
                dictUser[message['sender_name']]['sticker'] += 1
        else:
            dictUser[message['sender_name']] = {'content':0, 'photos':0, 'videos':0, 'gifs':0, 'sticker':0}
    return dictUser

cBU = countByUser(json)
print(cBU)

# create pie chart %msg by user
def percentageMessagesPerUser(msgByUser):
    fig1, ax1 = plt.subplots()
    ax1.pie(msgByUser.values(), explode=(0,0), labels=msgByUser.keys(), autopct='%1.1f%%', startangle=90)
    ax1.axis('equal')
    plt.show()
    
# percentageMessagesPerUser(cBU)
