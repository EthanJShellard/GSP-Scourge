@ECHO OFF
git add .\Scourge-GSP\Assets*
git add .\Scourge-GSP\ProjectSettings*
ECHO Adds complete
set /P comment="Please input comment:"
git commit -m "%comment%"
git pull
git push
ECHO Ignore the red mess talking about untracked files
PAUSE