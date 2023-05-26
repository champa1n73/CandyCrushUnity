# CandyCrush

To compile and run an existing Unity project, follow these general instructions:

1. Install Unity: If you haven't already, download and install the Unity Hub and Unity Editor from the official Unity website (https://unity.com/).

2. Open Unity Hub: Launch the Unity Hub application.

3. Add your project: Click on the "Projects" tab in the Unity Hub and then click the "Add" button. Browse to the location of your existing Unity project and select its root folder. Click "Open" to add the project to Unity Hub (Add the folder which is named "CandyCrush").

4. Select the Unity version: After adding the project, click on it in the Unity Hub's "Projects" tab. If you have multiple Unity versions installed, select the appropriate Unity version for the project. If the project was created with a newer version of Unity, you might need to upgrade Unity to that version. (My current version is 2021.3.24f1).

5. Open the project: Click the project appeared on "Projects" tab to launch the Unity Editor with your project ("CandyCrush" folder).

6. After open the project, head to "Edit" > "Project Settings" > "Player". In the Android tab (the one with Android icon) Check these options: "Package Name" (if this option is filled with "com.champa1n73.CandyCrush", you don't have to change anything, else, just copy the line I mentioned), "Minimum API Level" : "API level 33", "Target API Level": "Automatic (highest installed)" or "API level 33", "Install Location": "Automatic", "Write Permission": "External (SD Card)".

7. I tested the game on "LD Player" so before build the program please install LD Player from the official LD Player website (https://vn.ldplayer.net/), (I used LD Player 9).

8. Build the project: In the Unity Editor, navigate to "File" > "Build Settings." In the "Build Settings" window, select your target platform (Android) and click the "Switch Platform" button. Wait for Unity to finish the platform switching process. Make sure "Scenes/Splash" is upper "Scenes/Main", check "Run device": "All compatible devices". Then Click Build.

9. If you want to test before Build in Unity Editor, in "Project" tab, open "Assets" > "Scenes" > double click on "Splash", then hit the play button on the top, near the "Hierarchy" tab. If you can't find the "Project" tab, "Window" > "Panels" > "Project".

10. Drag and drop the apk file to LD Player screen and test it.

NOTE: You can use other Android Emulator but make sure the API level is 33.
