# File Watcher Service

![Release - File Watcher Service](https://github.com/LanceMcCarthy/Lancelot.Services/workflows/Release%20-%20File%20Watcher%20Service/badge.svg).

This service is simple and reliable hand-off helper that will copy any file changes in a source folder to a destination folder.

`FileWatcher.Service.exe C:\Source C:\Destination -recurive=true -overwrite=false`

> Source and Destination are required. Defaults: Recurive=true, Overwrite=false (will skip)

## Download/Compile

First order of business is to downloaded (or compile) the FileWatcher.Service.exe and related files. Whereever you put it, you will want it to be in a safe location so it doesnt get deleted. I recommend creating a simple folder on your C:/ drive just to put services files into.

For example, I created `C:\MyServices\FileWatcherService\` and copied all the files into it:

![image](https://user-images.githubusercontent.com/3520532/107980614-97ef1780-6f8e-11eb-8872-d0622a5c60a5.png)

## Setup

Once you have the files in a known location, you will now have a path to **FileWatcher.Service.exe**, you can start up the service.

1. Open Powershell in Administrator mode
2. Run the service `create` command you need 3 things:
   - The path to the exe
   - The Folder to watch for file changes (e.g. C:\Source)
   - The folder the files will get copied into (e.g. C:\Destination)

   `sc.exe create "My File Watcher Service" binPath="C:\MyServices\FileWatcherService\FileWatcher.Service.exe C:\SourceFolder C:\DestinationFolder"`
3. Run the service start command

    `sc.exe start "My File Watcher Service"`

![image](https://user-images.githubusercontent.com/3520532/107978424-bbb05e80-6f8a-11eb-9062-a5fdc4576ff0.png)

Once it is started, you will be able to see it in Task Manager's Services tab:

![image](https://user-images.githubusercontent.com/3520532/107978566-04681780-6f8b-11eb-882f-4f68e139c0a4.png)

That service will now automatically copy any new files/file changes to the destination folder!

## Remove Stop/Remove

To remove and delete the service, take the following steps

1. Open Powershell in Administrator mode
2. Run `sc.exe stop "My File Watcher Service"`
3. Run `sc.exe delete "My File Watcher Service"`

Here's a screenshot of what you should see

![image](https://user-images.githubusercontent.com/3520532/107978954-ad167700-6f8b-11eb-8fa1-2346ebd076a3.png)
