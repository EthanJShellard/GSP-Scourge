@ECHO OFF
ECHO #####     ADDING TRACKED FOLDERS     #####  
git add .\Scourge-GSP\Assets*
git add .\Scourge-GSP\ProjectSettings*
ECHO #####     ADDING COMPLETE            ##### 
set /P comment="Please input comment:"
ECHO #####     COMMITTING                 ##### 
git commit -m "%comment%"
ECHO #####     PULLING                    ##### 
git pull
ECHO #####     PUSHING                    ##### 
git push
ECHO #####     FINISHED                   ##### 
PAUSE