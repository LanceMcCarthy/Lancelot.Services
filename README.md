# Lancelot Software - Windows Services

A repository of a bunch of my helpful Windows services. Find each service's documentation in its own readme, links in the table below.

| Workflow | Documentation | Status |
|----------|---------------|--------|
| Main | none | ![Main](https://github.com/LanceMcCarthy/Lancelot.Services/workflows/Main/badge.svg) |
| File Watcher Service (Release) | [Readme](https://github.com/LanceMcCarthy/Lancelot.Services/blob/main/src/FileWatcher.Service/README.md) | ![Release - File Watcher Service](https://github.com/LanceMcCarthy/Lancelot.Services/workflows/Release%20-%20File%20Watcher%20Service/badge.svg?branch=release-file-watcher-service) |

# File Watcher Service

This service is simple and reliable hand-off helper that will copy any file changes in a source folder to a destination folder.

## Download/Compile

First order of business is to get FileWatcher.Service.exe and its dependencies. The easiest options are:

- [Option 1] - Download from the artifacts from the GitHub Actions build result or from the GitHub Releases page.
    - Go to [File Watcher Actions](https://github.com/LanceMcCarthy/Lancelot.Services/actions?query=workflow%3A%22Release+-+File+Watcher+Service%22) tab, select the newest build with a green check, then download the Artifacts
- [Option 2] - Compile the project in Visual Studio and 
  - Find the files in the `/bin/Release/` folder

Once you get the files, you will want it to be in a safe location so it doesnt get accidentally deleted. I recommend creating a dedicated folder on your C:/ drive. For example, I created `C:\MyServices\FileWatcherService\` and copied all the files into it:

![image](https://user-images.githubusercontent.com/3520532/107980614-97ef1780-6f8e-11eb-8872-d0622a5c60a5.png)

## Install - Setup

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

## Uninstall - Stop and Delete Service

To remove and delete the service, take the following steps

1. Open Powershell in Administrator mode
2. Run `sc.exe stop "My File Watcher Service"`
3. Run `sc.exe delete "My File Watcher Service"`

Here's a screenshot of what you should see

![image](https://user-images.githubusercontent.com/3520532/107978954-ad167700-6f8b-11eb-8fa1-2346ebd076a3.png)

