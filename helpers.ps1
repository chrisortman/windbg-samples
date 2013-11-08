set-alias -Name simple-debug 'C:\code\windbg-samples\SimpleApp.Wpf\bin\Debug\SimpleApp.Wpf.exe'
set-alias -Name windbg86 'C:\Program Files (x86)\Windows Kits\8.0\Debuggers\x86\windbg.exe'
set-alias -name simple-release 'C:\code\windbg-samples\SimpleApp.Wpf\bin\Release\SimpleApp.Wpf.exe'
function copy-dump
{
    $dumpFile = 'C:\users\chris\AppData\Local\temp\SimpleApp.Wpf.DMP'
    $count = (Get-ChildItem -Path C:\temp\blackmagic -Filter *.DMP | Measure-Object).Count
    $count = $count + 1
    Move-Item $dumpFile "C:\temp\blackmagic\$($count)_SimpleApp.Wpf.DMP"

}
