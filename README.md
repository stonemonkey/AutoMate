AutoMate
========

Hachaton2013Sibiu application for analyzing driving behavior.
Team: Adrian Barglazan (Web), Costin Morariu (WP8), Mihai Stancu (WP8)

Prerequisites:
- MSVS2013 Premium (http://www.visualstudio.com/downloads/download-visual-studio-vs);
- WP 8.0 SDK (http://www.microsoft.com/en-us/download/details.aspx?id=35471);
- WP8 unlocked for development (http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff769508(v=vs.105).aspx) with Data connection for sending statistics;
- Internet connection for downloading sources and used NuGet packages.

How to setup development environment and run application localy:
1 Get sources from GitHub (https://github.com/stonemonkey/AutoMate) to your local machine;
2 Open AutoMate.sln in MSVS2013 from your local machine;
3 Rebuild solution;
4 Publish Database (setup your prefered SQL server and datbase e.g. '.', "AutoMateDatabase");
5 Set Ui.Web\Web.config connection string 'DataContext' to upper step database location;
6 Set Ui.Wp8 as Startup project;
7 Setup Ui.Web and WebService hosting, you have to configure acces from internet to your machine (make sure your machine is accesible on an IP + port);
8 Setup Ui.Web Project deployment to your hosting from step 7 (Ui.Web Project properties Web tab -> Servers -> Project URL);
9 Set URL from step 7 in Ui.Wp8\Components\MainPage\MainPageViewModel ctor (e.g. "http://automatewebui.azurewebsites.net/api/automate" this is claud location used at Hachathon, will be accessible only temporary cca. 30 days);
10 Rebuild solution;
11 Connect WP8 to machine through USB, open it, keep it running (not in stend by);
12 Run application (Ui.Wp8);

Remarks:
- WP8 Application is still buggy :) but it is registering high acceleration (when you shake the phone havily for cca.3 seconds), you have to stop the recording and have internet connection up and running on WP then it will send statistics to Web page.


