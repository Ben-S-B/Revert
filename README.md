[![Build status](https://ci.appveyor.com/api/projects/status/imjseh4u5rninkwl?svg=true)](https://ci.appveyor.com/project/mrexodia/fabiano-swagger-of-doom)

## What is this?

This is a private server for [Realm of the Mad God](http://www.realmofthemadgod.com) forked from [here](https://github.com/ossimc82/fabiano-swagger-of-doom). It is mostly a cleanup of the original source and the focus lies on getting a server running as quickly and easily as possible.

## How to run this on my own server?

**Notice:** This guide is based on http://www.mpgh.net/forum/showthread.php?t=959037

1. Compile `server.sln` with Visual Studio 2015 or higher in `Release` mode. If you don't have Visual Studio, you can get the latest binaries [here](https://ci.appveyor.com/project/mrexodia/fabiano-swagger-of-doom/build/artifacts).
2. Install [XAMPP](https://www.apachefriends.org) and configure a user/database called `rotmgprod` and import `db\rotmgprod.sql` to get the right tables. **Do not allow outside connections if you don't set a password!**
3. Add the following line to `%systemroot%\System32\drivers\etc\hosts` on the server: `127.0.0.1 c453.pw`
4. Add firewall/port forwarding rules for ports 80 and 2050
5. Start `bin\Release\server.exe` as Administrator (required for access to port 80). Make sure to uncheck `Use port 80 and 443 for additional incoming connections` in Skype.
6. Start `bin\Release\wServer.exe`
7. Visit http://c453.pw in your browser to play.

## How to allow clients to connect to my server?

1. Configure your firewall to allow outside connections.
2. On the client side add `se.rv.er.ip c453.pw` to `%systemroot%\System32\drivers\etc\hosts`
3. Visit http://c453.pw in your browser to play.

Alternatively you can use [RABCDAsm](https://github.com/CyberShadow/RABCDAsm) to modify `AssembleeGameClient201409192100.swf` to directly connect to your server domain/IP (this method is not recommended if you are just trying to play with some friends):

1. `abcexport AssembleeGameClient201409192100.swf`
2. `rabcdasm AssembleeGameClient201409192100-1.abc`
3. Replace all occurrences of `c453.pw` with your domain/IP in all `*.asasm` files.
4. `rabcasm AssembleeGameClient201409192100-1\AssembleeGameClient201409192100-1.main.asasm`
5. `abcreplace AssembleeGameClient201409192100.swf 1 AssembleeGameClient201409192100-1\AssembleeGameClient201409192100-1.main.abc`

**Do not contact me for help, I have none to offer.**

## Additional License information

This software is licensed under [AGPLv3](https://choosealicense.com/licenses/agpl-3.0). In practice this means that **you have to distribute the source code of your application, even if you are only hosting the server and not distributing server binaries**.

You are free to use this source as long as u credit this guys:

- ossimc82/Fabian Fischer
- C453
- Trapped
- Donran
- creepylava
- Krazyshank
- Barm
- Nilly
- sebastianfra12 for some more behaviors
- Kieron for making 1 behavior
- everyone else that have contributed to this project on mpgh or any other site

And you are not "WintersQ" and "I Don't Love You" on MPGH
