InstaBot was written to simulate sending messages in Instagram on PC. 
It achieves this by taking under control a cursor on your monitor.

A csv files are used to provide nicknames from instagram. A csv file shoud contain only one colomn. NO ',' separator!!!
Appropriate types of csv data are: 
-> '\USER_NAME'
-> 'USER_NAME'
-> 'https://www.instagram.com/USER_NAME/?hl=ru'
-> 'https://www.instagram.com/USER_NAME'
-> 'https://www.instagram.com/USER_NAME?123123'

A txt files are used to provide messages which you want to send. The will be randomly selected. 
In txt files messages shoud be separated by '\n'.

The programm requires a json file named "insta_send_message.json" which contains coordinates for each button that is used to send a message
You can change those values for your window. 
Also it is important that you was logged in the Instagram page you want to send message from.
During the program is working it better not to move mouse and let it to finish a cycle.

To eliminate the possibility of getting a ban from Instagram the bot chooses a random number of persons betwen 10 and 15 for each cycle. 
After each cycle you will see a timer that shows the time until the next cycle.
The bot  also deliberately makes pauses between actions. 
The user can stop the programm in any moment. But the programm stops only after it finishes processing  user that was currently processed. 
If the Internet connection was lost bot tries to restart  send messages every 2 minutes. 


