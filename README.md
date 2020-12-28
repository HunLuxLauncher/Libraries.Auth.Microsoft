# Microsoft authentication module for `Libraries.Auth`
Minecraft authentication library for the *new* Microsoft Authentication method. This library requires `Libraries.Auth` to work.

> **IMPORTANT!**
> All of HunLuxLauncher's projects **require** CzompiSoftware's NuGet server (because most of the packages are from there), so you need to add a NuGet repository to your VS or download [this NuGet.config](https://raw.githubusercontent.com/CzompiSoftware/SampleProject/master/nuget.config) file and place it next to your .sln file.
> If you'd like to manually add CzompiSoftware's NuGet server, then add the following url to your `Visual Studio` or `NuGet.config` file:
> ```
> https://nuget.czompisoftware.hu/v3/index.json
> ```

## Current state of development
- :ballot_box_with_check: Authenticate with Microsoft
- :ballot_box_with_check: Authenticate with Xbox Live
- :ballot_box_with_check: Authenticate with Minecraft
- :ballot_box_with_check: Checking Game Ownership
- :ballot_box_with_check: Get the profile
- :ballot_box_with_check: Fully reworked and made much simpler to work with

## How does it work?
> Check out the [sample WPF project](https://github.com/HunLuxLauncher/Libraries.Auth.Microsoft.Sample) to understand how does it work.

### Used sources:
- [wiki.vg](https://wiki.vg/Microsoft_Authentication_Scheme) 
- [Calling Xbox Live Services from Your Title Service](http://strauss.hu/download/16)

> A product of [Czompi Software](https://czompisoftware.hu/en/).
