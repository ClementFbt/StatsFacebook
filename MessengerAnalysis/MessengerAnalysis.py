import os
import json
import getpass
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
import datetime
from pprint import pprint
import matplotlib.dates as mdates

user = getpass.getuser()
pathFolder = os.path.join('C:\\Users\\', user, 'Documents\\StatsMessengerJson\\')

#import json into python code
def openJson():
    path = os.path.join(pathFolder,'messages.json')
    with open(path, encoding="utf8") as f:
        data = json.load(f)
    for message in data['messages']:
        message['timestamp_datetime'] = datetime.datetime.fromtimestamp(message['timestamp_ms']/1000.0)
    # os.remove(path)
    return data

json = openJson()

# count number of messages per user per day
def countMessagesPerUserDay(data):
    path = os.path.join(pathFolder,'messagesPerUserDay.png')
    messages = pd.DataFrame.from_dict(json['messages'])
    newMessages = messages[['timestamp_datetime', 'sender_name']].copy()
    
    messagesPerUserDay = newMessages.groupby([newMessages['timestamp_datetime'].dt.date, 'sender_name']).size().unstack()
    

    ax = messagesPerUserDay.plot(figsize=(35,6))
    ax.xaxis.set_major_locator(mdates.MonthLocator())
    plt.xticks(rotation=45)
    ax.set_ylabel('Nombre de messages par personne')
    ax.set_xlabel('Date')
    ax.set_title('Nombre de messages par jour et par utilisateur')
    
    fig = ax.get_figure()
    fig.savefig(path)
    
# count number of messages per user per day
def countMessagesPerHour(data):
    path = os.path.join(pathFolder,'messagesPerHour.png')
    messages = pd.DataFrame.from_dict(json['messages'])
    newMessages = messages[['timestamp_datetime', 'sender_name']].copy()

    messagesPerHour = newMessages.groupby(messages['timestamp_datetime'].dt.hour).size()
    #radar chart

# cloud map of most used words (per user in one map)

# cloud map of most used emoji

# average messages per day

# average words per messages per user

# most used emoji

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



# create pie chart %msg by user
def percentageMessagesPerUser(msgByUser):
    path = os.path.join(pathFolder,'messagesPerUserTotal.png')
    
    fig, ax = plt.subplots()

    vals = []
    labels = list(msgByUser.keys())
    
    for user in msgByUser.values():
        vals.append(sum(list(user.values())))
    wedges, texts, autotexts = ax.pie(vals, labels=tuple(labels), autopct='%.1f%%', pctdistance=0.8, textprops=dict(color="w"))

    ax.legend(wedges, labels, title="Noms", loc="center left", bbox_to_anchor=(0.85, 0, 0.5, 1))
    ax.set_title('Messages totaux envoyés')
    ax.axis('equal')
    my_circle=plt.Circle( (0,0), 0.65, color='white')
    p=plt.gcf()
    p.gca().add_artist(my_circle)
    plt.savefig(path)
    
percentageMessagesPerUser(countByUser(json))
countMessagesPerUserDay(json)
countMessagesPerHour(json)