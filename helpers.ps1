$mydir = (Split-Path -parent $MyInvocation.MyCommand.Definition)

set-alias -Name simple-debug "$mydir\SimpleApp.Wpf\bin\Debug\SimpleApp.Wpf.exe"
set-alias -name simple-release "$mydir\SimpleApp.Wpf\bin\Release\SimpleApp.Wpf.exe"
set-alias -Name windbg86 "C:\Program Files (x86)\Windows Kits\8.0\Debuggers\x86\windbg.exe"

function copy-dump
{
    $dumpFile = "$env:TEMP\SimpleApp.Wpf.DMP"
    $count = (Get-ChildItem -Path C:\temp\blackmagic -Filter *.DMP | Measure-Object).Count
    $count = $count + 1
    Move-Item $dumpFile "C:\temp\blackmagic\$($count)_SimpleApp.Wpf.DMP"
    Write-Host "Copied SimpleApp.Wpf.DMP to $($count)_SimpleApp.Wpf.DMP"
}
