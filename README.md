# Sogeti Academy

Home of the source code for Sogeti Academy. This repository is completely open source and therefore contains no passwords, usernames, keys, etc.

Please contact Bryce Klinker for access to the repository with the above information.

# Getting Started

## Required software
  1. [Visual Studio 2015](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx)
    1. [Visual Studio 2015 Update 1](http://go.microsoft.com/fwlink/?LinkId=691129)
  2. [NodeJS](https://nodejs.org/en/)
  3. [Git](https://git-scm.com/)
  
##  Start helping
  1. Clone the github repository
    1. Open a command prompt and change to the directory you want to keep your source code. 
    2. git clone https://github.com/bryce-klinker-sogeti/sogeti-academy
      1. This will create a sogeti-academy directory. This directory will contain all of the code for Sogeti Academy
  2. Install gulp globally. In a command prompt type the following
    2. npm install gulp -g
  3. Open the sogeti-academy/Sogeti.Academy.sln in Visual Studio
  4. Right-click on the Solution 'Sogeti.Academy' item in the Solution Explorer window. Select "Restore NuGet Packages"
  5. Right-click on the Dependencies item under Sogeti.Academy node in Solution Explorer. Select "Restore Packages"
  6. You will also need to setup some environment variables.
    1. Please contact Bryce Klinker for access to these as they are not suitable to be part of the open source code.
  7. You will now be ready to start debugging and working with the code.
  
# FYI
  1. If you would like to commit code to the central repository you will need to do one of two things
    1. Let Bryce Klinker know you have something to push to Sogeti Academy and ask for contributor access.
    2. Fork the sogeti academy repo
      1. Make your modifications
      2. Push your changes to your forked repo
      3. Open a Pull Request for Sogeti Academy
      4. Bryce Klinker will be notified and will merge the changes into the central repo.
  2. This project is setup for continous delivery. This means as soon as code hits the sogeti-academy repo it is deployed to azure and is live in minutes. Keep your code production quality.
