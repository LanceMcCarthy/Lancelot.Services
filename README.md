# Lancelot Software - Windows Services

A repository of a bunch of my helpful Windows services.

| Workflow | Status |
|---------|--------------|
| Main | ![Main](https://github.com/LanceMcCarthy/Lancelot.Services/workflows/Main/badge.svg) |
| Release - File Watcher Service | ![Release - File Watcher Service](https://github.com/LanceMcCarthy/Lancelot.Services/workflows/Release%20-%20File%20Watcher%20Service/badge.svg) |

You will find separate READMEs for each service the project's root folder. Below are concise getting started options.

## File Watcher

### Install/Setup

1. Open Powershell in Administrator mode
2. Navigate to the folder you downloaded (or compiled) **FileWatcher.Service.exe** to.
3. Run  `sc.exe create "My File Watcher Service" binPath="C:\Downloads\FileWatcher.Service.exe C:\SourceFolder C:\DestinationFolder"`
4. Run  `sc.exe start "My File Watcher Service"`

![image](https://user-images.githubusercontent.com/3520532/107978424-bbb05e80-6f8a-11eb-9062-a5fdc4576ff0.png)

Once it is started, you will be able to see it in Task Manager's Services tab:

![image](https://user-images.githubusercontent.com/3520532/107978566-04681780-6f8b-11eb-882f-4f68e139c0a4.png)

That service will now automatically copy any new files/file changes to the destination folder!

### Stop/Remove

To remove and delete the service, take the following steps

1. Open Powershell in Administrator mode
2. Run `sc.exe stop "My File Watcher Service"`
3. Run `sc.exe delete "My File Watcher Service"`

Here's a screenshot of what you should see

![image](https://user-images.githubusercontent.com/3520532/107978954-ad167700-6f8b-11eb-8fa1-2346ebd076a3.png)
